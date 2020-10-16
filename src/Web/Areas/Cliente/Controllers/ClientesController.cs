using Locadora.Domain;
using Locadora.Helpers;
using Locadora.Web.Helpers;
using Microsoft.Ajax.Utilities;
using NPOI.OpenXmlFormats.Dml;
using Simple.Validation;
using Simple.Web.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using WebGrease;

namespace Locadora.Web.Areas.Cliente.Controllers
{
   
    public partial class ClientesController : BaseController
    {        
        public virtual ActionResult Index()
        {
            var clientes = TClient.ListAll().ToList();//chamando todos os objs Tclient           
            return View(clientes);
        }

        public virtual ActionResult Cadastrar()
        {
            var cliente = new TClient();//instancio um novo obj
            ViewBag.MostrarSenha = true;
            ViewBag.EnumprofileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());//pego o value
            ViewBag.Category = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);
            return View(cliente);
        }

        [HttpPost]
        public virtual ActionResult Cadastrar(TClient model)//backend
        {
            try
            {
                model.Password = TClient.HashPassword(model.PasswordString);
                model.Save();//insert no bd
                TPreference.SavePreferences(model);
                TempData["Alerta"] = new Alert("success", "Cadastro realizado com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                TempData["Alerta"] = new Alert("error", "Cadastro não realizado");
                ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
                return HandleViewException(model, ex);
            }
        }
       

        public virtual ActionResult Editar(int id)
        {
            var cliente = TClient.Load(id);//carrega o obj on Client.id == client.id
            ViewBag.MostrarSenha = false;//barra a senha
            ViewBag.MostrarCategoria = true;
            ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());//recebe os valores digitados
            ViewBag.Category = TCategory.ListAll().ToSelectList(x => x.Id, x => x.Name);
            ViewBag.Preferences = TPreference.ListAll().Where(x => x.Client.Id == id);
            return View(cliente);
        }


        [HttpPost]
        public virtual ActionResult Editar(TClient model)
        {
            try {
                model.Edit();//chama o método feito na classe
                TPreference.SavePreferences(model);
                TempData["Alerta"] = new Alert("success", "Alterações realizadas com sucesso");
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.MostrarSenha = false;
                ViewBag.EnumProfileClient = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
                return HandleViewException(model, ex);
            }
        }


        public virtual ActionResult Excluir(int id)
        {
            var cliente = TClient.Load(id);
            return PartialView("_excluir", cliente);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id, object diff)
        {
            TReservation.Delete(x => x.Client.Id == id);
            TPreference.Delete(x => x.Client.Id == id);
            TClient.Delete(id);
            return RedirectToAction("Index");
        }

  
        public virtual ActionResult MinhaConta()
        {
            var client = (TClient)ViewBag.Usuario;//pegando o usuário logado
            //A estrutura pode ser feita no model server
            var reservas = TReservation.List(x => x.Client.Id == client.Id);
            var preferences = client.TPreferences.Select(x => x.Category.Id).ToList();//Carregando o ID de categorias atrelados a prefenrecia do Client.Select funciona apenas com List, Load carrega apenas 1 obj, não a lista//Carrego o nome dos filmes pelo ID de Categoria
            var movieCategory = TMovieCategory.List(x => preferences.Contains(x.Category.Id)).ToList();//percorro a lista de preferencias de acorodo com o Id passado                                                          
            if (movieCategory.Count != 0)
            {
                return View(movieCategory);//passando a lista para a view, cuja esta esperando essa lista.
            }

            else
            {
                return View(TMovieCategory.ListAll());
            }
                
        }

       
        public virtual ActionResult Login()
        { 
            return View();
        }

        [HttpPost]
        public virtual ActionResult Login(Login login)
        { 
            var user = TClient.Authenticate(login);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(login.User,login.Remember);
                HttpContext.User = new GenericPrincipal(new GenericIdentity(user.Login), new string[] { });
                if (!string.IsNullOrWhiteSpace("Login"))
                    TempData["Alerta"] = new Alert("success", "Bem Vindo "+login.User);
                    return Redirect("MinhaConta");
            }
            else
            {
                
                ViewBag.Alerta = new Alert("error","Usuário não encontrado");
                return RedirectToAction("Login");
            }

        }

        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(MVC.Cliente.Clientes.Login());
        }

        public virtual ActionResult ListarGenero(int id)
        {
            var genero = TCategory.Load(id);
            return PartialView("_listar-genero", genero);
        }

        public virtual ActionResult ListarCategoria(int id)
        {
            var categoria = TClient.Load(id).Preference;
            return PartialView("listar-categoria", categoria);
        }

       public virtual ActionResult ListarFilmes(int id)
        {
            var filme = TMovie.Load(id);
            return PartialView("_lista-de-filmes", filme);
        }


        public virtual ActionResult Reservas()
        {
            var reserva = new TReservation();
            ViewBag.Filme = TMovie.ListAll().ToSelectList(x => x.Id, x => x.Name);
            return View(reserva);
        }

        [HttpPost]
        public virtual ActionResult Reservas(TReservation reservation)
        {
            var cliente = (TClient)ViewBag.Usuario;
            reservation.Client = cliente;
            reservation.Save();
            TIten.SaveItem(reservation);
            TempData["Alerta"] = new Alert("success", "Pedido de reserva cadastrado!");
            return RedirectToAction("MinhaConta");
        }


        public virtual ActionResult ListarReservas()
        {
            var cliente = (TClient)ViewBag.Usuario;
            var reservas =  TIten.List(x => x.Reservation.Client.Id == cliente.Id);
            return PartialView("_minhas_reservas", reservas);
        }

    }
}