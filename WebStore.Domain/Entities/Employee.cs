using System;
using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Interfaces;

namespace WebStore.Domain.Entities
{
    public class Employee : IBaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Name { get; set; }

        public string Patronymic { get; set; }

        [Required]
        public DateTime BirthDateTime { get; set; }

        public int Age => (int) Math.Floor( ( DateTime.Now - BirthDateTime ).TotalDays / 365.25 );
    }
}
