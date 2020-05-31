using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Model;

namespace WebStore.Controllers
{
    [Route("Staff")]
    public class EmployeesController : Controller
    {
        private readonly List<Employee> _employees = TestData.Employees;

        public IActionResult Index()
        {
            return View(_employees);
        }

        [Route("{id}")]
        public IActionResult EmployeeDetails(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.Title = "Информация о сотруднике";

            return View(employee);
        }

    }
}
