using System.Web.Http;
using System.Globalization;

namespace teste.Api
{
    public static class WebApiConfig
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            log4net.Config.XmlConfigurator.Configure();
        }
    }

    public static class Cultures
    {
        public static readonly CultureInfo ctrInfo = CultureInfo.GetCultureInfo("pt-Br");
    }
}
