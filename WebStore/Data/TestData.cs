using System;
using System.Collections.Generic;
using WebStore.Model;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees => new List<Employee>()
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
}
