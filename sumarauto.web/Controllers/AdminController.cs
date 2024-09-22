using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace sumarauto.web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region Category
        public ActionResult Category() { 
            return View();  
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