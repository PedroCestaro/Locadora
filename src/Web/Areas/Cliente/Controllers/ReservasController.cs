using Locadora.Domain;
using Locadora.Web.Areas.Admin.ViewModel;
using Locadora.Web.Helpers;
using Simple;
using Simple.Entities;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Web.Areas.Cliente.Controllers
{
    public partial class ReservasController : BaseController
    {
        public virtual ActionResult Index()
        {
            ViewBag.Pagina = 1;
            var cliente = (TClient)ViewBag.Usuario;
            var reservation = new ClientSearchViewModel(cliente.Name);
            var reserva = reservation.ConverToClientSearch();
            var buscarReservas = TClient.Search(reserva);
            var total = TClient.CountSearch(reserva);
            var page = new Page<TReservation>(buscarReservas, total);
            return View(page);
        }

        [RequiresAuthorization]
        public virtual ActionResult NovaReserva()
        {
            var reserva = new TReservation();
            reserva.Returned = false;
            ViewBag.Filme = TMovie.ListAll().ToSelectList(x=>x.Id, x=>x.Name);
            return View(reserva);
        }

        [HttpPost]
        public virtual ActionResult NovaReserva(TReservation reservation)
        {

            if (reservation.Movies == null)
            {
                TempData["Alerta"] = new Alert("error", "Escolha pelo menos um filme");
                return RedirectToAction("NovaReserva");
            }
            else{
                for (int i = 0; i <reservation.Quantity.Length; i++)
                {
                    if (reservation.Quantity[i] == null)
                    {
                        TempData["Alerta"] = new Alert("error", "Reveja as quantidades da reserva");
                        return RedirectToAction("NovaReserva");

                    }
                }
                var cliente = (TClient)ViewBag.Usuario;
                reservation.Client = cliente;
                reservation.Save();
                TIten.SaveItem(reservation);
                TempData["Alerta"] = new Alert("success", "Pedido de reserva cadastrado!");
                return RedirectToAction("Index");
            }

        }

        public virtual ActionResult ListarFilmes(int id)
        {
            var filme = TMovie.Load(id);
            return PartialView("_lista-de-filmes", filme);
        }

        public virtual ActionResult ListarReservas(ClientSearchViewModel reservation)
        {
            var cliente = (TClient)ViewBag.Usuario;
            reservation.name = cliente.Name;
            reservation.pagina = reservation.pagina ?? 1;
            ViewBag.Pagina = reservation.pagina;
            var reserva = reservation.ConverToClientSearch();
            var buscarReservas = TClient.Search(reserva);
            var total = TClient.CountSearch(reserva);
            var page = new Page<TReservation>(buscarReservas, total);
            return PartialView("_lista_de_reservas", page);
        }

    }
}