using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudentWebPortal.Model.Entity;

namespace StudentWebPortal.Data
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }
    }
}
