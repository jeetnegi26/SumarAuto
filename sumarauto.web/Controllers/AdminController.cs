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
using Model;
using sumarauto.database;
using sumarauto.DataModel;

namespace sumarauto.web.Controllers
{
    public class AdminController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sumarautoDb"].ConnectionString;
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        #region AutoParts
        public ActionResult AutoPart()
        {
            return View();
        }
        public ActionResult AutoPartAction(int Id = 0)
        {
            int width = Convert.ToInt32(ConfigurationManager.AppSettings["Width"]);
            int height = Convert.ToInt32(ConfigurationManager.AppSettings["Height"]);
            ViewBag.Width = width;
            ViewBag.Height = height;
            return View();
        }
        [HttpPost]
        public ActionResult saveOrUpdateAutoParts(AutoPartDataModel model)
        {
            bool result = false;
            string Message = "Something went wrong!";
            try
            {
                if (ModelState.IsValid)
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
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@CategoryId", model.CategoryId);
                    cmd.Parameters.AddWithValue("@DisplayOrder", model.DisplayOrder);
                    cmd.Parameters.AddWithValue("@CreatedBy", "ADMIN");
                    cmd.Parameters.AddWithValue("@UserHostAddress", Request.UserHostAddress);

                    // Table-valued parameter for multiple details
                    var table = new DataTable();
                    table.Columns.Add("Make", typeof(string));
                    table.Columns.Add("Model", typeof(string));
                    table.Columns.Add("Year", typeof(string));
                    table.Columns.Add("Engine", typeof(string));
                    table.Columns.Add("Liter", typeof(string));
                    table.Columns.Add("Chassis", typeof(string));

                    foreach (var detail in model.MultipleDetails)
                    {
                        if (!string.IsNullOrEmpty(detail.Make))
                        {
                            table.Rows.Add(detail.Make, detail.Model, detail.Year, detail.Engine, detail.Liter, detail.Chassis);
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

        //Make
        public ActionResult MakeList(string Id)
        {
            List<MakeDetail> makeDetails = new List<MakeDetail>();
            try
            {
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
                                AutoPartSId = Id,
                                Make = Convert.ToString(reader["Make"]),
                                Chassis = Convert.ToString(reader["Chassis"]),
                                Engine = Convert.ToString(reader["Engine"]),
                                Liter = Convert.ToString(reader["Liter"]),
                                Model = Convert.ToString(reader["Model"]),
                                Year = Convert.ToString(reader["Year"]),
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
            return View(makeDetails.OrderBy(x => x.DisplayOrder)
                      .ToList());
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
                            data.DisplayOrder = Convert.ToString(reader["DisplayOrder"]);
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

        #region Helper

        #endregion
    }
}