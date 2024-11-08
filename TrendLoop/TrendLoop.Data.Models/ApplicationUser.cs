﻿using Microsoft.AspNetCore.Identity;

namespace TrendLoop.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<Product> ProductsForSale { get; set; } = new HashSet<Product>();
        public virtual ICollection<Product> ProductsBought { get; set; } = new HashSet<Product>();

        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }
    }
}