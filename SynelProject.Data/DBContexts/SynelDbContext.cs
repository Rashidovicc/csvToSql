using Microsoft.EntityFrameworkCore;
using SynelProject.Domain.Models;

namespace SynelProject.Data.DBContexts
{
    public class SynelDbContext : DbContext
    {
        public SynelDbContext(DbContextOptions<SynelDbContext> options)
            :base(options)
        {
            
        }

         public DbSet<Employee> Employees { get; set; } 
    

    }
}