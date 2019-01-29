using BusinessLogicProject.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicProject.Models.ViewModels
{
    public class CreateTicketsInFlight
    {

        public Flight Flight { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public ForClass ToClass { get; set; }

    }
    
   
 

}
