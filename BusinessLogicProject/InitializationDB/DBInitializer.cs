using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicProject
{
    public class DBInitializer
    {
        /// <summary>
        /// Дані для заповнення бази даних
        /// </summary>

        public readonly string[] ArrayCities = { "Amsterdam","Athens", "Berlin", "Bern", "Warsaw", "Viena", "Dublin", "Gdansk", "L'viv", "London", "Madrid",
        "Minsk","Monaco","Paris","Prague","Riga","Rome","San-Marino", "Tallinn","Tirana","Helsinki", "Oslo", "Budapest","Brussels"};
        public DateTime[] ArrayDateTimes { set; get; } = { new DateTime(2018, 10, 20, 20, 30, 00), new DateTime(2018, 10, 21, 01, 10, 00) };
        public string[] Airlines { set; get; } = { "Turkish Airlines", "Pan American Airlines", "Air Arabia Airlines" };
        public string[] @Class { get; } = { "Buisness", "Econom" };
        //Поки не використовується
        //private string[] ArrayCityPort = new string[24];
        //public string[] ArrayStatus { get; } = { "CHECK-IN", "GATE CLOSED", "ARRIVED", "DEPARTED", "UNKNOWN", "CANCELED",
        //    "EXPECTED", "DELAYED", "IN FLIGHT" };
        //public string[] ArrayTerminal { get; } = { "A", "B", "C", "D", "E", "F", "G", "H" };
        //public int[] ArrayGates { get; } = { 1, 2, 3, 4, 5, 6, 7, 8 };


    }
}
