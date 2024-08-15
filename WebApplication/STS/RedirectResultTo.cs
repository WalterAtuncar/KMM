using System.Web.Mvc;
using System.Web.Routing;

namespace Komatsu_SistemaSeguros.STS
{
    public class RedirectResultTo
    {
        public static ActionResult Redirect(RouteValueDictionary route, ActionExecutingContext context)
        {
            ActionResult result;

            if (context.HttpContext.Request.IsAjaxRequest())
            {
                UrlHelper url = new UrlHelper(context.RequestContext);

                result = new JavaScriptResult()
                {
                    Script = "window.location = '" + url.Action(null, route) + "';"
                };
            }
            else
            {
                result = new RedirectToRouteResult(route);
            }

            return result;
        }
    }
}