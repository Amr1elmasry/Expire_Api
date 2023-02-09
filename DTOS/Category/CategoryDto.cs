﻿using Expire_Api.DTOS.Market;
using Expire_Api.DTOS.Seller;
using Expire_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Expire_Api.DTOS.Category
{
    public class CategoryDto
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }
        public ICollection<Product> Products { get; set; }

        public ForeignMarketDto Market { get; set; }
        public int MarketId { get; set; }
        public ForeignSellerDto Seller { get; set; }
        public string SellerId { get; set; }
    }
}