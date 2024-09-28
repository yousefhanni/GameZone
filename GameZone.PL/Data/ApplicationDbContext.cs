using Microsoft.EntityFrameworkCore;

namespace GameZone.PL.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
        {
        }

    }
}
