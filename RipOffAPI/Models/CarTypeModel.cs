using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RipOffAPI.Models
{
    public class CarTypeModel
    {
        public int Index { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string ModelYear { get; set; }
        public int DailyCost { get; set; }
        public int DailyLateCost { get; set; }
        public string Transmission { get; set; }

        public CarTypeModel(
            int id,
            string manu,
            string model,
            string my,
            int dailycost,
            int dailylatecost,
            string transmission)
        {
            Index = id;
            Manufacturer = manu;
            Model = model;
            ModelYear = my;
            DailyCost = dailycost;
            DailyLateCost = dailylatecost;
            Transmission = transmission;
        }
    }
}