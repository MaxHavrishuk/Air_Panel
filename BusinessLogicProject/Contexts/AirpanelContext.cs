using System.Data.Entity;

namespace BusinessLogicProject

{
    /// <summary>
    /// Контекст бази даних
    /// </summary>
    public class AirpanelContext : DbContext
    {
    
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Flight> Flight { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
      
    }
   
}