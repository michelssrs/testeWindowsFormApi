using System.Web;
using System.Web.Http;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace teste.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}