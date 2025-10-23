using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApplication4.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}

