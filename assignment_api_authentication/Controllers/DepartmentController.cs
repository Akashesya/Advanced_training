using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data.Context;
using WebApplication4.Data.Entities;


namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/department
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        // GET: api/department/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var dept = await _context.Departments
                                                 .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (dept == null)
                return NotFound();

            return dept;
        }


        // POST: api/department
        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentId }, department);
        }

        // PUT: api/department/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
                return BadRequest();

            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null)
                return NotFound();

            _context.Departments.Remove(dept);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
