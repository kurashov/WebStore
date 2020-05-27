using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Model;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        

        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
