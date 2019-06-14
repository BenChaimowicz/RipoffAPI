using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RipOffAPI.Models
{
    public class CarModel
    {
        public int Index { get; set; }
        public int CarTypeIndex { get; set; }
        public int Mileage { get; set; }
        public byte[] Image { get; set; }
        public bool FitForRental { get; set; }
        public bool Available { get; set; }
        public string PlateNumber { get; set; }
        public int Branch { get; set; }

        public CarModel(
            int id,
            int cartype,
            int mileage,
            byte[] image,
            bool fit,
            bool available,
            string platenumber,
            int branch)
        {
            Index = id;
            CarTypeIndex = cartype;
            Mileage = mileage;
            Image = image;
            FitForRental = fit;
            Available = available;
            PlateNumber = platenumber;
            Branch = branch;
        }
    }
}