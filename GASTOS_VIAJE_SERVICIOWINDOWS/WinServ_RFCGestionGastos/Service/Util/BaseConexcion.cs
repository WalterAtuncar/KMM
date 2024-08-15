using System.Configuration;
using System.Web.Configuration;

namespace Service.Util
{
    public class BaseConexcion
    {
        public static string ConexionGasto { get { return  WebConfigurationManager.ConnectionStrings["bdTaxi"].ConnectionString; } }
    }
}
