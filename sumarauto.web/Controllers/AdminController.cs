using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

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
            return View();
        }
        [HttpPost]
        public ActionResult CategoryAction(Category category)
        {
            return View();
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

    }
}