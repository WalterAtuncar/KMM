using System.Web.Configuration;

namespace GASTO.Service.Util
{
    public class BaseConexcion
    {
        public static string ConexionGasto { get { return WebConfigurationManager.ConnectionStrings["bdTaxi"].ConnectionString; } }
    }
}
