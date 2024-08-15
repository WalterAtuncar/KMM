using System.Web.Configuration;

namespace GASTO.DataAccess.Abstracts
{
    public class BaseConexcion
    {
        public static string ConexionTaxi { get { return WebConfigurationManager.ConnectionStrings["bdTaxi"].ConnectionString; } }
    }
}
