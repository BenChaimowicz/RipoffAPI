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
    public class BranchesController : ApiController
    {
        private RipoffRentalsEntities db = new RipoffRentalsEntities();

        // GET: api/Branches
        public IHttpActionResult GetBranches()
        {
            var branches = new List<BranchModel>();
            foreach (var b in db.Branches.ToList())
            {
                var branch = new BranchModel(b.Index, b.Name, b.Address, b.Latitude, b.Longitude);
                branches.Add(branch);
                    }
            return Ok(branches);
        }

        // GET: api/Branches/5
        [ResponseType(typeof(BranchModel))]
        public IHttpActionResult GetBranch(int id)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }

            var branchToReturn = new BranchModel(branch.Index, branch.Name, branch.Address, branch.Latitude, branch.Longitude);

            return Ok(branchToReturn);
        }

        // PUT: api/Branches/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBranch(int id, Branch branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != branch.Index)
            {
                return BadRequest();
            }

            db.Entry(branch).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
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

        // POST: api/Branches
        [ResponseType(typeof(Branch))]
        public IHttpActionResult PostBranch(Branch branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Branches.Add(branch);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BranchExists(branch.Index))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = branch.Index }, branch);
        }

        // DELETE: api/Branches/5
        [ResponseType(typeof(Branch))]
        public IHttpActionResult DeleteBranch(int id)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }

            db.Branches.Remove(branch);
            db.SaveChanges();

            return Ok(branch);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BranchExists(int id)
        {
            return db.Branches.Count(e => e.Index == id) > 0;
        }
    }
}