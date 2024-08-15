using Serilog;
using System.IO;
using System.Web.Hosting;

namespace Data.Util
{
    public class Utilitario
    {
        public static string ServerPath(string relative)
        {
            return HostingEnvironment.MapPath(relative);
        }

        public static void Logger()
        {
            string rutaDestinoLog = ServerPath("~/Log");
            if (!Directory.Exists(rutaDestinoLog))
            {
                Directory.CreateDirectory(rutaDestinoLog);
            }

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Console()
               .WriteTo.File(ServerPath("~/Log/LogService.txt"))
               .CreateLogger();
        }
    }
}
