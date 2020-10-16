using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locadora.Web.Areas.Admin.Controllers
{
    public partial class TesteController : Controller
    {
        // GET: Admin/Teste
        public virtual ActionResult Index()
        {
            //var aa = TClient.List(x => x.Login.EndsWith("almeida"));
            //var aa2 = TReservation.List(x => x.Client.Login.Contains("matheus") && x.Client.Name.Contains("matheus"));

            ////Listar reservas dos clientes com nome que começam com a letra P
            //var clientesComecamComP = TReservation.List(x => x.Client.Name.StartsWith("P"));//

            ////Listar filmes que estão na categoria 3 que terminam com a letra 'O'
            //var filmesComecamComO = TMovieCategory.List(x => x.Category.Id == 3 && x.Movie.Name.EndsWith("o")).Select(x=>x.Movie);

            ////listar/selecionar o id das reservas que tenham o filme.id == 5
            //var filmesId_5 = TIten.List(x => x.Movie.Id == 5).Select(x=>x.Reservation.Id);

            ////excluir as reservas do filme id==10 e que o cliente.name contenha 'Joao'
            ////var ListaJoaoEId_10 = TIten.List(x => x.Movie.Id == 10 && x.Reservation.Client.Name.Contains("joao")).Select(x => x.Id);
            ////TIten.Delete(x => ListaJoaoEId_10.Contains(x.Id));
            ////TReservation.Delete(x => ListaJoaoEId_10.Contains(x.Id));

            //var reservasExcluir = TIten.List(x => x.Movie.Id == 10 && x.Reservation.Client.Name.Contains("joao")).Select(x => x.Reservation);
            //TIten.Delete(x => reservasExcluir.Select(y => y.Id).Contains(x.Reservation.Id));
            //TReservation.Delete(x => reservasExcluir.Select(y => y.Id).Contains(x.Id));//Não há como excluir apenas alguns itens da reserva

            ////listar/selecionar o nome das preferencias do cliente Id==18
            //var pref = TPreference.List(x => x.Client.Id == 18).Select(x => x.Category.Name);

            ////listar as categorias que não tenham filmes reservados
            //var reservados = TIten.ListAll().Select(x => x.Movie.Id);
            //var filmesNãoReservados = TMovieCategory.List(x => !reservados.Contains(x.Id)).Select(x => x.Category.Name); 

            ////listar os filmes da categoria 35 que possuam reservas
            //var categoriasDosFilmes = TMovieCategory.List(x => x.Category.Id == 35).Select(x => x.Movie.Id);
            //var filme = TIten.List(x => categoriasDosFilmes.Contains(x.Movie.Id)).Select(x => x.Movie.Name);

            ////excluir os clientes que NÃO fizeram reservas 
            //var clientecomReserva = TReservation.ListAll().Select(x => x.Client.Id);
            //TClient.Delete(x => !clientecomReserva.Contains(x.Id));


            ////Atualizar o nome de todos os filmes acrescentando '-novo' no final, onde category.id == 25
            //var filmeCategoria25 = TMovieCategory.List(x => x.Category.Id == 25).Select(x => x.Movie);
            ////var mov = TMovie.List(x => cate.Contains(x.Id)).Select(x => x.Name.Replace(Name, Name + "-novo"));
            //foreach (var item in filmeCategoria25)
            //{
            //    item.Name += " - NOVO";
            //    item.Update();
            //}

            ////atualizar as reservas do cliente id==46 acrescentando mais um dia na data de entrega 
            //var reservasCliente = TReservation.List(x => x.Client.Id == 46);
            //foreach (var item in reservasCliente)
            //{
            //    item.Devolution = item.Devolution.AddDays(1);
            //    item.Update();
            //}

            ////listar/selecionar as quantidades dos itens das reservas dos clientes que começam com a letar 'p'
            //TIten.List(x => x.Reservation.Client.Name.StartsWith("p")).Select(x => x.Quantity);
            //TIten.List(x => x.Reservation.Client.Name.StartsWith("p")).Sum(x => x.Quantity);

            ////excluir os itens das reservas que os filmes terminem com a letra m  e a data de retirada da reserva ja expirou
            //var excl = TIten.Delete(x => x.Movie.Name.EndsWith("m") && x.Reservation.Withdraw < DateTime.Now);
            return View();
        }
    }
}