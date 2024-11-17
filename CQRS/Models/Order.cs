using System.ComponentModel.DataAnnotations;

namespace CQRS.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime? OrderDate { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
