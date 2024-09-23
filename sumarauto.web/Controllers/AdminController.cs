using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
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

        #region Category
        public ActionResult Category() {
            List<Category> categories = new List<Category>();
            try
            {
                using(var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("GetCategoryList", connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) 
                        {
                            Category category = new Category
                            {
                                Title = Convert.ToString(reader["Title"]),
                                Image = Convert.ToString(reader["Image"]),
                                Description = Convert.ToString(reader["Description"]),
                                Status = (bool)reader["Status"],
                                CreatedOn = (DateTime)reader["CreatedOn"],
                                CreatedOnString = ((DateTime)reader["CreatedOn"]).ToString("dd MMM yyyy"),
                                EditedOnString = ((DateTime)reader["EditedOn"]).ToString("dd MMM yyyy"),
                            };

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(categories);  
        }
        public ActionResult CategoryAction(int Id=0)
        {
            var data = new Category();
            try
            {
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
            try
            {
                return View(data);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult CategoryStatus()
        {
            return View();
        }
        #endregion

        #region AutoParts
        public ActionResult AutoPart()
        {
            return View();
        }
        public ActionResult AutoPartAction(int Id = 0)
        {
            return View();
        }
        [HttpPost]
        public ActionResult AutoPartAction(Category category)
        {
            return View();
        }
        public ActionResult AutoPartStatus()
        {
            return View();
        }
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
    }
}