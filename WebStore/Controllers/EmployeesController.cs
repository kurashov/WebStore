﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Model;

namespace WebStore.Controllers
{
    [Route("Users")]
    public class EmployeesController : Controller
    {
        private readonly List<Employee> _employees;

        public EmployeesController()
        {
            _employees = new List<Employee>()
            {
                new Employee
                {
                    Id = 1,
                    Surname = "Иванов",
                    Name = "Иван",
                    Patronymic = "Иванович",
                    BirthDateTime = new DateTime( 1960, 1, 1 )
                },
                new Employee
                {
                    Id = 2,
                    Surname = "Петров",
                    Name = "Петр",
                    Patronymic = "Петрович",
                    BirthDateTime = new DateTime( 1970, 2, 2 )
                },
                new Employee
                {
                    Id = 3,
                    Surname = "Сидоров",
                    Name = "Сидр",
                    Patronymic = "Сидорович",
                    BirthDateTime = new DateTime( 1980, 3, 3 )
                }
            };
        }

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
