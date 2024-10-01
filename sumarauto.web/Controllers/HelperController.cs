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

namespace sumarauto.web.Controllers
{
    public class HelperController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sumarautoDb"].ConnectionString;
        #region Helper
        [HttpPost]
        public ActionResult FeaturedChange(bool value,int Id)
        {
            bool result = false;
            try
            {
                using (var db = new AppDbContext())
                {
                    var Category = db.Category.Find(Id);
                    if (Category != null)
                    {
                        Category.IsFeatured = value;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return Json(new{ Result = result }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult StatusChange(bool value, int Id, string Type)
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
                                Category.Status = value;
                                db.SaveChanges();
                            }
                            result = true;
                            break;
                        case "Make":
                            var Make = db.Make.Find(Id);
                            if (Make != null)
                            {
                                Make.Status = value;
                                db.SaveChanges();
                            }
                            result = true;
                            break;
                        case "MModel":
                            var MModel = db.MModel.Find(Id);
                            if (MModel != null)
                            {
                                MModel.Status = value;
                                db.SaveChanges();
                            }
                            result = true;
                            break;
                        case "AutoPart":
                            var AuModel = db.AutoPart.Find(Id);
                            if (AuModel != null)
                            {
                                AuModel.Status = value;
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
        public ActionResult GetSelectListCat()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            //Method
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSelectListEngine()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            //Method
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSelectListMake()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            //Method
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSelectListMMade()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            //Method
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSelectListChassis()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            //Method
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSelectListLiter()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            //Method
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSelectListYear()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            //Method
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetDropdownCatList()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("GetCategoryList", connection);
                    command.CommandType = CommandType.StoredProcedure;
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
                    var dataList = await db.Make.AsNoTracking().ToListAsync();
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
        #endregion
    }
}