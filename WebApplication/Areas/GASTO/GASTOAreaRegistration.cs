using System.Web.Mvc;

namespace WebApplication.Areas.GASTO
{
    public class GASTOAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GASTO";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GASTO_default",
                "GASTO/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "WebApplication.Areas.GASTO.Controllers" }
            );
        }
    }
}