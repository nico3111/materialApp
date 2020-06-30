using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialData.models
{
    public class MaterialEntities : DbContext
    {
        public DbSet<notebook> notebook { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=material;username=root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<notebook>(entity =>
            {
                entity.HasKey(x => x.id);
                entity.Property(x => x.serial_number).IsRequired();
                entity.Property(x => x.make).IsRequired();
                entity.Property(x => x.model).IsRequired();
            });
        }
    }
}
