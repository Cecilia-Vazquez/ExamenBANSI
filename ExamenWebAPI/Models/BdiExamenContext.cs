using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExamenWebAPI.Models
{
    public partial class BdiExamenContext : DbContext
    {
        public BdiExamenContext()
        {
        }

        public BdiExamenContext(DbContextOptions<BdiExamenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbIexaman> TbIexamen { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbIexaman>(entity =>
            {
                entity.HasKey(e => e.IdExamen);

                entity.ToTable("tbIExamen");

                entity.Property(e => e.IdExamen).HasColumnName("idExamen");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
