using BusinessLogicProject.BusinessLogicMethods;
using BusinessLogicProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace BusinessLogicProject
{
    /// <summary>
    /// Клас для організації методів пошуку даних в бд
    /// </summary>
    public class FlightBusinessLogic
    {
        AirpanelContext flightsContext = new AirpanelContext();

        public List<Flight> GetFlights(Flight flight)
        {
            IQueryable<Flight> flights = flightsContext.Flight;
            if (!String.IsNullOrEmpty(flight.DestinationCity) &&
                !String.IsNullOrEmpty(flight.OriginCity) &&
                flight.DateTimeOrigin != DateTime.MinValue)
            {
                flights = flightsContext.Flight.Where(e => e.DestinationCity.Contains(flight.DestinationCity) &&
                e.OriginCity.Contains(flight.OriginCity) && DbFunctions.TruncateTime(e.DateTimeOrigin) == DbFunctions.TruncateTime(flight.DateTimeOrigin));
                return flights.ToList();

            }

            else
            {

                return null;
            }

        }


        public Flight FindFlight(int? id)
        {

            var flight = flightsContext.Flight.Find(id);
            return flight;

        }


        public bool EditFlight(Flight flight)
        {
            //Підрахунок тривалості польоту
            TimeSpan span = new TimeSpan();
            span = flight.DateTimeDestination - flight.DateTimeOrigin;

            if (flight.DateTimeOrigin < DateTime.Now ||
                flight.DateTimeDestination < DateTime.Now ||
                span.TotalHours > 24)
            {
                return false;
            }

            flight.FlightDuration = span;
            flightsContext.Entry(flight).State = System.Data.Entity.EntityState.Modified;
            flightsContext.SaveChanges();
            return true;
        }
        public bool CreateFlight(CreateTicketsInFlight flight)
        {
            //Підрахунок тривалості польоту
            TimeSpan span = new TimeSpan();
            span = flight.Flight.DateTimeDestination - flight.Flight.DateTimeOrigin;

            if (flight.Flight.DateTimeOrigin < DateTime.Now ||
                flight.Flight.DateTimeDestination < DateTime.Now ||
                span.TotalHours > 24 || flight.Flight.DestinationCity == flight.Flight.OriginCity ||
                flight.Price < 1 || flight.Count < 1|| flight.Flight.FlightNumber < 1 || string.IsNullOrEmpty(flight.Flight.Airline)||
                flight.Flight.FlightNumber<999 || flight.Flight.FlightNumber > 9999)
            {
                return false;
            }

            List<Ticket> tickets = new List<Ticket>();
            for (int i = 0; i < flight.Count; i++)
            {
                Ticket ticket = new Ticket
                {
                    Clas = flight.ToClass.ToString(),
                    Price = flight.Price,
                    Status = null,
                    Flight = flight.Flight
                };
                tickets.Add(ticket);
            }


            flight.Flight.Tickets.AddRange(tickets);

            flight.Flight.FlightDuration = span;
            flightsContext.Flight.Add(flight.Flight);
            flightsContext.SaveChanges();
            return true;
        }



        public bool DeleteFlight(int flightId)
        {
            //flightsContext.Database.ExecuteSqlCommand("ALTER TABLE dbo.Tickets ADD CONSTRAINT Tickets_Flights FOREIGN KEY (FlightId) REFERENCES dbo.Flights (FlightId) ON DELETE SET NULL");

            TicketBuisnessLogic ticketBuisnessLogic = new TicketBuisnessLogic();
            Flight flightToDelet;

            if (ticketBuisnessLogic.GetTickets(flightId).Count > 0)
            {
                try
                {
                    ticketBuisnessLogic.DeleteTickets(flightId);
                }
                catch (Exception)
                {

                    return false;
                }

                flightsContext.SaveChanges();

                flightToDelet = flightsContext.Flight.First(e => e.FlightId == flightId);
                flightsContext.Flight.Remove(flightToDelet);
                flightsContext.SaveChanges();
                return true;
            }
            else
            {
                flightToDelet = flightsContext.Flight.First(e => e.FlightId == flightId);
                flightsContext.Flight.Remove(flightToDelet);
                flightsContext.SaveChanges();
                return true;
            }

        }



        public List<Flight> ShowAllFlights()
        {
            IQueryable<Flight> flights = flightsContext.Flight;
            return flights.ToList();
        }


        public List<Passenger> ShowPassengers(int flightId)
        {
            TicketBuisnessLogic ticketBuisnessLogic = new TicketBuisnessLogic();
            List<Ticket> tikcets = ticketBuisnessLogic.GetTickets(flightId);
            var ticketsForPassangers = tikcets.Where(e => e.Status == "sold").ToList();
            List<Passenger> pass = new List<Passenger>();

            for (int i = 0; i < ticketsForPassangers.Count; i++)
            {
                int j = ticketsForPassangers[i].TicketId;
                Passenger passenger = new Passenger();
                passenger = flightsContext.Passengers.First(e => e.TicketId == j);
                pass.Add(passenger);
            }
            return pass;

        }

    }
}