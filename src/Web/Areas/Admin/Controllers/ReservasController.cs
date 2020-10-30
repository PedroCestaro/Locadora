using Locadora.Domain;
using Locadora.Web.Areas.Admin.ViewModel;
using Locadora.Web.Helpers;
using Simple.Entities;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Web.Areas.Admin.Controllers
{
    public partial class ReservasController : BaseController
    {
        [RequiresAuthorization]
        public virtual ActionResult Index()
        {
            ViewBag.Pagina = 1;
            ViewBag.Filmes = TMovie.ListAll().ToSelectList(x => x.Id, x => x.Name);
            var usuario = (TUser)ViewBag.Usuario;
            var reservation = new ReservationSearchViewModel(usuario.Id);
            var reserva = reservation.ConverToReservationSearch();
            var buscarReservas = TReservation.Search(reserva);
            var total = TReservation.CountSearch(reserva);
            var page = new Page<TReservation>(buscarReservas, total);

            return View(page);
        }
    

        [RequiresAuthorization]
        public virtual ActionResult NovaReserva()
        {
            var reserva = new TReservation();
            ViewBag.Filme = TMovie.ListAll().ToSelectList(x => x.Id, x => x.Name);
            ViewBag.Cliente = TClient.ListAll().ToSelectList(x => x.Id, x => x.Name);
            return View(reserva);

        }


        [HttpPost]
        public virtual ActionResult NovaReserva(TReservation reservation)
        {
            if (reservation.Clientes == null)
            {
                TempData["Alerta"] = new Alert("error", "Escolha o cliente");
                return RedirectToAction("NovaReserva");
            }
            else if (reservation.Movies == null)
            {
                TempData["Alerta"] = new Alert("error", "Escolha pelo menos um filme");
                return RedirectToAction("NovaReserva");
            }else
            {
                for (int i = 0; i < reservation.Clientes.Length; i++)
                {
                    reservation.Client = TClient.Load(reservation.Clientes[i]);
                }
                reservation.Save();
                TIten.SaveItem(reservation);
                TempData["Alerta"] = new Alert("success", "Pedido de reserva cadastrado!");
                return RedirectToAction("Index");
            }

        }

        public virtual ActionResult ListarReservas(ReservationSearchViewModel reservation)
        {
            reservation.pagina = reservation.pagina ?? 1;
            ViewBag.Pagina = reservation.pagina;
            var reserva = reservation.ConverToReservationSearch();
            var buscarReservas = TReservation.Search(reserva);
            var total = TReservation.CountSearch(reserva);
            var page = new Page<TReservation>(buscarReservas, total);
            return PartialView("_lista_de_reservas", page);
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
            return RedirectToAction("Index");
        }

        public virtual ActionResult AlterarDataDevolucao(int id)
        {
            var reservation = TReservation.Load(id);
            return PartialView("_editar_devolucao", reservation);
        }


        [HttpPost]
        public virtual ActionResult AlterarDataDevolucao(TReservation reservation)

        {
            var updatedReservation = TReservation.Load(reservation.Id);
            updatedReservation.Devolution = reservation.Devolution;
            updatedReservation.Update();
            TempData["Alerta"] = new Alert("success", "Reserva Confirmada");
            return RedirectToAction("Index");
        }

    }
}