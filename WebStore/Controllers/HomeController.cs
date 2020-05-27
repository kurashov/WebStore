using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Model;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult EmployeeDetails( int id )
        //{
        //    var employee = _employees.FirstOrDefault( e => e.Id == id );
        //    if( employee == null )
        //    {
        //        return NotFound();
        //    }

        //    ViewBag.Title = "Информация о сотруднике";

        //    return View(employee);
        //}

    }
}
