using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple.Web.Mvc;
using System.CodeDom.Compiler;

namespace Locadora.Web.Controllers
{
    public partial class HomeController : BaseController
    {
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult Realizar()
        {
            return View();
        }
    }
}
