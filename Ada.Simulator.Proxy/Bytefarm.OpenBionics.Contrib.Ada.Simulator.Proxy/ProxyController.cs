using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.IO;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Lib.Processing;

namespace Bytefarm.OpenBionics.Contrib.Ada.Simulator.Proxy
{
    public class ProxyController : ApiController
    {
        [Dependency]
        public IProcessor Processor { get; set; }

        public object Get()
        {
            return new HttpResponseMessage()
            {
                Content = new ObjectContent(Processor.OutputMessageType , Processor.CurrentOutput, new JsonMediaTypeFormatter())
            };
        }

    }
}