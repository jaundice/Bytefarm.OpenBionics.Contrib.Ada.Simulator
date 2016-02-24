using System;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.IO;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Protocol;

namespace Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.Processing
{
    public abstract class ProcessorBase<TInputProtocol, TOutputProtocol> : IProcessor<TInputProtocol, TOutputProtocol>
        where TInputProtocol : struct, IProtocolMessage where TOutputProtocol : struct, IProtocolMessage
    {
        protected ProcessorBase(IInput<TInputProtocol> input)
        {
            Input = input;
            Input.MessageReceived += Input_MessageReceived;
        }

        public IInput<TInputProtocol> Input { get; protected set; }

        IProtocolMessage IProcessor.CurrentInput
        {
            get { return CurrentInput; }
        }

        IProtocolMessage IProcessor.CurrentOutput
        {
            get { return CurrentOutput; }
        }

        object IProcessor.ProcessInput(object input)
        {
            return ProcessInput((TInputProtocol)input);
        }

        public Type InputMessageType { get { return typeof (TInputProtocol); } }

        public Type OutputMessageType
        {
            get { return typeof (TOutputProtocol); }
        }

        public TOutputProtocol CurrentOutput { get; protected set; }

        object IProcessor.Input
        {
            get { return Input; }
        }

        public TInputProtocol CurrentInput { get; protected set; }
        public abstract TOutputProtocol ProcessInput(TInputProtocol input);

        private void Input_MessageReceived(object sender, ProtocolMessageEventArgs<TInputProtocol> e)
        {
            var outPut = ProcessInput(e.Message);
            CurrentInput = e.Message;
            CurrentOutput = outPut;
        }
    }
}