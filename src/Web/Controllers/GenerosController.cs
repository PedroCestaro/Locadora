using Locadora.Domain;
using Locadora.Helpers;
using Simple.Validation;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Web.Controllers
{
    public partial class GenerosController : BaseController
    {
        public virtual ActionResult Index()
        {//listando os generos
            var categoria = TCategory.ListAll().ToList();
            return View(categoria);
        }

        public virtual ActionResult Cadastrar()
        {//instanciando um novo obj
            var categoria = new TCategory();
            return View(categoria);
        }

        [HttpPost]
        public virtual ActionResult Cadastrar(TCategory model)
        {
            try
            { //salvando o obj
                model.Save();
                TempData["Alerta"] = new Alert("success", "Cadastro realizado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                TempData["Alerta"] = new Alert("error", "Erro no cadastro");
                return HandleViewException(model, ex);
            }
        }

        public virtual ActionResult Editar(int id)
        { //acessando id específico
            var categoria = TCategory.Load(id);
            return View(categoria);
        }

        [HttpPost]
        public virtual ActionResult Editar(TCategory model)
        {//update no banco
            model.Update();
            return RedirectToAction("Index");
        }

        public virtual ActionResult Excluir(int id)
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id, object diff)
        {
            return View();
        }

    }
}