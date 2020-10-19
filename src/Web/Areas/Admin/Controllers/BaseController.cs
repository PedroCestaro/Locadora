using Locadora.Domain;
using Simple.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Locadora.Web.Areas.Admin.Controllers
{
    public partial class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            //if (filterContext.HttpContext.IsCustomErrorEnabled && !(filterContext.Exception is AuthorizationException))
            //{
            //    filterContext.ExceptionHandled = true;
            //    var code = filterContext.Exception.GetType().Name;
            //    var message = filterContext.Exception.Message;
            //    var stackTrace = "";
            //    while (filterContext.Exception != null)
            //    {
            //        stackTrace += filterContext.Exception.GetType().Name + "\n" + filterContext.Exception.StackTrace + "\n\n";
            //        filterContext.Exception = filterContext.Exception.InnerException;
            //    }
            //    ViewBag.Code = code;
            //    ViewBag.Message = message;
            //    ViewBag.StackTrace = stackTrace;
            //    this.View("erro").ExecuteResult(this.ControllerContext);
            //}
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = TUser.FindByUsername(User.Identity.Name);//procuro o usuário pelo nome
            if (User.Identity.IsAuthenticated && user != null)//se o usuário estiver autenticado, cria a viewbag
            {
                ViewBag.Usuario = user;
            }
            else
            {
                FormsAuthentication.SignOut();
            }

            ViewBag.Alerta = TempData["Alerta"];
        }

        //public ActionResult Alert()
        //{
        //    string texo = "Cadastro realizado com sucesso!";
        //    TempData["Cadastro"] = texo;

        //}

       

        protected ActionResult HandleViewException<T>(T model, SimpleValidationException ex)
        {
            ModelState.Clear();
            foreach (var item in ex.Errors)
                ModelState.AddModelError(item.PropertyName, item.Message);
            return View(model);
        }
    }
}