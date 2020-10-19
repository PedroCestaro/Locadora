using Locadora.Domain;
using Locadora.Web.Areas.Admin.ViewModel;
using Locadora.Web.Helpers;
using NHibernate.Criterion;
using NPOI.SS.Formula.Atp;
using Simple.Validation;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Locadora.Web.Areas.Admin.Controllers
{
    public partial class UsuariosController : BaseController
    {
        // GET: User
        [RequiresAuthorization]
        public virtual ActionResult Index()
        {
            var users = TUser.ListAll().ToList();
            return View(users);
        }

        [RequiresAuthorization]
        public virtual ActionResult Cadastrar()
        {
            var user = new TUser();
            ViewBag.MostrarSenha = true;
            ViewBag.EnumProfileUser = EnumHelper.ListAll<ProfileUser>().ToSelectList(x => x, x => x.Description());
            return View(user);
        }

        [HttpPost]
        public virtual ActionResult Cadastrar(TUser model)
        {
            try
            {
                model.Password = TUser.HashPassword(model.PasswordString);
                model.Save();
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.EnumProfileUser = EnumHelper.ListAll<ProfileClient>().ToSelectList(x => x, x => x.Description());
                return HandleViewException(model, ex);
            }
        }

        [RequiresAuthorization]
        public virtual ActionResult Editar(int id)
        {
            var user = TUser.Load(id);
            ViewBag.MostrarSenha = false;
            ViewBag.EnumProfileUser = EnumHelper.ListAll<ProfileUser>().ToSelectList(x => x, x => x.Description());
            return View(user);
        }

        [HttpPost]
        public virtual ActionResult Editar(TUser model)
        {
            try
            {
                model.Editar();//chama o método feito na classe
                return RedirectToAction("Index");
            }
            catch (SimpleValidationException ex)
            {
                ViewBag.MostrarSenha = false;
                ViewBag.EnumProfileUser = EnumHelper.ListAll<ProfileUser>().ToSelectList(x => x, x => x.Description());
                return HandleViewException(model, ex);
            }
        }

        public virtual ActionResult Excluir(int id)
        {
            var user = TUser.Load(id);
            return PartialView("_excluir", user);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id, object diff)
        {
            TUser.Delete(id);
            return RedirectToAction("Index");
        }
        public virtual ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Login(Login login)
        {
            var user = TUser.Authenticate(login);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(login.User, login.Remember);
                HttpContext.User = new GenericPrincipal(new GenericIdentity(user.Login), new string[] { });
                if (!string.IsNullOrWhiteSpace("Login"))
                    TempData["Alerta"] = new Alert("success", "Bem Vindo " + login.User);
                return Redirect("PainelFuncionario");
            }
            else
            {
                ViewBag.Alerta = new Alert("error", "Usuário não encontrado");
                return RedirectToAction("Login");
            }
        }

        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(MVC.Admin.Usuarios.Login());
        }

        [RequiresAuthorization]
        public virtual ActionResult PainelFuncionario()
        {
            return View();
        }

        public virtual ActionResult ListarFilmes(int id)
        {
            var filme = TMovie.Load(id);
            return PartialView("_lista-de-filmes", filme);
        }

        public virtual ActionResult ListarClientes(int id)
        {
            var cliente = TClient.Load(id);
            return PartialView("_lista-de-clientes", cliente);
        }

        [RequiresAuthorization]
        public virtual ActionResult Reservas()
        {
            var reserva = new TReservation();
            ViewBag.Filme = TMovie.ListAll().ToSelectList(x => x.Id, x => x.Name);
            ViewBag.Cliente = TClient.ListAll().ToSelectList(x => x.Id, x => x.Name);
            return View(reserva);

        }


        [HttpPost]
        public virtual ActionResult Reservas(TReservation reservation)
        {
             for(int i = 0; i < reservation.Clientes.Length; i++)
            {
               reservation.Client = TClient.Load(reservation.Clientes[i]);
            }
            reservation.Save();
            TIten.SaveItem(reservation);
            TempData["Alerta"] = new Alert("success", "Pedido de reserva cadastrado!");
            return RedirectToAction("PainelFuncionario");
        }

        [RequiresAuthorization]
        public virtual ActionResult ReservasDeClientes()
        {
            ViewBag.Filmes = TMovie.ListAll().ToSelectList(x => x.Id, x => x.Name);
            //ViewBag.Status = TReservation.ListAll().ToSelectList(x => x, x => x.Returned);
            var reservas = TReservation.ListAll();
            return View(reservas);
        }

        public virtual ActionResult AlterarDataDevolucao(int id)
        {
            var reservation = TReservation.Load(id);
            return PartialView("_editar_devolucao", reservation);
        }


        [HttpPost]
        public virtual ActionResult AlterarDataDevolucao (TReservation reservation)
        
        {
            var updatedReservation = TReservation.Load(reservation.Id);
            updatedReservation.Devolution = reservation.Devolution;
            updatedReservation.Update();
            TempData["Alerta"] = new Alert("success", "Reserva Confirmada");
            return RedirectToAction("ReservasDeClientes");
        }

        public virtual ActionResult ListarReservas(ReservationSearchViewModel reservation)
        {
            var reserva = reservation.ConverToReservationSearch();
            var buscarReservas = TReservation.SearchCriteria(reserva);
            
            return PartialView("_lista_de_reservas",buscarReservas);
        }


        public virtual ActionResult FinalizarReservas(int id)
        {
            var reservation = TReservation.Load(id);
            return PartialView("_finalizar_reserva", reservation);
        }


        [HttpPost]
        public virtual ActionResult FinalizarReservas(TReservation reservation)

        {
            var updatedReservation = TReservation.Load(reservation.Id);
            updatedReservation.Returned = reservation.Returned;
            updatedReservation.Returned = true;
            updatedReservation.Update();
            TempData["Alerta"] = new Alert("success", "Devolução Confirmada");
            return RedirectToAction("ReservasDeClientes");
        }
    }

}