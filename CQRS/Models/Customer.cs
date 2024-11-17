﻿using System.ComponentModel.DataAnnotations;

namespace CQRS.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public ICollection<Order> Orders { get; set; }
        //Add-Migration AddEmailInCustomers -Context OnlineShopDbContext
    }
}
