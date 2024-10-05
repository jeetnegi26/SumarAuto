using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using sumarauto.database;

namespace sumarauto.web.Controllers
{
    public class ImageController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sumarautoDb"].ConnectionString;
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
        public async Task<JsonResult> UploadBlog(IEnumerable<HttpPostedFileBase> Files, string prefix,string OldImage)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                foreach (var file in Files)
                {
                    var fileName = prefix + file.FileName;
                    var path = Path.Combine(Server.MapPath("~/Content/Blog/"), fileName);
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

        [HttpPost]
        public async Task<JsonResult> UploadAutoPartImgs(IEnumerable<HttpPostedFileBase> Files, int Id = 0,  string SelectedDefault = "")
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                if (Files != null)
                {
                    var listImgs = new List<AutoPartImages>();
                    using(var db = new AppDbContext())
                    {
                        foreach (var file in Files)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                // Generate unique file name
                                var timestampPrefix = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                                var fileName = timestampPrefix + "_" + Path.GetFileName(file.FileName);
                                var path = Path.Combine(Server.MapPath("~/Content/AutoPartImages/"), fileName);
                                await Task.Run(() => file.SaveAs(path));
                                var imgUrl = string.Format("/Content/AutoPartImages/" + fileName);

                                var data = new AutoPartImages()
                                {
                                    Image = imgUrl,
                                    AutoPartId = Id,
                                    Default = !string.IsNullOrEmpty(SelectedDefault) &&
                                    imgUrl.Contains(SelectedDefault) ? true : false,
                                };

                                listImgs.Add(data);
                            }
                        }

                        db.AutoPartImages.AddRange(listImgs);
                        await db.SaveChangesAsync();
                    }
                }
                result.Data = new { Success = true, Message = "Media and data successfully uploaded." };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = "Oops! The media could not be saved on the server, try again. " + ex.Message };
            }
            return result;
        }
        // Helper function to resize an image
        public Image ResizeImage(Image image, int targetWidth, int targetHeight)
        {
            // Calculate the aspect ratio
            var originalWidth = image.Width;
            var originalHeight = image.Height;
            float ratioX = (float)targetWidth / originalWidth;
            float ratioY = (float)targetHeight / originalHeight;
            float ratio = Math.Min(ratioX, ratioY); // Maintain aspect ratio, use the smaller ratio

            // Calculate the new dimensions based on the aspect ratio
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            // Create a blank image with the target dimensions and a white background
            var destImage = new Bitmap(targetWidth, targetHeight);
            using (Graphics graphics = Graphics.FromImage(destImage))
            {
                // Fill the background with white
                graphics.Clear(Color.White);

                // Set high quality settings
                graphics.CompositingMode = CompositingMode.SourceOver;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // Calculate the position to center the image on the canvas
                int posX = (targetWidth - newWidth) / 2;
                int posY = (targetHeight - newHeight) / 2;

                // Draw the resized image on the canvas
                graphics.DrawImage(image, posX, posY, newWidth, newHeight);
            }

            return destImage;
        }

        [HttpPost]
        public JsonResult DeletAutoPartImg(string img)
        {
            try
            {
                if (img.Length > 0)
                {
                    string removeimagepath = System.Web.Hosting.HostingEnvironment.MapPath(img);
                    if (System.IO.File.Exists(removeimagepath))
                    {
                        System.IO.File.Delete(removeimagepath);
                        using (var db = new AppDbContext())
                        {
                            var data = db.AutoPartImages.FirstOrDefault(x => x.Image == img);
                            if (data != null)
                            {
                                db.AutoPartImages.Remove(data);
                                db.SaveChanges();
                            }
                        }
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

        public JsonResult UploadImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var file = Request.Files[0];
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                file.SaveAs(path);
                result.Data = new { Success = true, ImageURL = string.Format("/Content/images/{0}", fileName) };

            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }
            return result;
        }
        //Gallery Work
        [HttpPost]
        public async Task<JsonResult> UploadGalleryImages(IEnumerable<HttpPostedFileBase> Files, string Title, int Id = 0, string SelectedDefault = "")
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var galleryImgList = new List<GalleryImage>();

                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("UpsertGallery", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@Title", Title);
                    command.Parameters.AddWithValue("@UserHostAdd", Request.UserHostAddress);
                    command.Parameters.AddWithValue("@CreatedBy", Convert.ToString(TempData["UName"]));
                    command.Parameters.AddWithValue("@SelectedDefault", string.IsNullOrEmpty(SelectedDefault) ? (object)DBNull.Value : SelectedDefault);
                    var outputIdParam = new SqlParameter("@OutputId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    connection.Open();
                    command.ExecuteNonQuery();
                    Id = (int)outputIdParam.Value;
                }
                using (var db = new AppDbContext())
                {
                    if (Files != null)
                    {
                        foreach (var file in Files)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                // Generate unique file name
                                var timestampPrefix = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                                var fileName = timestampPrefix + "_" + Path.GetFileName(file.FileName);
                                var path = Path.Combine(Server.MapPath("~/Content/Gallery/"), fileName);
                                await Task.Run(() => file.SaveAs(path));
                                var imgUrl = string.Format("/Content/Gallery/" + fileName);
                                var data = new GalleryImage()
                                {
                                    Image = imgUrl,
                                    GalleryId = Id,
                                    DefaultImage = !string.IsNullOrEmpty(SelectedDefault) &&
                                    imgUrl.Contains(SelectedDefault) ? true : false,
                                };

                                galleryImgList.Add(data);
                            }
                        }

                        db.GalleryImages.AddRange(galleryImgList);
                        await db.SaveChangesAsync();
                    }
                }
                result.Data = new { Success = true, Message = "Media and data successfully uploaded." };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = "Oops! The media could not be saved on the server, try again. " + ex.Message };
            }
            return result;
        }
        [HttpPost]
        public JsonResult DeleteGalleryImg(string img)
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
                    using (var db = new AppDbContext())
                    {
                        var data = db.GalleryImages.FirstOrDefault(x => x.Image == img);
                        if (data != null)
                        {
                            db.GalleryImages.Remove(data);
                            db.SaveChanges();
                        }
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