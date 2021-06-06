using Microsoft.EntityFrameworkCore;
using PeopleManagementApp.Entities;

namespace CourseWork_1
{
    class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString);
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasKey("Employee_Id");
        }
    }
}
