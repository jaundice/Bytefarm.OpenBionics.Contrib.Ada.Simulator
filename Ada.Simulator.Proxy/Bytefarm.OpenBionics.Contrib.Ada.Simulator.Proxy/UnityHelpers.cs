using System;
using Microsoft.Practices.Unity;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.IO;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.Processing;
using Bytefarm.OpenBionics.Ada.Simulator.Mock.IO;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Common;

namespace Bytefarm.OpenBionics.Contrib.Ada.Simulator.Proxy
{
    public static class UnityHelpers
    {
        public static void RegisterTypes(IUnityContainer c)
        {
            //c.RegisterInstance(new DummyInput());
            //c.RegisterType<IProcessor, NullOpProcessor<DummyProtocolMessage>>(
            //    new ContainerControlledLifetimeManager());

            //c.Resolve<IInput>().StartListening

            var input = new DummyMotorsInput();
            c.RegisterInstance<IProcessor>(new NullOpProcessor<MotorsProtocol>(input));
            input.StartListening();
        }

        #region Unity Container

        private static readonly Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        #endregion
    }
}