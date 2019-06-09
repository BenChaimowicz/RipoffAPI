using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RipOffAPI.Controllers
{
    [Authorize]
    public class FileUploadController : ApiController
    {
        [HttpPost]
        public IHttpActionResult UploadFile()
        {
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/App_Data/Images");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {

            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
