using Locadora.Domain;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Locadora.Web.Helpers
{
    public class RequiresAuthorizationAttribute : ActionFilterAttribute, IAuthorizationFilter, IExceptionFilter
    {
    
       public RequiresAuthorizationAttribute()
        {
            
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ident = filterContext.HttpContext.Request.IsAuthenticated;
            //SecurityContext.Do.Init(()=> filterContext);
           

             var security = SecurityContext.Do;
            if (security == null)
            {
                NotLogged(filterContext);
                return;
            }
            if (!security.IsAuthenticated)
            {
                NotLogged(filterContext);
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        private static void NotLogged(ActionExecutingContext filterContext)
        {
            var rUrl = "";
            if (!string.IsNullOrWhiteSpace(filterContext.HttpContext.Request.QueryString["returnUrl"]))
                rUrl = filterContext.HttpContext.Request.QueryString["returnUrl"].Replace("/Web_deploy", "");
            else
                rUrl = filterContext.HttpContext.Request.Url.AbsoluteUri.Replace("/Web_deploy", "");

            if (rUrl.Contains("painel"))
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Login" }, { "Area", "Painel" }, { "returnUrl", rUrl } });
            else if (rUrl.Contains("pdv"))
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Login" }, { "Area", "Pdv" }, { "returnUrl", rUrl } });
            else if (rUrl.Contains("ev"))
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Login" }, { "Area", "Ev" }, { "returnUrl", rUrl } });
            else
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Clientes" }, { "Action", "Login" }, { "Area", "" }, { "returnUrl", rUrl } });
        }

        private static void NotAuthorizated(ActionExecutingContext filterContext)
        {
            filterContext.Result = NotAuthorizated(filterContext.HttpContext.Request.Url.AbsoluteUri.Replace("/Web_deploy", ""));
        }

        private static void NotAuthorizated(ExceptionContext filterContext)
        {
            filterContext.Result = NotAuthorizated(filterContext.HttpContext.Request.Url.AbsoluteUri.Replace("/Web_deploy", ""));
        }

        private static RedirectToRouteResult NotAuthorizated(string url)
        {
            if (url.Contains("Index"))
                return new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "acesso-negado" } });
            else
                return new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Clientes" }, { "Action", "Login" }});
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
        }

        #region IExceptionFilter Members

        public void OnException(ExceptionContext exceptionContext)
        {
            if (exceptionContext.Exception is AuthorizationException)
                NotAuthorizated(exceptionContext);
        }

        #endregion
    }
}