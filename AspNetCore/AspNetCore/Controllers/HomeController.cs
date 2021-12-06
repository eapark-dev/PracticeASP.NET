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
    //데이터 모델
    //데이터 종류가 다양하다
    //-Binding Model
    //클라이언트에서 보낸 Request를 파싱하기 위한 데이터 모델 << 유효성 검증 필수
    //Applictaion Model
    //서버의 각종 서비스들이 사용하는 데이터 모델 (ex, RankingService라면 RankingDate)
    //View Model
    //Response UI를 만들기 위한 데이터 모델
    //API Model
    //WebAPI Controller에서 Json / XML으로 포맷으로 응답할 때 필요한 데이터 모델

    //Model Binding
    //1) Form Values
    // Request의 Body에서 보낸 값 (Http Post방식의 요청)
    //2) Routes Values
    // URL 매칭, Default Value
    //3) Query String Value
    // URL 끝에 붙이는 방법 ?Name=Rookiss (Http Get방식의 요청)

    public class HomeController : Controller //Helper함수를 사용하려면 Controller를 참조해야함
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Test(int id, string value)
        {
            return null;
        }


        public IActionResult Test2(TestModel testModel)
        {
            return null;
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
