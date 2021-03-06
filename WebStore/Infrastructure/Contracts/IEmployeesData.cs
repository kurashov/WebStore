﻿using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Contracts
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> Get();

        Employee GetById( int id );

        int Add( Employee employee );

        void Edit( Employee employee );

        bool Delete( int id );

        void SaveChanges();
    }
}
