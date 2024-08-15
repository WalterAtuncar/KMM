using System.Configuration;
using System.Web.Configuration;

namespace Servicio.Util
{
    public class BaseConexion
    {
        public static string ConexionTaxi{ get {return WebConfigurationManager.ConnectionStrings["bdTaxi"].ConnectionString; }}
    }
}
