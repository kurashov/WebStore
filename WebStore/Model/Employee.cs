using System;
using System.Runtime.CompilerServices;

namespace WebStore.Model
{
    public class Employee
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public DateTime BirthDateTime { get; set; }

        public int Age => (int) Math.Floor( ( DateTime.Now - BirthDateTime ).TotalDays / 365.25 );
    }
}
