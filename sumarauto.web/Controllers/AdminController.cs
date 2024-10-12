using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using DataModel;
using Model;
using Service;
using sumarauto.database;
using sumarauto.web.App_Start;

namespace sumarauto.web.Controllers
{
    [CustomAuthorize]
    public class AdminController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sumarautoDb"].ConnectionString;
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        #region AutoParts
        //public ActionResult AutoPart()
        //{
        //    return View();
        //}
        public ActionResult AutoPartAction(int Id = 0)
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult saveOrUpdateAutoParts(AutoPartDataModel model)
        {
            bool result = false;
            string Message = "Something went wrong!";
            try
            {
                if (!ModelState.IsValid)
                {
                    // Handle the form submission here
                    return Json(new { success = result,message = "Please fill the required fields." });
                }

                int itemId = model.Id;  // If this is an update, it will have an ID, else it will be 0

                using (var connection = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("saveOrUpdateAutoParts", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Main item details
                    cmd.Parameters.AddWithValue("@ItemId", itemId == 0 ? (object)DBNull.Value : itemId);
                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@ExtraField", string.IsNullOrEmpty(model.ExtraField) ? (object)DBNull.Value : model.ExtraField);
                    cmd.Parameters.AddWithValue("@DefaultImage", string.IsNullOrEmpty(model.DefaultImage) ? (object)DBNull.Value : model.DefaultImage);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@CategoryId", model.CategoryId);
                    cmd.Parameters.AddWithValue("@DisplayOrder", model.DisplayOrder);
                    cmd.Parameters.AddWithValue("@IsFeatured", model.IsFeatured);
                    cmd.Parameters.AddWithValue("@Status", model.Status);
                    cmd.Parameters.AddWithValue("@CreatedBy", "Admin");
                    cmd.Parameters.AddWithValue("@RewriteUrl", model.RewriteUrl);
                    cmd.Parameters.AddWithValue("@UserHostAddress", Request.UserHostAddress);

                    // Table-valued parameter for multiple details
                    var table = new DataTable();
                    table.Columns.Add("Make", typeof(string));
                    table.Columns.Add("Model", typeof(string));
                    table.Columns.Add("Year", typeof(string));
                    table.Columns.Add("Engine", typeof(string));
                    table.Columns.Add("Liter", typeof(string));
                    table.Columns.Add("Chassis", typeof(string));
                    table.Columns.Add("DisplayOrder", typeof(int));
                    int dOCount = 0;
                    if (model.MultipleDetails != null)
                    {
                        foreach (var detail in model.MultipleDetails)
                        {
                            if (!string.IsNullOrEmpty(detail.Make))
                            {
                                dOCount += 1;
                                table.Rows.Add(detail.Make, detail.Model, detail.Year, detail.Engine, detail.Liter, detail.Chassis, dOCount);
                            }
                        }
                    }


                    var detailsParam = new SqlParameter("@MultipleDetails", SqlDbType.Structured)
                    {
                        TypeName = "dbo.MultipleDetailType",
                        Value = table
                    };

                    cmd.Parameters.Add(detailsParam);

                    connection.Open();
                    itemId = (int)cmd.ExecuteScalar();
                    result = true;
                }
                return Json(new { success = result, message = itemId.ToString() });
            }
            catch (Exception ex)
            {
                return Json(new { success = result, message = Message + " "+ ex.Message});
            }
        }

        public ActionResult AutoPartEdit(int Id = 0)
        {
            using(var db = new AppDbContext())
            {
                var data = db.AutoPart.Include(x=>x.AutoPartImages).AsNoTracking().FirstOrDefault(x => x.Id == Id);
                ViewBag.GalleryImgs = data.AutoPartImages.Select(x => x.Image).ToList();
                var dataDefault = data.AutoPartImages.FirstOrDefault(x => x.Default == true);
                ViewBag.DefaultImg = dataDefault != null ? dataDefault.Image : "";
                return View(data);
            }
        }
        //Make
        public ActionResult MakeList(string Id)
        {
            List<MakeDetail> makeDetails = new List<MakeDetail>();
            try
            {
                ViewBag.AutoPartId = Id;
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("GetMakeList", connection);
                    command.Parameters.AddWithValue("@Id", Id);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MakeDetail makeDetail = new MakeDetail
                            {
                                Id = (int)reader["Id"],
                                AutoPartSId = Convert.ToString(reader["AutoPartSId"]),
                                Make = Convert.ToString(reader["Make_Title"]),
                                Chassis = string.IsNullOrEmpty(Convert.ToString(reader["Chassis_Title"])) ? "-" : Convert.ToString(reader["Chassis_Title"]),
                                Engine = string.IsNullOrEmpty(Convert.ToString(reader["Engine_Title"])) ? "-" : Convert.ToString(reader["Engine_Title"]),
                                Liter = string.IsNullOrEmpty(Convert.ToString(reader["Liter_Title"])) ? "-" : Convert.ToString(reader["Liter_Title"]),
                                Model = string.IsNullOrEmpty(Convert.ToString(reader["Model_Title"])) ? "-" : Convert.ToString(reader["Model_Title"]),
                                Year = string.IsNullOrEmpty(Convert.ToString(reader["Year_Title"])) ? "-" : Convert.ToString(reader["Year_Title"]),
                                DisplayOrder = !string.IsNullOrEmpty(Convert.ToString(reader["DisplayOrder"]))
                                ? Convert.ToInt32(Convert.ToString(reader["DisplayOrder"])): 0,
                            };
                            makeDetails.Add(makeDetail);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(makeDetails.OrderBy(x => x.DisplayOrder).ThenByDescending(x=>x.Id)
                      .ToList());
        }
        public ActionResult AutoMakeAction(int autoId,int Id=0)
        {
            ViewBag.AutoPartId = autoId;
            var data = new MakeDetailAction();
            if (Id > 0)
            {
                using(var db = new AppDbContext())
                {
                   var Modeldata = db.AutoPartMake.AsNoTracking().FirstOrDefault(x=>x.Id == Id);
                    data.AutoPartId = autoId;
                    data.Id = Id;
                    data.Year = Modeldata.Year_Title;
                    data.DisplayOrder = Modeldata.DisplayOrder;
                    data.Chassis = Modeldata.Chassis_Title;
                    data.Status = Modeldata.Status;
                    data.Engine = Modeldata.Engine_Title;
                    data.Liter = Modeldata.Liter_Title;
                    data.MakeId = Modeldata.Make_Id;
                    data.Model = Modeldata.Model_Title;
                }
            }
            return View(data);
        }
        [HttpPost]
        public ActionResult AutoMakeAction(MakeDetailAction autoPart)
        {
            ViewBag.AutoPartId = autoPart.AutoPartId;
            bool result = false;
            string message = "Something went wrong!";
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("SaveOrUpdateAutoPartMake", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Pass parameters
                    cmd.Parameters.AddWithValue("@Id", autoPart.Id == 0 ? (object)DBNull.Value : autoPart.Id);
                    cmd.Parameters.AddWithValue("@AutoPartId", autoPart.AutoPartId);
                    cmd.Parameters.AddWithValue("@MakeId", autoPart.MakeId);
                    cmd.Parameters.AddWithValue("@Model_Title", string.IsNullOrEmpty(autoPart.Model) ? (object)DBNull.Value : autoPart.Model);
                    cmd.Parameters.AddWithValue("@Year_Title", string.IsNullOrEmpty(autoPart.Year) ? (object)DBNull.Value : autoPart.Year);
                    cmd.Parameters.AddWithValue("@Engine_Title", string.IsNullOrEmpty(autoPart.Engine) ? (object)DBNull.Value : autoPart.Engine);
                    cmd.Parameters.AddWithValue("@Liter_Title", string.IsNullOrEmpty(autoPart.Liter) ? (object)DBNull.Value : autoPart.Liter);
                    cmd.Parameters.AddWithValue("@Chassis_Title", string.IsNullOrEmpty(autoPart.Chassis) ? (object)DBNull.Value : autoPart.Chassis);
                    cmd.Parameters.AddWithValue("@DisplayOrder", autoPart.DisplayOrder);
                    cmd.Parameters.AddWithValue("@Status", autoPart.Status);
                    cmd.Parameters.AddWithValue("@CreatedBy", "Admin");  
                    cmd.Parameters.AddWithValue("@UserHostAddress", Request.UserHostAddress);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(new { success = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Components
        #region Category
        public ActionResult Category()
        {
            List<Category> categories = new List<Category>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("GetCategoryList", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category category = new Category
                            {
                                Id = (int)reader["Id"],
                                Title = Convert.ToString(reader["Title"]),
                                Image = Convert.ToString(reader["Image"]),
                                Description = Convert.ToString(reader["Description"]),
                                Status = (bool)reader["Status"],
                                IsFeatured = (bool)reader["IsFeatured"],
                                CreatedOn = (DateTime)reader["CreatedOn"],
                                CreatedOnString = ((DateTime)reader["CreatedOn"]).ToString("dd MMM yyyy"),
                                EditedOnString = ((DateTime)reader["EditedOn"]).ToString("dd MMM yyyy"),
                            };
                            categories.Add(category);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(categories.OrderBy(x => x.DisplayOrder)
                      .ThenBy(x => x.CreatedOn)
                      .ToList());
        }
        public ActionResult CategoryAction(int Id = 0)
        {
            var data = new Category();
            try
            {
                if (Id > 0)
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        var command = new SqlCommand("GetCategoryById", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", Id);
                        connection.Open();
                        var reader = command.ExecuteReader();
                        if (reader.Read()) 
                        {
                            data.Id = (int)reader["Id"];
                            data.Title = Convert.ToString(reader["Title"]);
                            data.Image = Convert.ToString(reader["Image"]);
                            data.Description = Convert.ToString(reader["Description"]);
                            data.DisplayOrder = Convert.ToInt32(reader["DisplayOrder"]);
                        }
                    }
                    return View(data);
                }
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult CategoryAction(Category category)
        {
            var data = new Category();
            string message = DateTime.Now.ToString("yyyyMMddHHmmss");
            if (category.NewImage != null && category.NewImage != "" && category.NewImage.Length > 0)
            {
                category.Image = "/Content/Component/" + message + category.NewImage;
            }
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("CategoryAction", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", category.Id);
                        command.Parameters.AddWithValue("@Title", category.Title);
                        command.Parameters.AddWithValue("@Description", (object)category.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Image", (object)category.Image ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", "Admin");
                        command.Parameters.AddWithValue("@HostAddress", Request.UserHostAddress);
                        command.Parameters.AddWithValue("@DisplayOrder", (object)category.DisplayOrder ?? 0); // Ensure integer type

                        // Output parameters
                        var requestparam = new SqlParameter("@Result", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        var requestparam1 = new SqlParameter("@IsCatExist", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(requestparam);
                        command.Parameters.Add(requestparam1);

                        connection.Open();
                        command.ExecuteNonQuery();

                        // Safely handle output parameter values
                        int result = requestparam.Value != DBNull.Value ? (int)requestparam.Value : 0;
                        int result1 = requestparam1.Value != DBNull.Value ? (int)requestparam1.Value : 0;

                        if (result1 == 1)
                        {
                            result = 0;
                            message = "Category with same name already exists in database.";
                        }

                        return Json(new { Success = result > 0, Message = message }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {
                return Json(new { Success = false, Message = "Something went wrong!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Make 
        public ActionResult Make()
        {
            try
            {
                using(var db = new AppDbContext())
                {
                    var data = db.Make.OrderBy(x => x.DisplayOrder).ThenBy(x => x.CreatedOn).ToList();
                    return View(data);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult MakeAction(int Id = 0)
        {
            var data = new Make();
            try
            {
                if (Id > 0)
                {
                    using(var db = new AppDbContext())
                    {
                        data = db.Make.FirstOrDefault(x=>x.MakeId == Id);
                    }
                    return View(data);
                }
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult MakeAction(Make make)
        {
            string message = DateTime.Now.ToString("yyyyMMddHHmmss");
            if (make.NewImage != null && make.NewImage != "" && make.NewImage.Length > 0)
            {
                make.Image = "/Content/Component/" + message + make.NewImage;
            }
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("MakeAction", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", make.MakeId);
                        command.Parameters.AddWithValue("@Title", make.Title);
                        command.Parameters.AddWithValue("@Description", (object)make.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Image", (object)make.Image ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", "Admin");
                        command.Parameters.AddWithValue("@HostAddress", Request.UserHostAddress);
                        command.Parameters.AddWithValue("@DisplayOrder", (object)make.DisplayOrder ?? 0); // Ensure integer type

                        // Output parameters
                        var requestparam = new SqlParameter("@Result", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        var requestparam1 = new SqlParameter("@IsMakeExist", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(requestparam);
                        command.Parameters.Add(requestparam1);

                        connection.Open();
                        command.ExecuteNonQuery();

                        // Safely handle output parameter values
                        int result = requestparam.Value != DBNull.Value ? (int)requestparam.Value : 0;
                        int result1 = requestparam1.Value != DBNull.Value ? (int)requestparam1.Value : 0;

                        if (result1 == 1)
                        {
                            result = 0;
                            message = "Make with same name already exists in database.";
                        }

                        return Json(new { Success = result > 0, Message = message }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {
                return Json(new { Success = false, Message = "Something went wrong!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region MModel
        public ActionResult MModel()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var data = db.MModel.AsNoTracking()
                      .OrderBy(x => x.DisplayOrder)
                      .ThenBy(x => x.CreatedOn)
                      .ToList();
                    return View(data);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult MModelAction(int Id = 0)
        {
            var data = new MModel();
            try
            {
                if (Id > 0)
                {
                    using (var db = new AppDbContext())
                    {
                        data = db.MModel.FirstOrDefault(x => x.Id == Id);
                    }
                    return View(data);
                }
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult MModelAction(MModel mModel)
        {
            string message = string.Empty;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("ModelAction", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", mModel.Id);
                        command.Parameters.AddWithValue("@Title", mModel.Title);
                        command.Parameters.AddWithValue("@Description", (object)mModel.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", "Admin");
                        command.Parameters.AddWithValue("@HostAddress", Request.UserHostAddress);
                        command.Parameters.AddWithValue("@DisplayOrder", (object)mModel.DisplayOrder ?? 0); // Ensure integer type

                        // Output parameters
                        var requestparam = new SqlParameter("@Result", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        var requestparam1 = new SqlParameter("@IsModelExist", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(requestparam);
                        command.Parameters.Add(requestparam1);

                        connection.Open();
                        command.ExecuteNonQuery();

                        // Safely handle output parameter values
                        int result = requestparam.Value != DBNull.Value ? (int)requestparam.Value : 0;
                        int result1 = requestparam1.Value != DBNull.Value ? (int)requestparam1.Value : 0;

                        if (result1 == 1)
                        {
                            result = 0;
                            message = "Model with same name already exists in database.";
                        }

                        return Json(new { Success = result > 0, Message = message }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {
                return Json(new { Success = false, Message = "Something went wrong!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #endregion

        #region Change Password
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ResetPassword password)
        {
            var message = "Something weng wrong!";
            bool success = false;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("ChangePassword", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OldPassword", password.OldPassword);
                    command.Parameters.AddWithValue("@NewPassword", password.NewPassword);
                    command.Parameters.AddWithValue("@UserId", 2);
                    var resultParam = new SqlParameter("@Result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultParam);
                    connection.Open();
                    command.ExecuteNonQuery();
                    int result = (int)resultParam.Value;
                    if (result == 1)
                    {
                        message = "Your password has been updated successfully.";
                        success = true;
                    }
                    else
                    {
                        message = "The old password is incorrect.";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Json(new { Success = success, Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Blogs
        public ActionResult Blogs()
        {
            return View();
        }
        public ActionResult BlogAction(int Id = 0)
        {
            var data = new Blogs();
            data.Date = DateTime.Now;
            if (Id > 0)
            {
                using(var db = new AppDbContext())
                {
                    data = db.Blogs.AsNoTracking().FirstOrDefault(x=>x.Id == Id);
                }
            }
            return View(data);
        }
        [HttpPost]
        public ActionResult BlogAction(Blogs blogs)
        {
            bool result = false;
            int Id = 0;
            string Message = DateTime.Now.ToString("yyyyMMddHHmmss");

            try
            {
                if (blogs.NewImage != null && blogs.NewImage != "" && blogs.NewImage.Length > 0)
                {
                    blogs.Image = "/Content/Blog/" + Message + blogs.NewImage;
                }
                using (var db = new AppDbContext())
                {
                    if (blogs != null && blogs.Id > 0)
                    {
                        //Edit
                        blogs.EditedOn = DateTime.Now;
                        blogs.UserHostAdd = Request.UserHostAddress;
                        Id = blogs.Id;
                        db.Entry(blogs).State = EntityState.Modified;
                        db.SaveChanges();
                        result = true;
                    }
                    else
                    {
                        //Save
                        blogs.CreatedBy = "Admin";
                        blogs.CreatedOn = DateTime.Now;
                        blogs.EditedOn = DateTime.Now;
                        blogs.UserHostAdd = Request.UserHostAddress;
                        db.Blogs.Add(blogs);
                        db.SaveChanges();
                        result = true;
                        Id = blogs.Id;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return Json(new {success = result, message = Message },JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Banners
        public ActionResult BannerList()
        {
            try
            {
                var data = BannerService.Instance.GetBannerList();
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult BannerAdd()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult BannerAdd(Banner model)
        {
            try
            {
                model.Status = true;
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                model.UserHostAdd = Request.UserHostAddress;
                var result = BannerService.Instance.SaveBanner(model);
                TempData["Message"] = result.Messsage;

            }
            catch (Exception)
            {
                TempData["Message"] = Constant.Message.Error;
            }
            return RedirectToAction("BannerList");
        }
        public ActionResult BannerEdit(int Id)
        {
            try
            {
                var BannerData = BannerService.Instance.GetBannerById(Id);
                TempData["OldImage"] = BannerData.Image;
                return View(BannerData);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult BannerEdit(Banner model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                model.UserHostAdd = Request.UserHostAddress;
                var result = BannerService.Instance.EditBanner(model);
                if (TempData["OldImage"].ToString() != result.Value1)
                {
                    string removeimagepath = Request.MapPath(TempData["OldImage"].ToString());
                    if (System.IO.File.Exists(removeimagepath))
                    {
                        System.IO.File.Delete(removeimagepath);
                    }
                }
                TempData["Message"] = result.Messsage;
            }
            catch (Exception)
            {
                TempData["Message"] = Constant.Message.Error;
            }
            return RedirectToAction("BannerList");
        }
        public ActionResult BannerDelete(int Id)
        {
            try
            {
                var result = BannerService.Instance.DeleteBanners(Id);
                TempData["Message"] = result.Messsage;
                if (result.Value1 != null)
                {
                    string removeimagepath = Request.MapPath(result.Value1);
                    if (System.IO.File.Exists(removeimagepath))
                    {
                        System.IO.File.Delete(removeimagepath);
                    }
                }
            }
            catch (Exception)
            {
                TempData["Message"] = Constant.Message.Error;
            }
            return RedirectToAction("BannerList");
        }
        [HttpPost]
        public ActionResult BannerStatus(bool status, int id)
        {
            var result = false;
            using (var context = new AppDbContext())
            {
                var data = context.Banners.FirstOrDefault(x => x.Id == id);
                data.Status = status;
                context.Entry(data).State = EntityState.Modified;
                context.SaveChanges();
                result = true;
            }
            return Json(result);
        }
        #endregion

        #region Website Info
        public ActionResult WebsiteInformation()
        {
            var data = WebInfoService.Instance.GetInfoKeys();
            return View(data);
        }
        public ActionResult InfoAdd()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult InfoAdd(Key Key)
        {
            string result;
            try
            {
                Key.CreatedOn = HelperService.Instance.getCurrentDateTime();
                var data = WebInfoService.Instance.SaveWebInfo(Key);
                result = data == true ? "true" : "false";
                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ActionResult InfoEdit(int id)
        {
            try
            {
                var key = WebInfoService.Instance.GetInfoKey(id);
                return PartialView(key);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public ActionResult InfoEdit(Key key)
        {
            string result = "false";
            try
            {
                var data = WebInfoService.Instance.EditWebInfo(key);
                result = data == true ? "true" : "false";
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(result);
            }
        }
        public bool InfoDelete(int id)
        {
            bool result = false;
            try
            {
                result = WebInfoService.Instance.RemoveWebInfo(id);
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
        #endregion

        #region Gallery
        public ActionResult Gallery()
        {
            return View();
        }
        public ActionResult GalleryAction(int Id = 0)
        {
            var gallery = new Gallery();
            List<GalleryImage> images = new List<GalleryImage>();
            if (Id > 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetGalleryDetails", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@GalleryId", Id);

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Read the banner details
                            if (reader.Read())
                            {
                                gallery = new Gallery
                                {
                                    Id = (int)reader["GalleryId"],
                                    Title = Convert.ToString(reader["Title"]),
                                };
                            }
                            // Move to the next result set (banner images)
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    images.Add(new GalleryImage
                                    {
                                        Id = (int)reader["GalleryImgId"],
                                        Image = reader["Image"].ToString(),
                                        DefaultImage = (bool)reader["DefaultImage"],
                                    });
                                }
                            }
                            //banner.BannerImages = images;
                            ViewBag.GalleryImgs = images.Select(x => x.Image).ToList();
                            var dataDefault = images.FirstOrDefault(x => x.DefaultImage == true);
                            ViewBag.DefaultImg = dataDefault != null ? dataDefault.Image : "";
                        }
                    }
                }
            }
            return View(gallery);
        }
        [HttpPost]
        public ActionResult UpdateGalleryStatus(int id, string field, bool value)
        {
            using (var context = new AppDbContext())
            {
                var downMaster = context.Gallery.FirstOrDefault(p => p.Id == id);
                if (downMaster == null)
                {
                    return Json(new { success = false, message = "Download page not found." });
                }
                // Update the corresponding field
                switch (field)
                {
                    case "Status":
                        downMaster.Status = value;
                        break;
                    default:
                        return Json(new { success = false, message = "Invalid field name." });
                }

                context.SaveChanges();
                return Json(new { success = true, message = "Status updated successfully!" });
            }
        }
        #endregion

        #region Contact Form
        public async Task<ActionResult> ContactList()
        {
            try
            {
                var dataList = await ContactService.Instance.GetContactList();
                return View(dataList);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public JsonResult DeleteContactForm(int id)
        {
            var resultData = "False";
            try
            {
                var data = ContactService.Instance.RemoveContactForm(id);
                resultData = data == true ? "True" : "False";
                return Json(resultData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(resultData, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EnquireList()
        {
            return View();
        }
        #endregion

        #region Clients
        public ActionResult Clients()
        {
            using(var db = new AppDbContext())
            {
                var data = db.Clients.AsNoTracking().ToList();
                return View(data);
            }
        }
        public ActionResult ClientAction(int Id = 0)
        {
            var data = new Client();
            try
            {
                if (Id > 0)
                {
                    using (var db = new AppDbContext())
                    {
                        data = db.Clients.FirstOrDefault(x => x.Id == Id);
                    }
                    return View(data);
                }
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult ClientAction(Client client)
        {
            var data = new Category();
            string message = DateTime.Now.ToString("yyyyMMddHHmmss");
            if (client.NewImage != null && client.NewImage != "" && client.NewImage.Length > 0)
            {
                client.Image = "/Content/Component/" + message + client.NewImage;
            }
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("ClientAction", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", client.Id);
                        command.Parameters.AddWithValue("@Title", client.Title);
                        command.Parameters.AddWithValue("@Description", (object)client.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Image", (object)client.Image ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", "Admin");
                        command.Parameters.AddWithValue("@HostAddress", Request.UserHostAddress);
                        command.Parameters.AddWithValue("@DisplayOrder", (object)client.DisplayOrder ?? 0); // Ensure integer type

                        // Output parameters
                        var requestparam = new SqlParameter("@Result", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        var requestparam1 = new SqlParameter("@IsCatExist", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(requestparam);
                        command.Parameters.Add(requestparam1);

                        connection.Open();
                        command.ExecuteNonQuery();

                        // Safely handle output parameter values
                        int result = requestparam.Value != DBNull.Value ? (int)requestparam.Value : 0;
                        int result1 = requestparam1.Value != DBNull.Value ? (int)requestparam1.Value : 0;

                        if (result1 == 1)
                        {
                            result = 0;
                            message = "Client with same name already exists in database.";
                        }

                        return Json(new { Success = result > 0, Message = message }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {
                return Json(new { Success = false, Message = "Something went wrong!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region FileUpload
        public ActionResult FileUpload()
        {
            List<FileUploadModel> fileUploadModels = new List<FileUploadModel>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("GetFileUploadList", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FileUploadModel fileUploadModel = new FileUploadModel
                            {
                                Id = (int)reader["Id"],
                                Title = Convert.ToString(reader["Title"]),
                                FilePath = Convert.ToString(reader["FilePath"]),
                                Status = (bool)reader["Status"],
                                Date = (DateTime)reader["Date"],
                                CreatedOnString = ((DateTime)reader["Date"]).ToString("dd MMM yyyy"),
                            };
                            fileUploadModels.Add(fileUploadModel);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(fileUploadModels.OrderBy(x => x.DisplayOrder)
                      .ThenBy(x => x.CreatedOn)
                      .ToList());
        }
        public ActionResult FileUploadAction(int Id = 0)
        {
            var data = new FileUploadModel();
            try
            {
                if (Id > 0)
                {
                    using (var db = new AppDbContext())
                    {
                        data = db.FileUploadModels.FirstOrDefault(x => x.Id == Id);
                    }
                    return View(data);
                }
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}