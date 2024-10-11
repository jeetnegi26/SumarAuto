using DataModel;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace sumarauto.web.Controllers
{
    public class ProductsController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sumarautoDb"].ConnectionString;
        // GET: Products
        [Route("Products")]
        public ActionResult Index(string Category, string Brand, string Model, string Years, string Engine, string Liters, string Chassis, string Search)
        {
            TempData["Category"] = Category;
            TempData["Brand"] = Brand;
            TempData["Model"] = Model;
            TempData["Years"] = Years;
            TempData["Engine"] = Engine;
            TempData["Liters"] = Liters;
            TempData["Chassis"] = Chassis;
            TempData["Search"] = Search;
            return View();
        }
        [Route("Product/{ProTitle}")]
        public async Task<ActionResult> Product(string ProTitle)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("GetFinalProduct", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Title", ProTitle);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        FinalProduct product = new FinalProduct();
                        while (reader.Read())
                        {
                            product.Id = (int)reader["AutoPartId"];
                            product.Title = reader["Title"].ToString();
                            product.Description = Convert.ToString(reader["Description"]);
                            product.Package = Convert.ToString(reader["Package"]);
                            product.FinalProductMake = new List<FinalProductMake>();
                            product.FinalProductImgs = new List<FinalProductImgs>();
                        }

                        // Move to the next result set (Categories)
                        if (await reader.NextResultAsync())
                        {
                            while (reader.Read())
                            {
                                if (product != null)
                                {
                                    product.FinalProductImgs.Add(new FinalProductImgs
                                    {
                                        Image = Convert.ToString(reader["Image"]),
                                        Default = Convert.ToBoolean(reader["Default"])
                                    });
                                }
                            }
                        }
                        if (await reader.NextResultAsync())
                        {
                            while (reader.Read())
                            {
                                if (product != null)
                                {
                                    product.FinalProductMake.Add(new FinalProductMake
                                    {
                                        AutoPArtMakeID = product.Id,
                                        Make = Convert.ToString(reader["Make"]),
                                        Model = Convert.ToString(reader["Model"]),
                                        Year = Convert.ToString(reader["Year"]),
                                        Engine = Convert.ToString(reader["Engine"]),
                                        Chassis = Convert.ToString(reader["Chassis"]),
                                    });
                                }
                            }
                        }
                        TempData["Title"] = product.Title;
                        return View(product);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> GetAllFilterProducts(int draw, int start, int length, string Category, string Brand, string Model, string Years, string Engine, string Liters, string Chassis, string Search)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("GetAllFilterProducts", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Start", start);
                        command.Parameters.AddWithValue("@Length", length);
                        command.Parameters.AddWithValue("@CatId", (object)Category ?? DBNull.Value); // Ensure correct handling of nulls
                        command.Parameters.AddWithValue("@MakeId", (object)Brand ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ModelTitle", (object)Model ?? DBNull.Value);
                        command.Parameters.AddWithValue("@YearTitle", (object)Years ?? DBNull.Value);
                        command.Parameters.AddWithValue("@EngineTitle", (object)Engine ?? DBNull.Value);
                        command.Parameters.AddWithValue("@LitersTitle", (object)Liters ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ChassisTitle", (object)Chassis ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Search", (object)Search ?? DBNull.Value);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<Product> products = new List<Product>();
                        int totalCount = 0;
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                Id = (int)reader["AutoPartId"],
                                Title = reader["Title"].ToString(),
                                Description = Convert.ToString(reader["Description"]),
                                ImageUrl = Convert.ToString(reader["ImageUrl"]),
                                Package = Convert.ToString(reader["Package"]),
                                RewriteUrl = Convert.ToString(reader["RewriteUrl"]),
                                ProductMake = new List<ProductMake>()
                            };
                            products.Add(product);
                        }

                        // Move to the next result set (Categories)
                        if (await reader.NextResultAsync())
                        {
                            while (reader.Read())
                            {
                                int productId = (int)reader["ProductId"];
                                Product product = products.FirstOrDefault(p => p.Id == productId);
                                if (product != null)
                                {
                                    product.ProductMake.Add(new ProductMake
                                    {
                                        ProductId = productId,
                                        Make = Convert.ToString(reader["Make_Title"]),
                                        Model = Convert.ToString(reader["Model_Title"]),
                                        Year = Convert.ToString(reader["Year_Title"])
                                    });
                                }
                            }
                        }
                        if (reader.NextResult())
                        {
                            // Reading the TotalCount result
                            if (reader.Read())
                            {
                                totalCount = (int)reader["TotalCount"];
                            }
                        }
                        return Json(new
                        {
                            draw = draw,
                            recordsTotal = totalCount,
                            recordsFiltered = totalCount,
                            products = products,
                            totalCount = totalCount
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult GetPartofSameModel(string Make, string Model, string Engine, string Chassis)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("GetPartofSameModel", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Make", (object)Make ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Model", (object)Model ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Engine", (object)Engine ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Chassis", (object)Chassis ?? DBNull.Value);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<Product> products = new List<Product>();
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                Id = (int)reader["Id"],
                                Title = reader["Title"].ToString(),
                                Package = Convert.ToString(reader["Package"]),
                                ImageUrl = Convert.ToString(reader["Image"]),
                            };
                            products.Add(product);
                        }
                        return Json(new
                        {
                            products
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}