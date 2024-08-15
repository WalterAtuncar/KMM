using System.Web.Configuration;

namespace Service.Util
{
    public class BaseConexcion
    {
        public static string ConexionTaxi { get { return WebConfigurationManager.ConnectionStrings["bdTaxi"].ConnectionString; } }
    }
}
