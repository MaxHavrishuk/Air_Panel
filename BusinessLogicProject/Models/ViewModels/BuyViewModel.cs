using BusinessLogicProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicProject.Models.ViewModels
{
    public class BuyViewModel
    {
        public Flight Flight { get; set; }
        public Passenger Passenger { get; set; }
        public List<Ticket> Tickets { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public ForClass ToClass { get; set; }
        [Required]
        public Sex Sex { get; set; }

    }
}
