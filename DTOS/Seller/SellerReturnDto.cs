﻿namespace Expire_Api.DTOS.Seller
{
    public class SellerReturnDto
    {
        public string? Massage { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public bool IsAuth { get; set; }
        public List<string>? Roles { get; set; }
    }
}
