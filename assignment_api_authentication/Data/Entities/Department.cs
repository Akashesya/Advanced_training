namespace WebApplication4.Data.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        // Navigation Property
        public List<Student> ?Students { get; set; } = new List<Student>();
    }
}
