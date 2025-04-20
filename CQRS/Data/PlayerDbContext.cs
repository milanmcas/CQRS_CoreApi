using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Data;

public partial class PlayerDbContext : DbContext
{
    public PlayerDbContext()
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public PlayerDbContext(DbContextOptions<PlayerDbContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public virtual DbSet<Player> Players { get; set; }
    public virtual DbSet<Player> PostPlayers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Student> Student { get; set; }
    public DbSet<Book> Book { get; set; }
    public DbSet<StudentCourses> StudentCourses { get; set; }
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //https://www.csharptutorial.net/entity-framework-core-tutorial/ef-core-log-sql-query/#:~:text=To%20log%20information%20generated%20by%20EF%20Core%20to,class%20like%20this%3A%20DbContextOptionsBuilder%20optionsBuilder%20optionsBuilder%20.UseSqlServer%28connectionString%29%20.LogTo%28target%29
        //base.OnConfiguring(optionsBuilder);
        //optionsBuilder.UseSqlServer("Data Source=DRPRIYATMAA;Initial Catalog=CQRS;User ID=milan;Password=priya;Encrypt=True;Trust Server Certificate=True;")
        //    .LogTo(Console.WriteLine)
        //    .EnableSensitiveDataLogging();
        optionsBuilder.UseSqlServer("Data Source=DRPRIYATMAA;Initial Catalog=CQRS;User ID=milan;Password=priya;Encrypt=Yes;TrustServerCertificate=Yes;")
            .LogTo(Console.WriteLine)
            .EnableSensitiveDataLogging();
    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Data Source=DRPRIYATMAA;Initial Catalog=CQRS;User ID=milan;Password=milan;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Player__3214EC0706633CC8");

            entity.ToTable("Player");

            //entity.Property(e => e.Id).ValueGeneratedNever();//UseIdentityColumn<int>(1,1)
            entity.Property(e => e.Name).HasMaxLength(100).HasDefaultValue("hnhh");
            //entity.Property(e => e.Name).HasDefaultValue("hnhh");
            //entity.Property<DateTime>("CreatedAt").HasDefaultValueSql("GETDATE()");
        });
        //modelBuilder.Entity<Player>().Property<DateTime>("CreatedAt").HasDefaultValueSql("GETDATE()")
            //.ValueGeneratedOnAddOrUpdate();
        //modelBuilder.Entity<Player>().Property(e => e.Name).HasDefaultValue("hnhh");
        modelBuilder.HasDbFunction(typeof(PlayerDbContext)
            .GetMethod(nameof(GetProductDetail))
            ).HasName("GetProducts");


        modelBuilder.Entity<StudentCourses>()
                .HasKey(sc => new { sc.StudentId, sc.BookId }); // Composite Key for Join Table
        modelBuilder.Entity<Book>().HasKey(x => x.Id);
        modelBuilder.Entity<Student>().HasKey(x => x.Id);
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Books)
            .WithMany(c => c.Students)
            .UsingEntity<StudentCourses>(
            l => l.HasOne<Book>(e => e.Book).WithMany().HasForeignKey(x => x.BookId),
            r => r.HasOne<Student>(e => e.Student).WithMany().HasForeignKey(x => x.StudentId));

        //.UsingEntity(j => j.ToTable("StudentCourses"));

        OnModelCreatingPartial(modelBuilder);
    }
    //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    //{
    //    ChangeTracker.DetectChanges();

    //    foreach (var entry in ChangeTracker.Entries().Where(entity => entity.State == EntityState.Modified))
    //    {
    //        entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
    //    }
    //    foreach (var entry in ChangeTracker.Entries().Where(entity => entity.State ==  EntityState.Added))
    //    {
    //        entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
    //    }

    //    return base.SaveChangesAsync();
    //}
    public IQueryable<Product> GetProductDetail()
    => FromExpression(() => GetProductDetail());


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
