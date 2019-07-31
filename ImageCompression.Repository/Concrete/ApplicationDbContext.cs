using Microsoft.EntityFrameworkCore;
using ImageCompression.Model.Entities;

namespace ImageCompression.Repository.Concrete
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }
    }


}