using System.Configuration;

namespace GASTO.DataAccess.Abstracts
{
    public abstract class BaseGASTO
    {
        protected string ConexionSQL = ConfigurationManager.ConnectionStrings["bdTaxi"].ConnectionString;
    }
}
