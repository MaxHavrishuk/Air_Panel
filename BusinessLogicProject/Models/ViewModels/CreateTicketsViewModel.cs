using BusinessLogicProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicProject.Models.ViewModels
{
    public class CreateTicketsViewModel
    {
        public int FlightId { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public ForClass ToClass { get; set; }
    }
}
