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
        public async Task<JsonResult> UploadComponent(IEnumerable<HttpPostedFileBase> Files, string prefix,string OldImage)
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
                if (!string.IsNullOrEmpty(OldImage))
                {
                    DeleteImg(OldImage);
                }
                result.Data = new { Success = true, Message = "Images have been saved on the server successfully." };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = "Oops! The images could not be saved on the server. " + ex.Message };
            }
            return result;
        }
        [HttpPost]
        public JsonResult DeleteImg(string img)
        {
            try
            {
                if (img.Length > 0)
                {
                    string removeimagepath = System.Web.Hosting.HostingEnvironment.MapPath(img);
                    if (System.IO.File.Exists(removeimagepath))
                    {
                        System.IO.File.Delete(removeimagepath);
                    }
                    return Json(new { Success = true, Message = "Successfully remove image from server." });
                }
                return Json(new { Success = false, Message = "Oops! Image not found." });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = "Oops! Something went wrong during removing the image from server. " + ex.Message });
            }
        }
    }
}