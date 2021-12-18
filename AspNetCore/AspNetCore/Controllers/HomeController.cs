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
    // 종속성
    // 생성자에서 new를 해서 직접 만들어주지 않아도 됨
    // 특정 인터페이스 A에 대해서 B라는 구현을 사용하라

    // 1)Request
    // 2) Routing
    // 3) Controller Activator (DI Container한테 Controller 생성 + 알맞는 Dependency 연결 위탁)
    // 4) DI Container 임무 실행
    // 5) Controller가 생성 시작

    // 만약 3번에서 요청한 Dependency를 못찾으면 에러
    // ConfigureServices에서 등록을 해야한다

    //Razor View Template에서도 서비스가 필요하다?
    // 이 경우 생성자를 아예 사용할 수 없으니
    // @inject

    // LifeTime
    // DI Container에 특정 서비스를 달라고 요청하면
    // 1) 만들어서 반환하거나
    // 2) 있는걸 반환하거나
    // 즉, 서비스 instance를 재 사용할지 말지를 결정

    // Transient (항상 새로운 서비스 Instance를 만든.
    // Scoped
    // Singleton (항상 동일한 Instance를 사용. )
    public interface IBaseLogger
    {
        public void Log(string log);
    }

    public class DbLogger : IBaseLogger
    {
        public DbLogger() { }
        public void Log(string log)
        {
            Console.WriteLine($"Log Ok {log}");
        }
    }

    public class FileLogSettings
    {
        string _filename;
        public FileLogSettings(string filename)
        {
            _filename = filename;
        }
    }
    public class FIleLogger : IBaseLogger
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
        IEnumerable<IBaseLogger> _looger;

        public HomeController(IEnumerable<IBaseLogger> logger)
        {
            _looger = logger;
        }


        [Route("Index")]
        [Route("/")]
        public IActionResult Index()
        {
            //_looger.Log("Log Test");

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
