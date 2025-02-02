using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
}
