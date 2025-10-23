using WebApplication4.Data.Entities;

namespace WebApplication4.Data.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        // Foreign key to Department
        public int DepartmentId { get; set; }

        // Navigation Property
        public Department ?Department { get; set; }
    }
}
