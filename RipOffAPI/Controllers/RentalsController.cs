using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RipOffAPI;
using RipOffAPI.Models;

namespace RipOffAPI.Controllers
{
    [Authorize]
    public class RentalsController : ApiController
    {
        private RipoffRentalsEntities db = new RipoffRentalsEntities();

        // GET: api/Rentals
        public IHttpActionResult GetRentals()
        {
            var rentals = new List<RentalModel>();
            foreach (var r in db.Rentals.ToList())
            {
                var rental = new RentalModel(
                    r.Index,
                    (DateTime)r.Start_Date,
                    (DateTime)r.End_Date,
                    (DateTime)r.Return_Date,
                    r.User_Index,
                    r.Car_Index);
                rentals.Add(rental);
            }
            return Ok(rentals);
        }

        // GET: api/Rentals/5
        [ResponseType(typeof(RentalModel))]
        public IHttpActionResult GetRental(int id)
        {
            Rental r = db.Rentals.Find(id);
            if (r == null)
            {
                return NotFound();
            }

            var rental = new RentalModel(
                    r.Index,
                    (DateTime)r.Start_Date,
                    (DateTime)r.End_Date,
                    (DateTime)r.Return_Date,
                    r.User_Index,
                    r.Car_Index);

            return Ok(rental);
        }

        // PUT: api/Rentals/5
        [Authorize(Roles = "Admin, Employee")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRental(int id, Rental rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rental.Index)
            {
                return BadRequest();
            }

            db.Entry(rental).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rentals
        [Authorize(Roles = "Admin, Employee")]
        [ResponseType(typeof(Rental))]
        public IHttpActionResult PostRental(Rental rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rentals.Add(rental);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RentalExists(rental.Index))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = rental.Index }, rental);
        }

        // DELETE: api/Rentals/5
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Rental))]
        public IHttpActionResult DeleteRental(int id)
        {
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return NotFound();
            }

            db.Rentals.Remove(rental);
            db.SaveChanges();

            return Ok(rental);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RentalExists(int id)
        {
            return db.Rentals.Count(e => e.Index == id) > 0;
        }
    }
}