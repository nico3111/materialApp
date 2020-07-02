using Microsoft.EntityFrameworkCore;

namespace MaterialData.models
{
    public class DcvEntities : DbContext
    {
        public DbSet<notebook> notebook { get; set; }
        public DbSet<person> person { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL("server=localhost;database=dcv;username=root");
          optionsBuilder.UseMySQL("server=192.168.0.94;database=dcv;user=root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<notebook>(entity =>
            {
                entity.HasKey(x => x.id);
                /*entity.Property(x => x.serial_number).IsRequired();
                entity.Property(x => x.make).IsRequired();
                entity.Property(x => x.model).IsRequired();*/
            });
            modelBuilder.Entity<person>().HasKey(x => x.id);

            modelBuilder.Entity<notebook>()
                .HasOne(x => x.people)
                .WithMany()
                .HasForeignKey(x => x.person_id);
            //x => x.notebook
        }
    }
}
