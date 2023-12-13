using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CanteenArduinoProject.Data;

public partial class CanteenContext : DbContext
{
    public CanteenContext()
    {
    }

    public CanteenContext(DbContextOptions<CanteenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=canteen;Uid=root;Pwd=xampp@123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasMaxLength(80);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
