using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicProject.Models
{
   public class FlightAndTicketsViewModel
    {
        public Flight Flight { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
