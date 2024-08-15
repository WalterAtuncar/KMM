using System.Web;

namespace WebApplication.AppCode
{
    public static class Util
    {
        public static string ServerPath(string relative)
        {
            return HttpContext.Current.Server.MapPath(relative);
        }
    }
}