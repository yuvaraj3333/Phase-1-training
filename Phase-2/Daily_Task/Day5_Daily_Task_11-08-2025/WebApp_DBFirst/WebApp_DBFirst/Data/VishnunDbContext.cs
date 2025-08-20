using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApp_DBFirst.Models;

namespace WebApp_DBFirst.Data;

public partial class VishnunDbContext : DbContext
{
    public VishnunDbContext()
    {
    }

    public VishnunDbContext(DbContextOptions<VishnunDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Emp> Emps { get; set; }

    public virtual DbSet<MyTree> MyTrees { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=PTPLL444\\SQLDEV; Initial Catalog=vishnunDB; Integrated Security=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyTree>(entity =>
        {
            entity.ToTable("myTrees");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("student");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("email");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
