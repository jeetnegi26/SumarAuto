using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace sumarauto.web.Controllers
{
    public class ImageController : Controller
    {
        [HttpPost]
        public async Task<JsonResult> UploadComponent(IEnumerable<HttpPostedFileBase> Files, string prefix)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                foreach (var file in Files)
                {
                    var fileName = prefix + file.FileName;
                    var path = Path.Combine(Server.MapPath("~/Content/Component/"), fileName);
                    await Task.Run(() => file.SaveAs(path));
                }
                result.Data = new { Success = true, Message = "Images have been saved on the server successfully." };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = "Oops! The images could not be saved on the server. " + ex.Message };
            }
            return result;
        }
    }
}