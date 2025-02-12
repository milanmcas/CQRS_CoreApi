using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Models
{
    public class ProductModel
    {
        public List<productNew> Products { get; set; }
        public List<product_category> ProductCategory { get; set; }
    }
    public class product_category
    {
        public int cat_id { get; set; }
        public string cat_name { get; set; }
    }
    public class productNew
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public DateOnly date_added { get; set; }
        public int product_category_id { get; set; }
    }
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
    public class Product1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        //Optimistic Concurrency With Database-Generated Tokens
        [Timestamp]
        public byte[]? RowVersion { get; set; }//EF Core will treat it as a concurrency token
    }
    //Optimistic Concurrency With Application-Managed Tokens
    public class Product2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        [ConcurrencyCheck]
        public Guid Version { get; set; }
    }
    public class Product3
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        //[ConcurrencyCheck]
        public Guid Version { get; set; }
    }
    public class Product4
    {
        [Key]
        public int Id { get; set; }//primary key

        [Required, MaxLength(100)]
        public string Name { get; set; }//Non-nullable,Max length of 100

        [MinLength(50)]
        public string Name1 { get; set; } = null!;// Min length of 5
        public string Name2 { get; set; }

        [StringLength(100, MinimumLength = 5)]
        public string Name3 { get; set; } // Length between 5 and 100

        [Column("ProductName", TypeName = "nvarchar(150)")]
        public string Name4 { get; set; } // Custom column name and type
        public decimal Price { get; set; }

        //[ConcurrencyCheck]
        public Guid Version { get; set; }
    }
    [Table("ProductsTable", Schema = "Inventory")]
    public class Product5
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordNum { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        [Unicode(false)]
        [MaxLength(22)]
        public string Isbn { get; set; }
    }
    [PrimaryKey(nameof(State), nameof(LicensePlate))]//core 7.0
    [Index(nameof(Id), IsUnique = true)]
    internal class Car
    {
        
        public int Id { get; set; }
        public string State { get; set; }
        public string LicensePlate { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
    }
}
