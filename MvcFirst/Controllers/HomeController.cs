using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcFirst.Models;

namespace MvcFirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Test(string x, string y)
        {
            ViewData["x"] = x;
            ViewBag.y = y;

            List<TestModel> list = new List<TestModel>();
            list.Add(new TestModel() { X = 1, Y = "a" });
            list.Add(new TestModel() { X = 2, Y = "b" });
            list.Add(new TestModel() { X = 3, Y = "c" });
            list.Add(new TestModel() { X = 4, Y = "d" });

            return View(list);
            //return Json(model);
        }
    }
}
