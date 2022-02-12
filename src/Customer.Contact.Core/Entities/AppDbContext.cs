using Microsoft.EntityFrameworkCore;

namespace Customer.Contact.Core.Entities
{
    public partial class AppDbContext : DbContext
    {       

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contact> Contact { get; set; }
        
    }
}
