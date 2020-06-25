using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Contracts;

namespace WebStore.Infrastructure.Services.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List<Employee> _employees = TestData.Employees;

        public IEnumerable<Employee> Get()
        {
            return _employees;
        }

        public Employee GetById( int id )
        {
            return _employees.FirstOrDefault( e => e.Id == id );
        }

        public int Add( Employee employee )
        {
            if( employee is null )
            {
                throw new ArgumentNullException( nameof(employee) );
            }

            if( _employees.Contains( employee ) )
            {
                return employee.Id;
            }

            employee.Id = _employees.Count == 0 ?
                1 :
                _employees.Max( e => e.Id ) + 1;

            _employees.Add(employee);
            return employee.Id;
        }

        public void Edit( Employee employee )
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            if (_employees.Contains(employee))
            {
                return;
            }

            var dbItem = GetById( employee.Id );

            dbItem.Surname = employee.Surname;
            dbItem.Name = employee.Name;
            dbItem.Patronymic = employee.Patronymic;
            dbItem.BirthDateTime = employee.BirthDateTime;
        }

        public bool Delete( int id )
        {
            var dbItem = GetById( id );

            if( dbItem is null )
            {
                return false;
            }

            return _employees.Remove( dbItem );
        }

        public void SaveChanges()
        {
            //do nothing
        }
    }
}
