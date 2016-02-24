using System;
using System.IO.Ports;
using System.Runtime.InteropServices;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Protocol;

namespace Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.IO.Serial
{
    public class SerialInput<T> : IInput<T> where T : struct, IProtocolMessage
    {
        private readonly SerialPort _port;
        private readonly object _lock = new object();
        private readonly byte[] _buffer;
        private readonly byte[] _syncMark;
        private readonly int _structSize;

        public SerialInput(SerialPort serialPort, byte sync0, byte sync1)
        {
            _port = serialPort;
            _structSize = Marshal.SizeOf<T>();
            _buffer = new byte[_structSize];
            _syncMark = new[] { sync0, sync1 };
        }

        public event EventHandler<ProtocolMessageEventArgs<T>> MessageReceived;

        public ListeningState ListeningState
        {
            get { return _port.IsOpen ? ListeningState.Active : ListeningState.Inactive; }
        }

        public void StartListening()
        {
            if (ListeningState == ListeningState.Inactive)
            {
                _port.Open();
                Sync();
            }
        }

        public void StopListening()
        {
            if (ListeningState == ListeningState.Active)
            {
                _port.Close();
            }
        }

        public Type InputMessageType
        {
            get { return typeof (T); }
        }

        private void PortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            lock (_lock)
            {
                while (_port.BytesToRead >= _structSize)
                {
                    var message = ReadBuffer();
                    if (message.Sync0 != _syncMark[0] || message.Sync1 != _syncMark[1])
                    {
                        Sync();
                        break;
                    }
                    OnMessageReceived(message);
                }
            }
        }

        private void OnMessageReceived(T message)
        {
            this.MessageReceived?.Invoke(this, new ProtocolMessageEventArgs<T>()
            {
                Message = message
            });
        }

        private T ReadBuffer()
        {
            _port.Read(_buffer, 0, _structSize);
            return ByteArrayToStructure<T>(_buffer);
        }

        private void PortError(object sender, SerialErrorReceivedEventArgs e)
        {
            Sync();
        }

        void AttachHandlers()
        {
            _port.ErrorReceived += PortError;
            _port.DataReceived += PortDataReceived;
        }

        void DetachHandlers()
        {
            _port.ErrorReceived -= PortError;
            _port.DataReceived -= PortDataReceived;
        }

        private void Sync()
        {
            DetachHandlers();
            _port.DiscardInBuffer();
            _port.DiscardOutBuffer();

            while (true)
            {
                for (var b = _port.ReadByte(); b != _syncMark[0]; b = _port.ReadByte())
                {
                    _buffer[0] = (byte)b;
                }
                var b1 = _port.ReadByte();
                if (b1 == _syncMark[1])
                {
                    _buffer[1] = (byte)b1;
                    _port.Read(_buffer, 2, _structSize - 2);
                    var message = ReadBuffer();
                    OnMessageReceived(message);
                    _port.ReceivedBytesThreshold = _structSize;
                    AttachHandlers();
                    break;
                }
            }

        }

        static TStruct ByteArrayToStructure<TStruct>(byte[] bytes) where TStruct : struct
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            TStruct ret = (TStruct)Marshal.PtrToStructure(handle.AddrOfPinnedObject(),
                typeof(TStruct));
            handle.Free();
            return ret;
        }
    }
}