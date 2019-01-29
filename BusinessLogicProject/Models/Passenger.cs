using BusinessLogicProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BusinessLogicProject
{
    public class Passenger
    {       
        public int PassengerId { get; set; }
        [DisplayName("Id")]
        public int TicketId { get; set; }

        [DisplayName("Ім'я")]
        public string FirstName { get; set; }
        [DisplayName("Прізвище")]
        public string LastName { get; set; }
        [DisplayName("Стать")]
        public string Sex { get; set; }
        [DisplayName("Дата Народження")]
        public DateTime Birthday { get; set; }
        [DisplayName("Дата покупки")]
        public DateTime PurchaseDate { get; set; }

    }
}