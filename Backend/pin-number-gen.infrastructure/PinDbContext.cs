using Microsoft.EntityFrameworkCore;

namespace pin_number_gen.infrastructure
{
    public class PinDbContext : DbContext
    {
        public PinDbContext(DbContextOptions<PinDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PinEntity> PinEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PinEntity>(
                    eb =>
                    {                                          
                        eb.ToTable("Pins");                        
                    });
            base.OnModelCreating(modelBuilder);
        }
    }
}