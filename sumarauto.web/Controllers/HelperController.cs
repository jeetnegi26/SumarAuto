using sumarauto.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace sumarauto.web.Controllers
{
    public class HelperController : Controller
    {
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
        #endregion
    }
}