using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          //  throw new Exception("This is unhandled exception");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HandleError]
        public ActionResult Contact()
        {
            string msg = null;
            ViewBag.Message = msg.Length;

            return View();
        }
    }
}