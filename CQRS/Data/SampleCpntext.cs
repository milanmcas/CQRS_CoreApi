using CQRS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CQRS.Data
{
    public class SampleCpntext : DbContext
    {
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Student1> Student { get; set; }
        public DbSet<City1> City1 { get; set; }
        public DbSet<productNew> productNews { get; set; }
        public DbSet<CityInformation> CityInformation { get; set; }
        public DbSet<product_category> product_Categories { get; set; }
        public DbSet<ProductModel> productModels { get; set; }
        // public SampleCpntext(DbContextOptions<PlayerDbContext> options)
        //: base(options)
        // {
        // }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<productNew>()
                .HasKey(x => x.product_id);
            modelBuilder.Entity<product_category>()
                .HasKey(x=>x.cat_id);
            modelBuilder.Entity<ProductModel>()
                .HasNoKey();

            modelBuilder.Entity<Contact>()
           .Property<DateTime>("LastUpdated").HasDefaultValueSql("GETDATE()");//shadow property
            modelBuilder.Entity<Contact>().Property(e => e.Email).HasDefaultValue("milan@gmail.com");

            modelBuilder.Entity<City1>()
                    .HasOne(e => e.CityInformation)
                    .WithOne(e => e.City)
                    .HasForeignKey<CityInformation>(e => e.CityId)
                    .IsRequired();

            modelBuilder.Entity<City>()
                    .HasOne(e => e.Country)
                    .WithMany(e => e.City)
                    .HasForeignKey(e => e.FKCountry)
                    .IsRequired();

            modelBuilder.Entity<TeacherStudent>()
                .HasKey(t => new { t.StudentId, t.TeacherId }); // composite primary key
            modelBuilder.Entity<Teacher>().HasKey(x=>x.Id);
            modelBuilder.Entity<Student1>().HasKey(x => x.Id);
            modelBuilder.Entity<Student1>()
                        .HasMany(e => e.Teacher)
                        .WithMany(e => e.Student)
                        .UsingEntity<TeacherStudent>(x =>
                        {
                            x.HasOne<Teacher>().WithMany().HasForeignKey(x => x.TeacherId);
                            x.HasOne<Student1>().WithMany().HasForeignKey(y => y.StudentId);
                        });
            //modelBuilder.Entity<Student1>()
            //            .HasMany(e => e.Teacher)
            //            .WithMany(e => e.Student)
            //            .UsingEntity<TeacherStudent>();
            //.ValueGeneratedOnAddOrUpdate();//.Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save); 
            //base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //https://www.csharptutorial.net/entity-framework-core-tutorial/ef-core-log-sql-query/#:~:text=To%20log%20information%20generated%20by%20EF%20Core%20to,class%20like%20this%3A%20DbContextOptionsBuilder%20optionsBuilder%20optionsBuilder%20.UseSqlServer%28connectionString%29%20.LogTo%28target%29
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=DRPRIYATMAA;Initial Catalog=RND1;User ID=milan;Password=milan;Trust Server Certificate=True;");
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
