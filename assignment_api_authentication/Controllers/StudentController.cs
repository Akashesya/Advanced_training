using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data.Context;
using WebApplication4.Data.Entities;
//using WebApplication4.Data.Entities;

namespace WebApplication4.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/student
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        //{
        //    return await _context.Students.ToListAsync();
        //}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetStudents()
        {
            var students = await _context.Students
                .Include(s => s.Department)  // Join with Department table
                .Select(s => new
                {
                    s.StudentId,
                    s.StudentName,
                    DepartmentName = s.Department.DepartmentName
                })
                .ToListAsync();

            return Ok(students);
        }
        [HttpGet("department/{deptId}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByDepartment(int deptId)
        {
            var students = await _context.Students
                .Where(s => s.DepartmentId == deptId)  // LINQ WHERE
                .ToListAsync();

            return Ok(students);
        }


        // GET: api/student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students
                                                 .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null)
                return NotFound();

            return student;
        }
        [HttpGet("sorted")]
        public async Task<ActionResult<IEnumerable<Student>>> GetSortedStudents()
        {
            var students = await _context.Students
                .OrderBy(s => s.StudentId)  // LINQ ORDERBY
                .ToListAsync();

            return Ok(students);
        }

        [HttpGet("count-by-department")]
        public async Task<ActionResult<IEnumerable<object>>> GetStudentCountByDepartment()
        {
            var result = await _context.Students
                .Include(s => s.Department)
                .GroupBy(s => s.Department.DepartmentName)
                .Select(g => new
                {
                    Department = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return Ok(result);
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Student>>> SearchStudents(string name)
        {
            var students = await _context.Students
                .Where(s => s.StudentName.Contains(name))  // LINQ Contains (like SQL LIKE)
                .ToListAsync();

            return Ok(students);
        }
        [HttpGet("basic")]
        public async Task<ActionResult<IEnumerable<object>>> GetBasicStudentInfo()
        {
            var result = await _context.Students
                .Select(s => new { s.StudentName, s.DepartmentId })
                .ToListAsync();

            return Ok(result);
        }


        // POST: api/student
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
        }

        // PUT: api/student/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.StudentId)
                return BadRequest();

            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
