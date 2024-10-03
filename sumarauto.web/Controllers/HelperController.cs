using sumarauto.database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using sumarauto.DataModel;
using Model;
using DataModel;

namespace sumarauto.web.Controllers
{
    public class HelperController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sumarautoDb"].ConnectionString;
        #region Helper  
        [HttpPost]
        public ActionResult ChangeOrder(int Id, string Type, int displayOrder = 0)
        {
            bool result = false;
            try
            {
                using (var db = new AppDbContext())
                {
                    switch (Type)
                    {
                        case "Category":
                            var Category = db.Category.Find(Id);
                            if (Category != null)
                            {
                                bool value = Category.IsFeatured;
                                Category.IsFeatured = value == true ? false : true;
                                db.SaveChanges();
                            }
                            result = true;
                            break;
                        case "AutoPart":
                            var AuModel = db.AutoPart.Find(Id);
                            if (AuModel != null)
                            {
                                AuModel.DisplayOrder = displayOrder;
                                db.SaveChanges();
                            }
                            result = true;
                            break;
                        case "Blogs":
                            var Blogs = db.Blogs.Find(Id);
                            if (Blogs != null)
                            {
                                Blogs.DisplayOrder = displayOrder;
                                db.SaveChanges();
                            }
                            result = true;
                            break;
                        default:
                            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception)
            {
                result = false;
            }
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult FeaturedChange(int Id, string Type)
        {
            bool result = false;
            try
            {
                using (var db = new AppDbContext())
                {
                    switch (Type)
                    {
                        case "Category":
                            var Category = db.Category.Find(Id);
                            if (Category != null)
                            {
                                bool value = Category.IsFeatured;
                                Category.IsFeatured = value == true ? false : true;
                                db.SaveChanges();
                            }
                            result = true;
                            break;
                        case "AutoPart":
                            var AuModel = db.AutoPart.Find(Id);
                            if (AuModel != null)
                            {
                                bool value = AuModel.IsFeatured;
                                AuModel.IsFeatured = value == true ? false : true;
                                db.SaveChanges();
                            }
                            result = true;
                            break;
                        case "Blogs":
                            var Blogs = db.Blogs.Find(Id);
                            if (Blogs != null)
                            {
                                bool value = Blogs.IsFeatured;
                                Blogs.IsFeatured = value == true ? false : true;
                                db.SaveChanges();
                            }
                            result = true;
                            break;
                        default:
                            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception)
            {
                result = false;
            }
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult StatusChange(int Id, string Type)
        {
            bool result = false;
            try
            {
                using (var db = new AppDbContext())
                {
                    switch (Type)
                    {
                        case "Category":
                            var Category = db.Category.Find(Id);
                            if (Category != null)
                            {
                                bool value = Category.Status;
                                Category.Status = value == true ? false : true;
                                db.SaveChanges();
                            }
                            result = true;
                            break;
                        case "Make":
                            var Make = db.Make.Find(Id);
                            if (Make != null)
                            {
                                bool value = Make.Status;
                                Make.Status = value == true ? false : true;
                                db.SaveChanges();
                            }
                            result = true;
                            break;
                        case "MModel":
                            var MModel = db.MModel.Find(Id);
                            if (MModel != null)
                            {
                                bool value = MModel.Status;
                                MModel.Status = value == true ? false : true;
                                db.SaveChanges();
                            }
                            result = true;
                            break;
                        case "AutoPart":
                            var AuModel = db.AutoPart.Find(Id);
                            if (AuModel != null)
                            {
                                bool value = AuModel.Status;
                                AuModel.Status = value == true ? false : true;
                                db.SaveChanges();
                            }
                            result = true;
                            break;
                        case "Blogs":
                            var Blogs = db.Blogs.Find(Id);
                            if (Blogs != null)
                            {
                                bool value = Blogs.Status;
                                Blogs.Status = value == true ? false : true;
                                db.SaveChanges();
                            }
                            result = true;
                            break;
                        default:
                            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception)
            {
                result = false;
            }
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetDropdownCatList()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("GetCategoryList", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Active", 1);
                    connection.Open();
                    var rows = await command.ExecuteReaderAsync();
                    var result = new List<SelectListItem>();
                    while (rows.Read())
                    {
                        var data = new SelectListItem();
                        data.Text = rows["Title"].ToString();
                        data.Value = rows["Id"].ToString();
                        result.Add(data);
                    }
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ActionResult> GetMakedownList()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var dataList = await db.Make.AsNoTracking().Where(x=>x.Status == true).ToListAsync();
                    var result = new List<SelectListItem>();
                    foreach (var item in dataList)
                    {
                        var data = new SelectListItem();
                        data.Text = item.Title;
                        data.Value = item.MakeId.ToString();
                        result.Add(data);
                    }
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult GetAutoPartList(int draw, int start, int length, int dropdownId)
        {
            try
            {
                var sortColumnIndex = Request.Form.GetValues("order[0][column]")?.FirstOrDefault();
                var sortColumnDirection = Request.Form.GetValues("order[0][dir]")?.FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("GetAutoPartList", connection);
                    command.CommandType = CommandType.StoredProcedure;
                   // Adding parameters
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@Length", length);
                    command.Parameters.AddWithValue("@dropdownId", dropdownId);
                    command.Parameters.AddWithValue("@sortColumnIndex", sortColumnIndex);
                    command.Parameters.AddWithValue("@sortColumnDirection", sortColumnDirection);
                    command.Parameters.AddWithValue("@SearchValue", string.IsNullOrEmpty(searchValue) ? (object)DBNull.Value : searchValue);
                    connection.Open();
                   // Execute the command and read the results
                    using (var reader = command.ExecuteReader())
                    {
                        var AutoPartDataModels = new List<AutoPartDataModel>();
                        while (reader.Read())
                        {
                           var AutoPartDataModel = new AutoPartDataModel
                           {
                                Id = (int)reader["Id"],
                                AutoPartSId = string.IsNullOrEmpty(reader["AutoPartSId"].ToString()) ? "-" : reader["AutoPartSId"].ToString(),
                                Title = string.IsNullOrEmpty(reader["Title"].ToString()) ? "-" : reader["Title"].ToString(),
                                Category = string.IsNullOrEmpty(reader["Category"].ToString()) ? "-" : reader["Category"].ToString(),
                                Status = (bool)reader["Status"],
                                IsFeatured = (bool)reader["IsFeatured"],
                                DisplayOrder = (int)reader["DisplayOrder"],
                           };
                            AutoPartDataModels.Add(AutoPartDataModel);
                        }
                        // Move to the second result set to get the total count
                        reader.NextResult();
                        int totalRecords = 0;
                        if (reader.Read())
                        {
                            totalRecords = reader.GetInt32(0);
                        }
                        var result = new
                        {
                            draw = draw,
                            recordsTotal = totalRecords,
                            recordsFiltered = totalRecords,
                            data = AutoPartDataModels.OrderByDescending(x => x.DisplayOrder)
                        };
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
               
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult GetBlogList(int draw, int start, int length, int dropdownId)
        {
            try
            {
                var sortColumnIndex = Request.Form.GetValues("order[0][column]")?.FirstOrDefault();
                var sortColumnDirection = Request.Form.GetValues("order[0][dir]")?.FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("GetBlogList", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    // Adding parameters
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@Length", length);
                    command.Parameters.AddWithValue("@dropdownId", dropdownId);
                    command.Parameters.AddWithValue("@sortColumnIndex", sortColumnIndex);
                    command.Parameters.AddWithValue("@sortColumnDirection", sortColumnDirection);
                    command.Parameters.AddWithValue("@SearchValue", string.IsNullOrEmpty(searchValue) ? (object)DBNull.Value : searchValue);
                    connection.Open();
                    // Execute the command and read the results
                    using (var reader = command.ExecuteReader())
                    {
                        var Blogs = new List<Blogs>();
                        while (reader.Read())
                        {
                            var Blog = new Blogs
                            {
                                Id = (int)reader["Id"],
                                Title = string.IsNullOrEmpty(reader["Title"].ToString()) ? "-" : reader["Title"].ToString(),
                                Status = (bool)reader["Status"],
                                IsFeatured = (bool)reader["IsFeatured"],
                                DisplayOrder = (int)reader["DisplayOrder"],
                            };
                            Blogs.Add(Blog);
                        }
                        // Move to the second result set to get the total count
                        reader.NextResult();
                        int totalRecords = 0;
                        if (reader.Read())
                        {
                            totalRecords = reader.GetInt32(0);
                        }
                        var result = new
                        {
                            draw = draw,
                            recordsTotal = totalRecords,
                            recordsFiltered = totalRecords,
                            data = Blogs.OrderByDescending(x => x.DisplayOrder)
                        };
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetGalleryData(int draw, int start, int length, int dropdownId)
        {
            List<Gallery> Galleries = new List<Gallery>();
            try
            {
                var sortColumnIndex = Request.Form.GetValues("order[0][column]")?.FirstOrDefault();
                var sortColumnDirection = Request.Form.GetValues("order[0][dir]")?.FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("GetGalleryData", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@Length", length);
                    command.Parameters.AddWithValue("@dropdownId", dropdownId);
                    command.Parameters.AddWithValue("@sortColumnIndex", sortColumnIndex);
                    command.Parameters.AddWithValue("@sortColumnDirection", sortColumnDirection);
                    command.Parameters.AddWithValue("@SearchValue", string.IsNullOrEmpty(searchValue) ? (object)DBNull.Value : searchValue);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new Gallery
                            {
                                Id = (int)reader["Id"],
                                DefaultImage = Convert.ToString(reader["DefaultImage"]),
                                Title = Convert.ToString(reader["Title"]),
                                CreatedBy = Convert.ToString(reader["CreatedBy"]),
                                Status = (bool)reader["Status"],
                                CreatedOnString = Convert.ToDateTime(reader["CreatedOn"]).ToString("dd MMM yyyy"),
                                EditedOnString = Convert.ToDateTime(reader["EditedOn"]).ToString("dd MMM yyyy"),
                            };
                            Galleries.Add(data);
                        }
                        reader.NextResult();
                        int totalRecords = 0;
                        if (reader.Read())
                        {
                            totalRecords = reader.GetInt32(0);
                        }
                        var result = new
                        {
                            draw = draw,
                            recordsTotal = totalRecords,
                            recordsFiltered = totalRecords,
                            data = Galleries.OrderByDescending(x => x.Id)
                        };

                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}