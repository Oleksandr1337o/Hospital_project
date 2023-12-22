using Hospital.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUser {  get; set; }
        public DbSet<Job_title> JobTitle { get; set; }

        public DbSet<Doctor> Doctor { get; set; }

        public DbSet<AppointmentModel> Appointments { get; set; }
    }
}
