using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RipOffAPI.Models
{
    public class BranchModel
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public BranchModel(int id, string name, string address, double lat, double lang)
        {
            Index = id;
            Name = name;
            Address = address;
            Latitude = lat;
            Longitude = lang;
        }
    }
}