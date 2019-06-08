using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using RipOffAPI;
using RipOffAPI.Models;

namespace RipOffAPI.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class UsersController : ApiController
    {
        private RipoffRentalsEntities db = new RipoffRentalsEntities();

        // GET: api/Users

        public IHttpActionResult GetUsers()
        {
            var users = new List<UserModel>();
            foreach (var u in db.Users.ToList())
            {
                var user = new UserModel(
                    u.uid, u.Full_Name, u.ID_Number, u.User_Name, u.Gender, u.Email, u.Permissions, u.Image, u.Date_Of_Birth.ToString());
                users.Add(user);
            }
            return Ok(users);
        }

        // GET: api/Users/5
        [Authorize(Roles = "Admin, Employee, Registered")]
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUser(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            int iuid = Convert.ToInt32(identity.FindFirst("user_id").Value);
            string irole = identity.FindFirst(ClaimTypes.Role).Value;

            if (id != iuid && irole != "Admin")
            {
                return Unauthorized();
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            var userToReturn = new UserModel(user.uid, user.Full_Name, user.ID_Number, user.User_Name, user.Gender, user.Email, user.Permissions, user.Image, user.Date_Of_Birth.ToString());

            return Ok(userToReturn);
        }

        // PUT: api/Users/5
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.uid)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [AllowAnonymous]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.uid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.uid }, user);
        }

        // DELETE: api/Users/5
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.uid == id) > 0;
        }
    }
}