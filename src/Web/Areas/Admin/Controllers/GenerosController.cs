using FluentNHibernate.Conventions;
using Locadora.Domain;
using Locadora.Helpers;
using Locadora.Web.Areas.Admin.ViewModel;
using Locadora.Web.Helpers;
using Simple.Entities;
using Simple.Validation;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Web.Areas.Admin.Controllers
{
    public partial class GenerosController : BaseController
    {
        [RequiresAuthorization]
        public virtual ActionResult Index()
        {
            ViewBag.Pagina = 1;
            var categoria = new CategorySearchViewModel();
            var categorias = categoria.ConveryToCategorySearch();
            var listaDeCategorias = TCategory.Search(categorias);
            var total = TCategory.CountSearch(categorias);
            var page = new Page<TCategory>(listaDeCategorias, total);

            return View(page);
        }

        [RequiresAuthorization]
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

        [RequiresAuthorization]
        public virtual ActionResult Editar(int id)
        { //acessando id específico
            var categoria = TCategory.Load(id);
            return View(categoria);
        }

        [HttpPost]
        public virtual ActionResult Editar(TCategory model)
        {//update no banco
            try
            {
                model.Update();
                TempData["Alerta"] = new Alert("success", "Edições salvas");
            } catch (SimpleValidationException ex)
            {
                TempData["Alerta"] = new Alert("error", "Erro ao editar: " + ex);
            }

            return RedirectToAction("Index");
        }
        [RequiresAuthorization]
        public virtual ActionResult Excluir(int id)
        {
            var categoria = TCategory.Load(id);
            return PartialView("_excluir",categoria);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id, object diff)
        {
            try
            {
                var preferencia = TPreference.Count(x => x.Category.Id == id);
                var categoria = TMovieCategory.Count(x => x.Category.Id == id);

                if(preferencia >0 || categoria>0)
                {
                    TempData["Alerta"] = new Alert("error", "Gênero associado, impossível excluir");
                
                    return RedirectToAction("Index");
                }
                else
                {
                    TPreference.Delete(x => x.Category.Id == id);
                    TMovieCategory.Delete(x => x.Category.Id == id);
                    TCategory.Delete(id);
                    TempData["Alerta"] = new Alert("success", "Genero excluído com sucesso");
                    return RedirectToAction("Index");
                }        
            }
            catch (SimpleValidationException ex)
            {
                TempData["Alerta"] = new Alert("error", "Erro ao excluir: " + ex);
            }
            return RedirectToAction("Index");
        }

        public virtual ActionResult ListarGeneros(CategorySearchViewModel search)
        {
            search.pagina = search.pagina ?? 1;
            ViewBag.Pagina = search.pagina;
            var categorias = search.ConveryToCategorySearch();
            var listaDeCategorias = TCategory.Search(categorias);
            var total = TCategory.CountSearch(categorias);
            var page = new Page<TCategory>(listaDeCategorias, total);
            return PartialView("_lista_de_categorias", page);
        }

    }
}