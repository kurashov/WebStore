using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebStore.Model;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            
        }

        public EmployeeViewModel( Employee employee )
        {
            Id = employee.Id;
            Surname = employee.Surname;
            Name = employee.Name;
            Patronymic = employee.Patronymic;
            BirthDateTime = employee.BirthDateTime;
            Age = employee.Age;
        }

        [DisplayName( "Id" )]
        public int Id { get; set; }

        [DisplayName( "Фамилия" )]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя не может быть пустым")]
        public string Surname { get; set; }

        [DisplayName( "Имя" )]
        public string Name { get; set; }

        [DisplayName( "Отчество" )] 
        public string Patronymic { get; set; }

        [DisplayName( "Дата рождения" )]
        public DateTime BirthDateTime { get; set; }

        [DisplayName( "Возраст" )]
        public int Age { get; }
    }
}
