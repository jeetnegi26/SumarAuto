using Model;
using sumarauto.database;
using sumarauto.DataModel;
using sumarauto.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sumarauto.web.Controllers
{
    public class DataUpdateController : Controller
    {
        private readonly AppDbContext _context;
        private readonly CsvService _csvService;

        public DataUpdateController()
        {
            _context = new AppDbContext();
            _csvService = new CsvService();
        }

        [HttpGet]
        public ActionResult UploadCsv()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // Ensure the file is an Excel (.xlsx) file
                if (Path.GetExtension(file.FileName).ToLower() == ".xlsx")
                {
                    // Save the file to a temporary location
                    var filePath = Path.Combine(Server.MapPath("~/Content/Products"), file.FileName);
                    file.SaveAs(filePath);

                    // Read the Excel file and map it to the Make model
                    var makes = _csvService.ReadExcel(filePath); // Assuming _csvService is already instantiated

                    // Insert the records into the database
                    using (var context = new AppDbContext())
                    {
                        context.Make.AddRange(makes);  // Adding the list of Make objects to the database
                        context.SaveChanges();          // Save changes to the database
                    }

                    ViewBag.Message = "File uploaded and data saved successfully.";
                }
                else
                {
                    ViewBag.Message = "Invalid file format. Please upload an Excel (.xlsx) file.";
                }
            }
            else
            {
                ViewBag.Message = "Please upload a valid file.";
            }

            return View();
        }

        [HttpPost]
        public ActionResult UploadCategory(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // Ensure the file is an Excel (.xlsx) file
                if (Path.GetExtension(file.FileName).ToLower() == ".xlsx")
                {
                    // Save the file to a temporary location
                    var filePath = Path.Combine(Server.MapPath("~/Content/Products"), file.FileName);
                    file.SaveAs(filePath);

                    // Read the Excel file and map it to the Make model
                    var categories = _csvService.ReadCategory(filePath); // Assuming _csvService is already instantiated

                    // Insert the records into the database
                    using (var context = new AppDbContext())
                    {
                        context.Category.AddRange(categories);  // Adding the list of Make objects to the database
                        context.SaveChanges();          // Save changes to the database
                    }

                    ViewBag.Message = "File uploaded and data saved successfully.";
                }
                else
                {
                    ViewBag.Message = "Invalid file format. Please upload an Excel (.xlsx) file.";
                }
            }
            else
            {
                ViewBag.Message = "Please upload a valid file.";
            }

            return View();
        }

    }
}