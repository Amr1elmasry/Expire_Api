﻿using System.ComponentModel.DataAnnotations;

namespace Expire_Api.Models
{
    public class Market
    {
        public Market()
        {
            Products = new HashSet<Product>();
        }

        [Required]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        public ICollection<Product> Products { get; set; }
        public Seller Seller { get; set; }
        public string SellerId { get; set; }
    }
}
