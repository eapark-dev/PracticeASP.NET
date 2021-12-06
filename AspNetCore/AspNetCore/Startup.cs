using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Routing.Constraints;
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

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //Css, Javascript, 이미지 등 요청 받을 때 처
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //라우팅 : 길잡이
            //Htttp request <-> 담당 Handler에게 넘겨주기 위해서 request주소와 담당자 사이에서 매핑하는 역할을 함

            //기본관례(Convertion)는 Controller/Action/Id 형식
            //다른 이름 지정하고 싶을 땐 -> API 서버로 사용할 때 URL 주소가 어떤 역할을 하고 있는 지 더 명확하게 힌트를 주고싶을 때 사용
            //굳이 Controller를 수정하지 않고 연결된 URL만 교체하고 싶을 때 사용
            //Routing이 적용되려면 [미들웨어 파이프라인]에 의해 전달되어야함
            //중간에 에러가 난다거나, 특정 미들웨어가 흐름을 가로챘다면 전달되지 않음
            //파이프라인 끝까지 도달했으면, MapControllerRoute에 의해 Routing 규칙이 결정
            //패턴을 이용한 방식으로 Routing
            //Attribute Routing

            //Route Template
            // name이 지정된다는 건 다수를 설정할 수 있음

            app.UseEndpoints(endpoints =>
            {
                //api : literal value (고정 문자열 값)
                //{controller} {action} : route parameter
                //{controller=Hone} {action=Index} : optional route parameter
                //{id} : optional route parameter

                //Metch-All
                //{*joker} *를 붙이면 모든 문자열을 다 매칭시켜준다.

                //Redirection : 다른 URL로 토스
                //Redirect(url) << URL 직접 만들어서 

                endpoints.MapControllerRoute(
                name: "test",
                pattern: "api/{test}",
                defaults: new { controller = "Home", action = "Privacy" },
                constraints: new { test = new IntRouteConstraint() });

                //라우팅 패턴 설정
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //가장 광범위한 애를 맨 밑으로 둔다.
                endpoints.MapControllerRoute(
                name: "joker",
                pattern: "{*joker}",
                defaults: new { controller = "Home", action = "Error" });

            });

        }
    }
}
