using Microsoft.EntityFrameworkCore;
using MedMinder_Api.Models;

namespace MedMinder_Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }

        public DbSet<Patient> Patients => Set<Patient>();
       
    }
}