using Locadora.Domain;
using Locadora.Helpers;
using NPOI.HSSF.Record.Chart;
using Simple.Validation;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Web.Controllers
{
    public partial class FilmesController : BaseController
    {
        public virtual ActionResult Index()
        {
            var filmes = TMovie.ListAll().ToList();
            return View(filmes);
        }

        public virtual ActionResult Cadastrar()
        {
            var filme = new TMovie();//chama novo obj
            ViewBag.EnumFormatMovie = EnumHelper.ListAll<FormatMovie>().ToSelectList(x => x, x => x.Description());//lista o enum
            ViewBag.EnumTypeMovie = EnumHelper.ListAll<TypeMovie>().ToSelectList(x => x, x => x.Description());//lista o enum
            ViewBag.Category = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);//envia dados para a Category 
            return View(filme);
        }

        [HttpPost]
        public virtual ActionResult Cadastrar(TMovie model)
        {
            try
            {
                model.Date = DateTime.Now.GetCurrent();
                model.Save();//insert no bd
                TMovieCategory.SaveCategories(model);//insert na relação
                TempData["Alerta"] = new Alert("success", "Cadastro Realizado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.EnumFormatMovie = EnumHelper.ListAll<FormatMovie>().ToSelectList(x => x, x => x.Description());
                ViewBag.EnumTypeMovie = EnumHelper.ListAll<TypeMovie>().ToSelectList(x => x, x => x.Description());
                TempData["Alerta"] = new Alert("error", "Erro no cadastro");
                return HandleViewException(model, ex);
            }
        }

        public virtual ActionResult Editar(int id)
        {
            var filme = TMovie.Load(id);
            var categoriasFilme = filme.TMovieCategories.Select(x => x.Category.Id).ToList();
            ViewBag.EnumFormatMovie = EnumHelper.ListAll<FormatMovie>().ToSelectList(x => x, x => x.Description());
            ViewBag.EnumTypeMovie = EnumHelper.ListAll<TypeMovie>().ToSelectList(x => x, x => x.Description());
            ViewBag.Category = TCategory.List(x => !categoriasFilme.Contains(x.Id)).ToSelectList(x => x.Id, x => x.Name);
            return View(filme);
        }

        [HttpPost]
        public virtual ActionResult Editar(TMovie model)
        {
            model.Update();
            TMovieCategory.SaveCategories(model);
            return RedirectToAction("Index");
        }

        public virtual ActionResult ListarGenero(int id)
        {
            var genero = TCategory.Load(id);
            return PartialView("_listar-genero", genero);
        }

        public virtual ActionResult Excluir(int id)
        {
            var filme = TMovie.Load(id);
            return PartialView("_excluir", filme);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id, object diff)
        {
            TMovieCategory.Delete(x => x.Movie.Id == id);
            TMovie.Delete(id);
            return RedirectToAction("Index");
        }
    }
}