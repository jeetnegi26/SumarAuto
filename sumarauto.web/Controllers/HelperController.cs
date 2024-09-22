using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sumarauto.web.Controllers
{
    public class HelperController : Controller
    {
        #region Helper
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