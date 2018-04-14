using Microsoft.EntityFrameworkCore;
using YAPMT.Framework.Extensions;

namespace YAPMT.Framework.Repositories
{
    public class PrincipalDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public PrincipalDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration();

            base.OnModelCreating(modelBuilder);
        }
    }
}
