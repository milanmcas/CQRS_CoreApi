using CQRS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CQRS.Data
{
    public class SampleCpntext : DbContext
    {
       // public SampleCpntext(DbContextOptions<PlayerDbContext> options)
       //: base(options)
       // {
       // }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
           .Property<DateTime>("LastUpdated").HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Contact>().Property(e => e.Email).HasDefaultValue("milan@gmail.com");
                //.ValueGeneratedOnAddOrUpdate();//.Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save); 
            //base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //https://www.csharptutorial.net/entity-framework-core-tutorial/ef-core-log-sql-query/#:~:text=To%20log%20information%20generated%20by%20EF%20Core%20to,class%20like%20this%3A%20DbContextOptionsBuilder%20optionsBuilder%20optionsBuilder%20.UseSqlServer%28connectionString%29%20.LogTo%28target%29
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=DRPRIYATMAA;Initial Catalog=CQRS;User ID=milan;Password=milan;Trust Server Certificate=True;");
                //.LogTo(Console.WriteLine)
                //.EnableSensitiveDataLogging();
        }
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Property("LastUpdated").CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }
        public DbSet<Contact> Contact { get; set; }
    }
}
