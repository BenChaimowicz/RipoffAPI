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
    public class CarTypesController : ApiController
    {
        private RipoffRentalsEntities db = new RipoffRentalsEntities();

        // GET: api/CarTypes
        [AllowAnonymous]
        public IHttpActionResult GetCarTypes()
        {
            var carTypes = new List<CarTypeModel>();
            foreach (var ct in db.CarTypes.ToList())
            {
                var cartype = new CarTypeModel(ct.index, ct.Manufacturer, ct.Model, ct.Model_Year, ct.Daily_Cost, ct.Daily_Late_Cost, ct.Transmission);
                carTypes.Add(cartype);
            }
            return Ok(carTypes);
        }

        // GET: api/CarTypes/5
        [AllowAnonymous]
        [ResponseType(typeof(CarTypeModel))]
        public IHttpActionResult GetCarType(int id)
        {
            CarType carType = db.CarTypes.Find(id);
            if (carType == null)
            {
                return NotFound();
            }

            var cartypeToReturn = new CarTypeModel(
                carType.index, carType.Manufacturer, carType.Model, carType.Model_Year, carType.Daily_Cost, carType.Daily_Late_Cost, carType.Transmission);


            return Ok(cartypeToReturn);
        }

        // PUT: api/CarTypes/5
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCarType(int id, CarType carType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carType.index)
            {
                return BadRequest();
            }

            db.Entry(carType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarTypeExists(id))
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

        // POST: api/CarTypes
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(CarType))]
        public IHttpActionResult PostCarType(CarType carType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CarTypes.Add(carType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CarTypeExists(carType.index))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = carType.index }, carType);
        }

        // DELETE: api/CarTypes/5
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(CarType))]
        public IHttpActionResult DeleteCarType(int id)
        {
            CarType carType = db.CarTypes.Find(id);
            if (carType == null)
            {
                return NotFound();
            }

            db.CarTypes.Remove(carType);
            db.SaveChanges();

            return Ok(carType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarTypeExists(int id)
        {
            return db.CarTypes.Count(e => e.index == id) > 0;
        }
    }
}