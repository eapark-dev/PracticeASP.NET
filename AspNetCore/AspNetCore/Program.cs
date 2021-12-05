using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCore
{
    //Program 거시적인 설정 (Http 서버, IIs 사용 여부 등, 거의 바뀌지 않음)
    //Startup은 세부적인 설정 (미들웨어 설정, Dependency Injection 등, 필요에 따라 설정)
    public class Program
    {
        public static void Main(string[] args)
        {
            // 2) IHost를 만든다.
            // 3) 구동 (Run) < 이때부터 Listen을 시작
            CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //Host.CreateDefaultBuilder(args)
        // .ConfigureWebHostDefaults(webBuilder =>
        //{
        //  webBuilder.UseStartup<Startup>();
        // });

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            //1) 각종 옵션 설정 세팅
            return  Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                // 2) Startup 클래스 지정
            webBuilder.UseStartup<Startup>();
            });
        }
    }
}
