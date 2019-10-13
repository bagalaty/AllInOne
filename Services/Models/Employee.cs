using System;
using System.ComponentModel.DataAnnotations;

namespace Services.Models
{

    public class Employee: BaseModel
    {
        [Key, MaxLength(38)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public Gender? Gender { get; set; }
    }

    public enum Gender
    {
        NotSelected,
        Male,
        Female
    }

}
