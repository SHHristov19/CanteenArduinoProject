using System;
using System.Collections.Generic;
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

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Usermenu> Usermenus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=canteen;Uid=root;Pwd=xampp@123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PRIMARY");

            entity.ToTable("menu");

            entity.Property(e => e.MenuId).HasMaxLength(20);
            entity.Property(e => e.Appetizer)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Day)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Dessert)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Main)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("reader");

            entity.Property(e => e.Time)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("timestamp");
            entity.Property(e => e.Uid).HasMaxLength(20);
        });

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

        modelBuilder.Entity<Usermenu>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("usermenu");

            entity.HasIndex(e => e.MenuId, "MenuId");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.Date)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date");
            entity.Property(e => e.MenuId).HasMaxLength(20);
            entity.Property(e => e.UserId).HasMaxLength(20);

            entity.HasOne(d => d.Menu).WithMany()
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("usermenu_ibfk_2");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("usermenu_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
