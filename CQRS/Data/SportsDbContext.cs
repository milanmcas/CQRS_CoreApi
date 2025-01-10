using System;
using System.Collections.Generic;
using Alachisoft.NCache.EntityFrameworkCore;
using CQRS.FootballModels;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Data;

public partial class SportsDbContext : DbContext
{
    public SportsDbContext(DbContextOptions<SportsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string cacheId = "myClusteredCache";
        NCacheConfiguration.Configure(cacheId, DependencyType.SqlServer);
        NCacheConfiguration.ConfigureLogger();
        optionsBuilder.UseSqlServer("Data Source=DRPRIYATMAA;Initial Catalog=FootballDb;User ID=milan;Password=milan;Trust Server Certificate=True;")
            .UseLazyLoadingProxies();
        //https://www.alachisoft.com/ncache/ef-core-cache.html#:~:text=The%20most%20common%20data%20to,application%20reads%20it%20multiple%20times.
        //https://www.c-sharpcorner.com/article/caching-in-entity-framework-ef-core-using-ncache/

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Players__3214EC0705CFE32D");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Position).WithMany(p => p.Players)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK__Players__Positio__3C69FB99");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Position__3214EC07B43B586E");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
