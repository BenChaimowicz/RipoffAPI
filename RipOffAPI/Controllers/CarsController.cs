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
using RipOffAPI.Managers;

namespace RipOffAPI.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class CarsController : ApiController
    {
        private RipoffRentalsEntities db = new RipoffRentalsEntities();

        // GET: api/Cars
        [AllowAnonymous]
        public IHttpActionResult GetCars()
        {
            var cars = new List<CarModel>();
            foreach (var c in db.Cars.ToList())
            {
                
                var car = new CarModel(c.Index, c.Car_Type_Index, c.Mileage, c.Image , c.Fit_For_Rental, c.Available, c.Plate_Number, c.Branch);
                cars.Add(car);
            }
            return Ok(cars);
        }

        // GET: api/Cars/5
        [ResponseType(typeof(CarModel))]
        public IHttpActionResult GetCar(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            var carToReturn = new CarModel(
                car.Index,
                car.Car_Type_Index,
                car.Mileage,
                car.Image,
                car.Fit_For_Rental,
                car.Available,
                car.Plate_Number,
                car.Branch
                );

            return Ok(carToReturn);
        }

        // PUT: api/Cars/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCar(int id, Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != car.Index)
            {
                return BadRequest();
            }

            db.Entry(car).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Cars
        [ResponseType(typeof(Car))]
        public IHttpActionResult PostCar(Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cars.Add(car);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CarExists(car.Index))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = car.Index }, car);
        }

        // DELETE: api/Cars/5
        [ResponseType(typeof(Car))]
        public IHttpActionResult DeleteCar(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            db.Cars.Remove(car);
            db.SaveChanges();

            return Ok(car);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarExists(int id)
        {
            return db.Cars.Count(e => e.Index == id) > 0;
        }
    }
}