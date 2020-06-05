using System;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Model;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesController( IEmployeesData employeesData )
        {
            _employeesData = employeesData;
        }

        public IActionResult Index()
        {
            return View(_employeesData.Get());
        }

        public IActionResult EmployeeDetails(int id)
        {
            var employee = _employeesData.GetById( id );
            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.Title = "Информация о сотруднике";

            var employeeVm = new EmployeeViewModel( employee );

            return View(employeeVm);
        }

        #region Edit

        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return View(new EmployeeViewModel());
            }

            if (id < 0)
            {
                return BadRequest();
            }

            var employee = _employeesData.GetById(id.Value);

            if (employee is null)
            {
                return NotFound();
            }

            return View(new EmployeeViewModel(employee));
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if( !ModelState.IsValid )
            {
                return View( viewModel );
            }

            var employee = new Employee
            {
                Id = viewModel.Id,
                Surname = viewModel.Surname,
                Name = viewModel.Name,
                Patronymic = viewModel.Patronymic,
                BirthDateTime = viewModel.BirthDateTime
            };

            if (employee.Id == 0)
            {
                _employeesData.Add(employee);
            }
            else
            {
                _employeesData.Edit(employee);
            }

            _employeesData.Commit();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var employee = _employeesData.GetById(id);
            if (employee is null)
            {
                return NotFound();
            }

            return View(new EmployeeViewModel(employee));
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeesData.Delete(id);
            _employeesData.Commit();

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
