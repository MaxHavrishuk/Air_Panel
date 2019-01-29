using BusinessLogicProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicProject.BusinessLogicMethods
{
    public class TicketBuisnessLogic
    {
        AirpanelContext flightsContext = new AirpanelContext();



        public bool DeleteTicket(int ticketId)
        {

            try
            {
                Ticket ticketToDelet = flightsContext.Tickets.First(e => e.TicketId == ticketId);
                flightsContext.Tickets.Remove(ticketToDelet);
            }
            catch (Exception)
            {

                return false;
            }

            flightsContext.SaveChanges();
            return true;
        }


        public bool DeleteTickets(int? flightId)
        {
            List<Ticket> ticketsToDelet = flightsContext.Tickets.Where(e => e.FlightId == flightId).ToList();
            if (ticketsToDelet.Count == 0)
            {
                return false;
            }
            else
            {
                try
                {

                    flightsContext.Tickets.RemoveRange(ticketsToDelet);
                }
                catch (Exception)
                {

                    return false;
                }
                flightsContext.SaveChanges();
                return true;
            }

        }

        //public bool EditTicket(int ticketId)
        //{

        //    try
        //    {
        //        Ticket ticketToEdit = flightsContext.Tickets.First(e => e.TicketId == ticketId);

        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }

        //    flightsContext.SaveChanges();
        //    return true;

        //}

        public List<Ticket> GetTickets(int? id)
        {
            var tickets = flightsContext.Tickets.Where(e => e.FlightId == id).ToList();
            return tickets;
        }

        public bool CreateTickets(CreateTicketsViewModel createTickets)
        {
            List<Ticket> tickets = new List<Ticket>();
            FlightBusinessLogic flightBusinessLogic = new FlightBusinessLogic();
            Flight flight = flightsContext.Flight
                .Where(e => e.FlightId == createTickets.FlightId)
                .FirstOrDefault();



            for (int i = 0; i < createTickets.Count; i++)
            {
                Ticket ticket = new Ticket
                {
                    Clas = createTickets.ToClass.ToString(),
                    Price = createTickets.Price,
                    Status = null,
                    Flight = flight
                };
                tickets.Add(ticket);
            }


            flightsContext.Tickets.AddRange(tickets);
            flightsContext.SaveChanges();
            return true;
        }
        //Тест 
        public bool BuyTicket(Passenger passenger, int countTickets, int flightId, string classTickets)
        {
          
            TicketBuisnessLogic ticketBuisnessLogic = new TicketBuisnessLogic();
            IEnumerable<Ticket> tickets = ticketBuisnessLogic.GetTickets(flightId).Where(e => e.Clas == classTickets && e.Status == null);
            if (tickets.Count() > 0 && tickets.Count() >= countTickets)
            {
           
                List<Ticket> choosedTickets = tickets.ToList();
                for (int i = 0; i < countTickets; i++)
                {

                    Passenger pass = new Passenger();
                    pass = passenger;
                    pass.TicketId = choosedTickets[i].TicketId;
                    pass.PurchaseDate = DateTime.Now;
                   
                    flightsContext.Passengers.Add(pass);
                    flightsContext.SaveChanges();
                    Ticket ticket = flightsContext.Tickets.Where(e => e.TicketId == pass.TicketId)
                        .FirstOrDefault();
                    ticket.Status = "sold";
                    flightsContext.SaveChanges();
                  
                }
                return true;
            }
            else
            {
                return false;
            }


            
            
        }

    }
}
