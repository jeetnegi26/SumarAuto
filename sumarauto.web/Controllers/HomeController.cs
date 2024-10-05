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
using sumarauto.database;

namespace sumarauto.web.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sumarautoDb"].ConnectionString;
        public ActionResult Index()
        {

            return View();
        }

        public async Task<ActionResult> GetHomePageData()
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
            return Json(homeViewModel,JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetIsFeaturedMakeAndCat()
        {
            var homeViewModel = new IsFeaturedMakeAndCat
            {
                Categories = new List<Category>(),
                Makes = new List<Make>()
            };
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("GetIsFeaturedMakeAndCat", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    connection.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // First result set: Banners
                        while (reader.Read())
                        {
                            homeViewModel.Categories.Add(new Category
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = Convert.ToString(reader["Title"]),
                                Description = Convert.ToString(reader["Description"]),
                                Image = Convert.ToString(reader["Image"]),
                            });
                        }
                        // Move to the next result set (Makes)
                        if (await reader.NextResultAsync())
                        {
                            while (reader.Read())
                            {
                                homeViewModel.Makes.Add(new Make
                                {
                                    MakeId = Convert.ToInt32(reader["MakeId"]),
                                    Title = Convert.ToString(reader["Title"]),
                                    Image = Convert.ToString(reader["Image"]),
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
            return Json(homeViewModel, JsonRequestBehavior.AllowGet);
        }
        [Route("about")]
        public ActionResult About()
        {
            return View();
        }
        public async Task<ActionResult> GetClientsAndGallery()
        {
            var clientandgallery = new ClientAndGallery
            {
                Clients = new List<ClientAndGalleryItem>(),
                Gallery = new List<ClientAndGalleryItem>()
            };
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("GetClientsAndGallery", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    connection.Open();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // First result set: Banners
                        while (reader.Read())
                        {
                            clientandgallery.Clients.Add(new ClientAndGalleryItem
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = Convert.ToString(reader["Title"]),
                                Image = Convert.ToString(reader["Image"]),
                            });
                        }
                        // Move to the next result set (Makes)
                        if (await reader.NextResultAsync())
                        {
                            while (reader.Read())
                            {
                                clientandgallery.Gallery.Add(new ClientAndGalleryItem
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Title = Convert.ToString(reader["Title"]),
                                    Image = Convert.ToString(reader["Image"]),
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
            return Json(clientandgallery, JsonRequestBehavior.AllowGet);
        }
        [Route("contact-us")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Contact(ContactForm model)
        {
            try
            {
                var result = "* All fields are required to fill.";
                if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Name) && !string.IsNullOrEmpty(model.Phone))
                {
                    using (var context = new AppDbContext())
                    {
                        model.CreatedOn = DateTime.Now;
                        model.EditedOn = DateTime.Now;
                        model.UserHostAdd = Request.UserHostAddress;
                        context.ContactForm.Add(model);
                        await context.SaveChangesAsync();
                    }
                    result = "true";
                    if (result == "true")
                    {
                        var brand = "St. Jude Elite Care";
                        var helperController = new HelperController();
                        string messageForUser = "<p>Thank you for reaching out to us! Your message has been received. If you're an existing member, we appreciate your continued engagement. Our team will review your inquiry and respond as soon as possible.</p>";
                        EmailTemplateModel RegisteremailUser = new EmailTemplateModel()
                        {
                            Message = messageForUser,
                            Destination = model.Email,
                            Subject = "Thank You for Contacting " + brand + "- We've Received Your Message!"
                        };
                        await helperController.SendEmail(RegisteremailUser);

                        string messageForAdmin = "<p>Name: " + model.Name +
                            "<br/>Phone No: " + model.Phone +
                            "<br/>Email: <a href=\"mailto:" + model.Email + "\">" + model.Email +
                            "<br/>Address: " + model.Address +
                            "<br/>Country: " + model.Country +
                            "<br/>Message: " + model.Comment +
                            " </p>";
                        EmailTemplateModel RegisteremailAdmin = new EmailTemplateModel()
                        {
                            Message = messageForAdmin,
                            Destination = ConfigurationManager.AppSettings["email"].ToString(),
                            Subject = "Contact form from" + model.Name,
                        };
                        await helperController.SendEmail(RegisteremailAdmin);
                    }
                }
                return Json(result);
            }
            catch (Exception)
            {
                return Json("Sorry! Something went wrong.");
            }
        }



        [Route("categories")]
        public ActionResult Categories()
        {
            using(var db = new AppDbContext())
            {
                var data = db.Category.AsNoTracking().Where(x => x.Status == true).ToList();
                return View(data);
            }
        }
        [Route("blogs")]
        public ActionResult Blogs()
        {
            try
            {
                var blogViewModel = new BlogsRecentAndFeatured
                {
                    Recent = new List<Blogs>(),
                    Featured = new List<Blogs>(),
                };
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        var cmd = new SqlCommand("GetBlogsRecentAndFeatured", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        connection.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            // First result set: Banners
                            while (reader.Read())
                            {
                                blogViewModel.Featured.Add(new Blogs
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Title = Convert.ToString(reader["Title"]),
                                    Description = Convert.ToString(reader["Description"]),
                                    Image = Convert.ToString(reader["Image"]),
                                    Date = Convert.ToDateTime(reader["Date"]),
                                });
                            }
                            // Move to the next result set (Makes)
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    blogViewModel.Recent.Add(new Blogs
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        Title = Convert.ToString(reader["Title"]),
                                        Description = Convert.ToString(reader["Description"]),
                                        Image = Convert.ToString(reader["Image"]),
                                        Date = Convert.ToDateTime(reader["Date"]),
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
                return View(blogViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Route("blog")]
        public ActionResult Blog(int bId)
        {
            using(var db = new AppDbContext())
            {
                var data = db.Blogs.AsNoTracking().FirstOrDefault(x=>x.Id == bId);
                return View(data);
            }
        }

        //Partial Pages
        public ActionResult _AboutPartial()
        {
            return PartialView();
        }
        public PartialViewResult _BlogsPartial()
        {

            var blogs = new List<Blogs>();
            try
            {
                using (var db = new AppDbContext())
                {
                    blogs = db.Blogs.AsNoTracking().Where(x => x.Status == true
                    && x.IsFeatured == true).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return PartialView(blogs);
        }
        public ActionResult _ServiceForm()
        {
            return PartialView();
        }
    }
}