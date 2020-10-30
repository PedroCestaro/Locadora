using FluentNHibernate.Conventions;
using Locadora.Domain;
using Locadora.Helpers;
using Locadora.Web.Areas.Admin.ViewModel;
using Locadora.Web.Helpers;
using NPOI.HSSF.Record.Chart;
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
    public partial class FilmesController : BaseController
    {
        [RequiresAuthorization]
        public virtual ActionResult Index()
        {
            ViewBag.Pagina = 1;
            ViewBag.Categorias = TCategory.ListAll().ToSelectList(x=>x.Id, x=>x.Name);
            var filmes = new MovieSearchViewModel();
            var filme = filmes.ConveryToMovieSearch() ;
            var listaDeFilmes = TMovie.Search(filme);
            var total = TMovie.CountSearch(filme);
            var page = new Page<TMovie>(listaDeFilmes, total);

            return View(page);
        }
        

        [RequiresAuthorization]
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
                if(model.Categories != null)
                {
                    TMovieCategory.SaveCategories(model);//insert na relação
                    TempData["Alerta"] = new Alert("success", "Cadastro Realizado com sucesso");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Alerta"] = new Alert("success", "Cadastro Realizado com sucesso");
                    return RedirectToAction("Index");
                }
               
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.EnumFormatMovie = EnumHelper.ListAll<FormatMovie>().ToSelectList(x => x, x => x.Description());
                ViewBag.EnumTypeMovie = EnumHelper.ListAll<TypeMovie>().ToSelectList(x => x, x => x.Description());
                TempData["Alerta"] = new Alert("error", "Erro no cadastro");
                return HandleViewException(model, ex);
            }
        }

        [RequiresAuthorization]
        public virtual ActionResult Editar(int id)
        {
            var filme = TMovie.Load(id);
            var categoriasFilme = filme.TMovieCategories.Select(x => x.Category.Id).ToList();
            ViewBag.EnumFormatMovie = EnumHelper.ListAll<FormatMovie>().ToSelectList(x => x, x => x.Description());
            ViewBag.EnumTypeMovie = EnumHelper.ListAll<TypeMovie>().ToSelectList(x => x, x => x.Description());
            if (categoriasFilme.Count > 0)
                ViewBag.Category = TCategory.List(x => !categoriasFilme.Contains(x.Id)).ToSelectList(x => x.Id, x => x.Name);
            else
                ViewBag.Category = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);
            return View(filme);
        }

        [HttpPost]
        public virtual ActionResult Editar(TMovie model)
        {
            try
            {
                model.Update();
                TMovieCategory.SaveCategories(model);
                TempData["Alerta"] = new Alert("success", "Alteraçoes salvas");
                return RedirectToAction("Index");
            } catch (SimpleValidationException ex)
            {
                TempData["Alerta"] = new Alert("error", "Erro ao modificar" + ex);
                return RedirectToAction("Index");
            }
          
        }

        public virtual ActionResult ListarGenero(int id)
        {
            var genero = TCategory.Load(id);
            return PartialView("_listar-genero", genero);
        }

        [RequiresAuthorization]
        public virtual ActionResult Excluir(int id)
        {
            var filme = TMovie.Load(id);
            return PartialView("_excluir", filme);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id, object diff)
        {
            try
            {
                var itemExistente =TIten.Count(x => x.Movie.Id == id);
                if (itemExistente > 0)
                {
                    TempData["Alerta"] = new Alert("error", "Filme já está em reservas");
                    return RedirectToAction("Index");
                }
                else
                {
                    var categoriaExistente = TMovieCategory.Count(x => x.Movie.Id == id);
                    if(categoriaExistente > 0)
                    {
                        TMovieCategory.Delete(x => x.Movie.Id == id);
                        TMovie.Delete(id);
                        TempData["Alerta"] = new Alert("success", "Filme excluído!");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TMovie.Delete(id);
                        TempData["Alerta"] = new Alert("success", "Filme excluído!");
                        return RedirectToAction("Index");
                    }
                  
                }

            } catch(SimpleValidationException ex)
            {
                TempData["Alerta"] = new Alert("error", "Erro ao excluir"+ex);
                return RedirectToAction("Index");
            }
        }

        public virtual ActionResult ListarFilmes(MovieSearchViewModel movie)
        {
            movie.pagina = movie.pagina ?? 1;
            ViewBag.Pagina = movie.pagina;
            var filme = movie.ConveryToMovieSearch();
            var listaDeFilmes = TMovie.Search(filme);
            var total = TMovie.CountSearch(filme);
            var page = new Page<TMovie>(listaDeFilmes, total);
            return PartialView("_lista_de_filmes", page);
        }

    }
}