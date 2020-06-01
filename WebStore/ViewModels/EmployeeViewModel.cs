using System;
using System.ComponentModel;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel
    {
        [DisplayName( "Id" )]
        public int Id { get; set; }

        [DisplayName( "Фамилия" )]
        public string Surname { get; set; }

        [DisplayName( "Имя" )]
        public string Name { get; set; }

        [DisplayName( "Отчество" )] 
        public string Patronymic { get; set; }

        [DisplayName( "Дата рождения" )]
        public DateTime BirthDateTime { get; set; }

        [DisplayName( "Возраст" )]
        public int Age { get; set; }
    }
}
