using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RipOffAPI.Models
{
    public class RentalModel
    {
        public int Index { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate{ get; set; }
        public DateTime ReturnDate { get; set; }
        public int UserIndex { get; set; }
        public int CarIndex { get; set; }

        public RentalModel(
            int id,
            DateTime start,
            DateTime end,
            DateTime ret,
            int userindex,
            int carindex)
        {
            Index = id;
            StartDate = start;
            EndDate = end;
            ReturnDate = ret;
            UserIndex = userindex;
            CarIndex = carindex;
        }

    }
}