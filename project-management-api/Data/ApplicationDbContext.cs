using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project_management_api.Model;

namespace project_management_api.Data
{
    
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<ProjectInformation> ProjectInformation { get; set; }
        public DbSet<TaskInformation> TaskInformations { get; set; }
    }
}
