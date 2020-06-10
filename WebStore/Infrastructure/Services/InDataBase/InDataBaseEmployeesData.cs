using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Contexts;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InDataBase
{
    public class InDataBaseEmployeesData : IEmployeesData
    {
        private readonly WebStoreDbContext _dbContext;

        public InDataBaseEmployeesData( WebStoreDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Employee> Get() => _dbContext.Employees.AsEnumerable();

        public Employee GetById( int id ) => _dbContext.Employees.Find( id );

        public int Add( Employee employee )
        {
            if( employee is null )
            {
                throw new ArgumentException(nameof(employee));
            }

            if( employee.Id != 0 )
            {
                throw new InvalidOperationException("Id should be equal 0");
            }

            _dbContext.Employees.Add(employee);
            return employee.Id;
        }

        public void Edit( Employee employee )
        {
            if( employee is null )
            {
                throw new ArgumentException(nameof(employee));
            }

            _dbContext.Attach(employee);
            _dbContext.Entry(employee).State = EntityState.Modified;
        }

        public bool Delete( int id )
        {
            var employee = GetById( id );
            _dbContext.Employees.Remove( employee );
            return true;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}