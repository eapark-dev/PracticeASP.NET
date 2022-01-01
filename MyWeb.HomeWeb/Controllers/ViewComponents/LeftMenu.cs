using System;
using Microsoft.AspNetCore.Mvc;

namespace MyWeb.HomeWeb.Controllers.ViewComponents
{
    public class LeftMenu : ViewComponent
    {
        public LeftMenu()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
