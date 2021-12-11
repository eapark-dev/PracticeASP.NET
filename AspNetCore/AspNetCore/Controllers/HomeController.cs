using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCore.Models;

namespace AspNetCore.Controllers
{
  
    public class HomeController : Controller //Helper함수를 사용하려면 Controller를 참조해야함
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult BuyItem()
        {
            return View();
        }

        public IActionResult Test()
        {
            TestViewModel testViewModel = new TestViewModel()
            {
                Id = 1005,
                Count = 2
            };
            return View(testViewModel); 
        }

        public IActionResult Index()
        {
            //string url = Url.Action("Privacy", "Home");
            //string url = Url.RouteUrl("test", new { test = 123 });
            return RedirectToAction("Privacy");
            
            //return View(); //ViewResult를 반환하는 값 (Helper함수)
        }

        public IActionResult Privacy()
        {
            ViewData["Message"] = "Data From Privacy";
            return View();
        }

        //[Route("Hello")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
