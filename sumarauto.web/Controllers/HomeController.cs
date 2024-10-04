using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;
using System.Threading.Tasks;
using Model;

namespace sumarauto.web.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sumarautoDb"].ConnectionString;
        public ActionResult Index()
        {

            return View();
        }

        public async Task<HomeViewModel> GetHomePageData()
        {
            var homeViewModel = new HomeViewModel
            {
                Banners = new List<Banner>(),
                Categories = new List<SelectListItem>(),
                Makes = new List<SelectListItem>()
            };

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("GetHomePageData", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    connection.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // First result set: Banners
                        while (reader.Read())
                        {
                            homeViewModel.Banners.Add(new Banner
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Heading = Convert.ToString(reader["Heading"]),
                                Subheading = Convert.ToString(reader["Subheading"]),
                                Image = Convert.ToString(reader["Image"]),
                                ButtonText = Convert.ToString(reader["ButtonText"]),
                                url = Convert.ToString(reader["url"]),
                            });
                        }

                        // Move to the next result set (Categories)
                        if (await reader.NextResultAsync())
                        {
                            while (reader.Read())
                            {
                                homeViewModel.Categories.Add(new SelectListItem
                                {
                                    Value = reader["Id"].ToString(),
                                    Text = reader["Title"].ToString()
                                });
                            }
                        }

                        // Move to the next result set (Makes)
                        if (await reader.NextResultAsync())
                        {
                            while (reader.Read())
                            {
                                homeViewModel.Makes.Add(new SelectListItem
                                {
                                    Value = reader["MakeId"].ToString(),
                                    Text = reader["Title"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return homeViewModel;
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Categories()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Blogs()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Blog()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult _ServiceForm()
        {
            return PartialView();
        }
    }
}