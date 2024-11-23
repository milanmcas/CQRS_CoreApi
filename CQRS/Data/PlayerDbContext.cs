using System;
using System.Collections.Generic;
using CQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Data;

public partial class PlayerDbContext : DbContext
{
    public PlayerDbContext()
    {
    }

    public PlayerDbContext(DbContextOptions<PlayerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Player> Players { get; set; }
    public virtual DbSet<Player> PostPlayers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Data Source=DRPRIYATMAA;Initial Catalog=CQRS;User ID=milan;Password=milan;Trust Server Certificate=True;");
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
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
