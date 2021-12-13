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

    // Dependency Injection (DI 종속성 주입)
    // 디자인 패턴에서 코드간 종속성을 줄이는 것을 중요하게 생각
    public class FileLogSettings
    {
        string _filename;
        public FileLogSettings(string filename)
        {
            _filename = filename;
        }
    }
    public class FIleLogger
    {
        FileLogSettings _settings;
        public FIleLogger(FileLogSettings settings)
        {
            _settings = settings;
        }

        public void Log(string log)
        {
            Console.WriteLine($"Log Ok {log}");
        }
    }

    [Route("Home")]
    public class HomeController : Controller //Helper함수를 사용하려면 Controller를 참조해야함
    {
        
        [Route("Index")]
        public IActionResult Index()
        {
            FIleLogger logger = new FIleLogger(new FileLogSettings("log.txt"));
            logger.Log("Log Test");

            return Ok();
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            ViewData["Message"] = "Data From Privacy";
            return View();
        }

        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
