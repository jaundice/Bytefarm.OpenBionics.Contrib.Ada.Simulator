using System;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Common;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.IO;
using System.Timers;

namespace Bytefarm.OpenBionics.Ada.Simulator.Mock.IO
{
    public class DummyMotorsInput : IInput<MotorsProtocol>
    {
        public ListeningState ListeningState { get; private set; }
        Timer _timer = new Timer();
        private ulong sequenceId;

        private double Extension = 0;
        private double MaxExtension = 30;
        private double MinExtension = 0;
        private double Step = 0.1;
        private int Direction = 1;

        public void StartListening()
        {
            if (ListeningState == ListeningState.Inactive)
            {
                ListeningState = ListeningState.Active;
                _timer.AutoReset = true;
                _timer.Interval = 0.001; //ms
                _timer.Elapsed += _timer_Elapsed;
                _timer.Start();
            }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            if (Extension > MaxExtension || Extension < MinExtension)
                Direction *= -1;

            Extension += Direction * Step;


            Motor[] motors = new[] {
                new Motor
                {
                    ExtensionLength=Extension
                },new Motor
                {
                    ExtensionLength=Extension
                },new Motor
                {
                    ExtensionLength=Extension
                },new Motor
                {
                    ExtensionLength=Extension
                },new Motor
                {
                    ExtensionLength=Extension
                }
            };

            OnTimerTick(new MotorsProtocol()
            {
                Sync0 = 7,
                Sync1 = 31,
                HandIdentifier = 1,
                SequenceId = ++sequenceId,

                Motors = motors
            });
        }

        private void OnTimerTick(MotorsProtocol dummyProtocolMessage)
        {
            MessageReceived?.Invoke(this, new ProtocolMessageEventArgs<MotorsProtocol>()
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
            get { return typeof(MotorsProtocol); }
        }

        public event EventHandler<ProtocolMessageEventArgs<MotorsProtocol>> MessageReceived;
    }
}
