using BusinessLogicProject;
using BusinessLogicProject.Models;
using BusinessLogicProject.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using BusinessLogicProject.BusinessLogicMethods;

namespace AirPanelFromVeryBegining.Controllers
{


    public class AdminController : Controller
    {

        FlightBusinessLogic flightBusinessLogic = new FlightBusinessLogic();
        TicketBuisnessLogic ticketBuisnessLogic = new TicketBuisnessLogic();
        DBInitializer dB = new DBInitializer();


        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public ActionResult FindFlight(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                Flight flight = flightBusinessLogic.FindFlight(id);
                if (flight != null)
                {
                    return View("EditFlight", flight);
                }
                else
                {
                    return HttpNotFound();
                }

            }
        }


        [Authorize]
        [HttpPost]
        public ActionResult EditFlight(Flight flight)
        {

            if (ModelState.IsValid)
            {
                string info;
                if (flightBusinessLogic.EditFlight(flight))
                {
                    info = "Зміни Внесено";
                    return RedirectToAction("InfoAndErrors", "Admin", new { info });
                }
                else
                {
                    info = "Помилка заповнення форми";
                    return RedirectToAction("InfoAndErrors", "Admin", new { info });

                }

            }
            else
            {
                return HttpNotFound();
            }

        }


        [Authorize]
        public ActionResult FindTickets(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                //List<Ticket> tickets = flightBusinessLogic.GetTickets(id);
                FlightAndTicketsViewModel viewModel = new FlightAndTicketsViewModel();
                viewModel.Flight = flightBusinessLogic.FindFlight(id);
                viewModel.Tickets = ticketBuisnessLogic.GetTickets(id);

                if (viewModel.Flight == null )
                {
                    return HttpNotFound();
                   
                }
                else
                {
                    return View("EditTickets", viewModel);
                }
            }
        }

       


        [Authorize]
        public ActionResult CreateTicketsInFlight()
        {
            SelectList list = new SelectList(dB.ArrayCities);
            ViewBag.myList = list;
            return View();
        }



        [Authorize]
        [HttpPost]
        public ActionResult CreateTicketsInFlight(CreateTicketsInFlight inFlight)
        {
            //if (ModelState.IsValid)// На фронтовій частині не відбувається валідація полів!!!
            if (inFlight != null)
            {
                string info;
                if (flightBusinessLogic.CreateFlight(inFlight))
                {
                    info = "Рейс Створено";
                    return RedirectToAction("InfoAndErrors", "Admin", new { info });
                }
                else
                {
                    info = "Помилка заповнення форми";
                    return RedirectToAction("InfoAndErrors", "Admin", new { info });
                }
            }
            else
            {
                return HttpNotFound();
            }
        }



        [Authorize]
        public ActionResult ShowAllFlights(int? page)
        {
            List<Flight> allFlights = new List<Flight>();
            allFlights = flightBusinessLogic.ShowAllFlights();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(allFlights.ToPagedList(pageNumber, pageSize));
        }




        [Authorize]
        public ActionResult DeleteTicket(int id, int? idTwo)
        {
            if (id == 0 || idTwo == null)
            {
                return HttpNotFound();
            }
            else
            {
                string info;
                if (ticketBuisnessLogic.DeleteTicket(id))
                {
                    return RedirectToAction("FindTickets", "Admin", new { id = idTwo });
                }
                info = "Помилка";
                return RedirectToAction("InfoAndErrors", "Admin", new { info });
            }
        }

        [Authorize]
        public ActionResult DeleteTickets(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                string info;
                if (ticketBuisnessLogic.DeleteTickets(id))
                {
                    return RedirectToAction("FindTickets", "Admin", new { id });
                }
                info = "Помилка";
                return RedirectToAction("InfoAndErrors", "Admin", new { info });
            }
        }

        [Authorize]
        public ActionResult DeleteFlight(int id)
        {
            if (id == 0)
            {
                return HttpNotFound();
            }
            else
            {
                string info;

                Flight fl = flightBusinessLogic.FindFlight(id);
                if (fl == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    info = fl.FlightNumber.ToString();
                    if (flightBusinessLogic.DeleteFlight(id))
                    {
                        info = "Рейс № " + info + " Видалено";
                        return RedirectToAction("InfoAndErrors", "Admin", new { info });
                    }
                    info = "Помилка Видалення";
                    return RedirectToAction("InfoAndErrors", "Admin", new { info });
                }
               
            }
        }

        [Authorize]
        public ActionResult CreateTickets(int id)
        {
            CreateTicketsViewModel createTicketsView = new CreateTicketsViewModel();
            createTicketsView.FlightId = id;
            return View("CreateTickets",createTicketsView);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateTickets(CreateTicketsViewModel inFlight)
        {
            string info;
            if (inFlight.Count > 0 && inFlight.Price > 0 && !string.IsNullOrEmpty(inFlight.ToClass.ToString()))
            {
                if (ticketBuisnessLogic.CreateTickets(inFlight))
                {
                  
                    return RedirectToAction("FindTickets", "Admin", new { id= inFlight.FlightId });
                }
                else
                {
                    info = "Помилка";
                    return RedirectToAction("InfoAndErrors", "Admin", new { info });
                }
            }
            else
            {
                info = "Помилка";
                return RedirectToAction("InfoAndErrors", "Admin", new { info });
            }
        }


        [Authorize]
        [HttpPost]
        public ActionResult EditTicket(Ticket tick)
        {

            return View();
        }


        [Authorize]
        public ActionResult InfoAndErrors(string info)
        {
            ViewBag.Message = info;
            return View();
        }
        [Authorize]
        public ActionResult ShowPassengers(int id)
        {
            List<Passenger> passangers = flightBusinessLogic.ShowPassengers(id);
            return View(passangers);
        }
    }
}