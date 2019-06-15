using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace RipOffAPI.Controllers
{
    public class AuthController : ApiController
    {
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetUserRole()
        {
            var identity = (ClaimsIdentity)User.Identity;
            string irole = identity.FindFirst(ClaimTypes.Role).Value;
            return Ok(irole);
        }
    }
}
