using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicProject
{
    /// <summary>
    /// Модель польоту
    /// </summary>

    public class Flight
    {
    
        [Key]
        public int FlightId { get; set; }
        [Required(ErrorMessage = "Заповніть це поле")]
        public string DestinationCity { get; set; }

        [Required(ErrorMessage = "Заповніть це поле")]
        public string OriginCity { get; set; }

        [Required(ErrorMessage = "Заповніть це поле")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTimeDestination { get; set; }

        [Required(ErrorMessage = "Заповніть це поле")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTimeOrigin { get; set; }
      
        public TimeSpan FlightDuration { get; set; }
        //Для обробки дати більше 24:00
        //public long FlightDuration { get; set; }
        //[NotMapped]
        //[Required]
        //public TimeSpan Duration
        //{
        //    get
        //    {
        //        return new TimeSpan(FlightDuration);
        //    }
        //    set
        //    {
        //        FlightDuration = value.Ticks;
        //    }

        [Required]
        [Range(1000,9999,ErrorMessage = "№ Рейса повинен бути від 1000-9999")]
        public int FlightNumber { get; set; }
        [Required(ErrorMessage = "Заповніть це поле")]
        public string Airline { get; set; }

        public List<Ticket> Tickets { get; set; } 
        public Flight()
        {
            Tickets = new List<Ticket>();
        }
    }
}