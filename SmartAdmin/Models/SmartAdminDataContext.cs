using Microsoft.EntityFrameworkCore;

namespace SmartAdmin.Models
{
    public class SmartAdminDataContext : DbContext
    {
        public SmartAdminDataContext(DbContextOptions<SmartAdminDataContext> options) 
            : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
    }
}