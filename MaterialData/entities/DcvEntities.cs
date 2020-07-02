using Microsoft.EntityFrameworkCore;

namespace MaterialData.models
{
    public class DcvEntities : DbContext
    {
        public DbSet<notebook> notebook { get; set; }
        public DbSet<person> person { get; set; }
        public DbSet<classroom> classroom { get; set; }

        public DbSet<addressLocation> addressLocation { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=192.168.0.94;database=dcv;user=root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<notebook>(entity =>
            {
                entity.HasKey(x => x.id);
            });

            modelBuilder.Entity<person>().HasKey(x => x.id);

            modelBuilder.Entity<classroom>().HasKey(x => x.id);

            modelBuilder.Entity<addressLocation>().HasKey(x => x.id);

            modelBuilder.Entity<address>().HasKey(x => x.id);

            modelBuilder.Entity<notebook>()
                .HasOne(x => x.person)
                .WithMany()
                .HasForeignKey(x => x.person_id);

            modelBuilder.Entity<notebook>()
                .HasOne(x => x.classroom)
                .WithMany()
                .HasForeignKey(x => x.location_id);

            modelBuilder.Entity<addressLocation>()
                .HasOne(x => x.address)
                .WithOne()
                .HasForeignKey<addressLocation>(x => x.adressId);

            modelBuilder.Entity<addressLocation>()
                .HasOne(x => x.classroom)
                .WithOne(x => x.addressloc)
                .HasForeignKey<addressLocation>(x => x.locationId);

        }
    }
}
