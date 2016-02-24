using System;
using System.Timers;

namespace Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.IO
{
    public class DummyInput : IInput<DummyProtocolMessage>
    {
        public ListeningState ListeningState { get; private set; }
        Timer _timer = new Timer();
        private ulong sequenceId;

        public void StartListening()
        {
            if (ListeningState == ListeningState.Inactive)
            {
                ListeningState = ListeningState.Active;
                _timer.AutoReset = true;
                _timer.Interval = 10; //ms
                _timer.Elapsed += _timer_Elapsed;
                _timer.Start();
            }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnTimerTick(new DummyProtocolMessage(7, 31, 1, ++sequenceId, (ulong)DateTime.Now.Ticks));
        }

        private void OnTimerTick(DummyProtocolMessage dummyProtocolMessage)
        {
            MessageReceived?.Invoke(this, new ProtocolMessageEventArgs<DummyProtocolMessage>()
            {
                Message = dummyProtocolMessage
            });
        }

        public void StopListening()
        {
            if (ListeningState == ListeningState.Active)
            {
                ListeningState = ListeningState.Inactive;
                _timer.Stop();
            }
        }

        public Type InputMessageType
        {
            get { return typeof(DummyProtocolMessage); }
        }

        public event EventHandler<ProtocolMessageEventArgs<DummyProtocolMessage>> MessageReceived;
    }
}
