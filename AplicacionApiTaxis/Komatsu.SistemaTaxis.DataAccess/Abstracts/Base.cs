using System.Configuration;

namespace Komatsu.SistemaTaxis.DataAccess
{
    public abstract class Base
    {
        protected string ConexionSQL = ConfigurationManager.ConnectionStrings["bdTaxi"].ConnectionString;
    }
}
