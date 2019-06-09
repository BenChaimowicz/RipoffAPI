using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RipOffAPI.Controllers
{
    [Authorize]
    public class FileUploadController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> UploadFile()
        {
            var identity = (ClaimsIdentity)User.Identity;
            int iuid = Convert.ToInt32(identity.FindFirst("user_id").Value);
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/App_Data/Images/Users/");
            var provider = new MultipartFormDataStreamProvider(root);


            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    var lastDot = file.Headers.ContentDisposition.FileName.LastIndexOf('.');
                    var extension = file.Headers.ContentDisposition.FileName.Substring(lastDot); 
                    var name = iuid.ToString() + extension;

                    name = name.Trim('"');

                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(root, name);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    File.Move(localFileName, filePath);
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            return Ok("File Uploaded successfuly!");
        }
    }
}
