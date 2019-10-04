namespace Services.Models
{

    public class Employee: BaseModel
    {
        public int Id { get; set; }
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
