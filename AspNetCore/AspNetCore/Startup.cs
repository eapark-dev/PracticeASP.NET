using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // 각종 서비스 추가 (DI) 영업 시
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // DI 서비스란 ? SRP (Single Responsiblility Principle)
            // ex) 랭킹 관련 기능이 필요하면 -> 랭킹 서비스
        }

        // Http Request Pipeline (NodeJs와 유사)
        // 미들웨어 : Http request / response를 처리하는 중간 부품

        // [Request]
        // [파이프라인]
        // [마지막 Mbc endpoint]

        // 미들웨어에서 처리한 결과물을 다른 미들웨어로 넘길 수 있다.
        // [파이프라인]

        //[!] Controller에서 처리하지 않는 이유는?
        //어떤 미들웨어에서 에러가 발생하면 다시 위로 쭉 에러를 전파시킨다.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/Home/Error");
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            app.UseHttpsRedirection();
            //Css, Javascript, 이미지 등 요청 받을 때 처
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //라우팅 패턴 설정
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
