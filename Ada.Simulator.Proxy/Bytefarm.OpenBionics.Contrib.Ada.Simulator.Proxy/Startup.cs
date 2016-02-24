using System.Web.Http;
using Owin;
using Unity.WebApi;

namespace Bytefarm.OpenBionics.Contrib.Ada.Simulator.Proxy
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration
            {
                DependencyResolver = new UnityDependencyResolver(
                    UnityHelpers.GetConfiguredContainer())
            };


            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}",
                new {action = "Get", controller = "ProxyController", id = RouteParameter.Optional});
            app.UseWebApi(config);
        }
    }
}