using BusinessLogicProject.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace BusinessLogicProject.InitializationDB
{
    /// <summary>
    /// Ініціалізація бд рандомними данними
    /// </summary>
    /// 

    public class FlightDbInitializer : DropCreateDatabaseAlways<AirpanelContext>
    {


        DBInitializer initialize = new DBInitializer();

        Random random = new Random();
        private string destCity;
        private string originCity;
        private DateTime timeOrigin;
        private DateTime timeDestination;


        TimeSpan duration;
        protected override void Seed(AirpanelContext flightContext)
        {


            for (int i = 0; i < 100; i++)
            {
                //Ініціалізація рандомом полів Польоту
                destCity = initialize.ArrayCities[random.Next(0, 23)];
                originCity = initialize.ArrayCities[random.Next(0, 23)];
                timeDestination = initialize.ArrayDateTimes[1];
                timeOrigin = initialize.ArrayDateTimes[0];
                duration = timeDestination - timeOrigin;

                //Щоб Відтправлення відрізнялось від Прибуття
                while (destCity == originCity)
                {
                    originCity = initialize.ArrayCities[random.Next(0, 10)];
                }
               //Заповнення полів класу Політ рандомом
                Flight flight = new Flight
                {
                    DestinationCity = destCity,
                    OriginCity = originCity,
                    DateTimeDestination = timeDestination,
                    DateTimeOrigin = timeOrigin,
                    FlightDuration = duration,
                    FlightNumber = random.Next(1000, 9999),
                    Airline = initialize.Airlines[random.Next(0, 3)],

                };

                //Створення списку квитків для польоту
                List<Ticket> tickets = new List<Ticket>();
                //Цикл для додавання квитків в кожен політ
                for (int j = 0; j < 15; j++)
                {
                    Ticket ticket = new Ticket {
                        Clas = initialize.Class[1],
                        Price = 100,
                        Status = null,
                        Flight = flight };
                  
                    tickets.Add(ticket);
                
                }
                
                flightContext.Flight.Add(flight);
                flightContext.Tickets.AddRange(new List<Ticket>(tickets));
                for (int j = 0; j < 15; j++)
                {
                    Ticket ticket = new Ticket
                    {
                        Clas = initialize.Class[0],
                        Price = 250,
                        Status = null,
                        Flight = flight
                    };

                    tickets.Add(ticket);

                }
                flightContext.Flight.Add(flight);
                flightContext.Tickets.AddRange(new List<Ticket>(tickets));

            }
            flightContext.SaveChanges();
            base.Seed(flightContext);
        }

    }
    //public class UserInitializer : DropCreateDatabaseAlways<UserContext>
    //{

    //}

}
