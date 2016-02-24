using System;
using System.Configuration;
using Microsoft.Owin.Hosting;
using Microsoft.Practices.Unity;

namespace Bytefarm.OpenBionics.Contrib.Ada.Simulator.Proxy
{
    internal class Program
    {
        private static readonly IUnityContainer _container = UnityHelpers.GetConfiguredContainer();

        private static void Main(string[] args)
        {
            var startup = _container.Resolve<Startup>();
            using (
                var webApplication = WebApp.Start(ConfigurationManager.AppSettings["Hosting.BaseServerUrl"],
                    startup.Configuration))
            {
                Console.WriteLine("Open Bionics Ada Simulator Server is running.");
                Console.WriteLine("Press any key to quit.");
                Console.ReadLine();
            }
        }
    }
}