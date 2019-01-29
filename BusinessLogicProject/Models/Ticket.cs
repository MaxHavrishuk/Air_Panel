using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogicProject
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int? FlightId { get; set; }

        [ForeignKey("FlightId")]
        public Flight Flight { get; set; }
        [Required]
        [DisplayName("Клас")]
        public string Clas { get; set; }
        [Required]
        [DisplayName("Ціна")]
        public int Price { get; set; }
        [DisplayName("Статус")]
        public string Status { get; set; }

    }
}