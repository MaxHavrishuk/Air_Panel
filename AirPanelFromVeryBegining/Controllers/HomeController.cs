using BusinessLogicProject;
using BusinessLogicProject.BusinessLogicMethods;
using BusinessLogicProject.Models;
using BusinessLogicProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace AirPanelFromVeryBegining.Controllers
{

    public class HomeController : Controller
    {
        FlightBusinessLogic flightBusinessLogic = new FlightBusinessLogic();
        TicketBuisnessLogic ticketBuisnessLogic = new TicketBuisnessLogic();

        public ActionResult Index()
        {


            return View();
        }

        [HttpGet]
        public ActionResult Index(Flight flight)
        {
            if (flight.OriginCity != null && flight.DestinationCity != null && flight.DateTimeOrigin != DateTime.MinValue)
            {
                var fl = flightBusinessLogic.GetFlights(flight);
                for (int i = 0; i < fl.Count; i++)
                {
                    List<Ticket> tckts = ticketBuisnessLogic.GetTickets(fl[i].FlightId);
                    fl[i].Tickets = tckts.Where(e => e.Status == null).OrderByDescending(e => e.Price).ToList();
                }



                return View("Result", fl);
            }
            return View("Index");

        }
        public ActionResult Buy(int? Id)
        {
            BuyViewModel viewModel = new BuyViewModel();
            viewModel.Flight = flightBusinessLogic.FindFlight(Id);
            viewModel.Tickets = ticketBuisnessLogic.GetTickets(Id);
            if (viewModel.Tickets.Where(e => string.IsNullOrEmpty(e.Status)).Count() == 0)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }
        //Тест
        [HttpPost]
        public ActionResult BuyForm(BuyViewModel pass)
        {

            pass.Passenger.Sex = pass.Sex.ToString();
            if (ticketBuisnessLogic.BuyTicket(pass.Passenger, pass.Count, pass.Flight.FlightId, pass.ToClass.ToString()))
            {
                ViewBag.Message = "Покупку здійснено";
                ViewBag.Name = pass.Passenger.FirstName;
                return View("PurchaseSuccessful");
            }
            else
            {
                ViewBag.Message = "Помилка заповнення форми";
                return View("InfoAndErrors");
            }


        }
    }
}