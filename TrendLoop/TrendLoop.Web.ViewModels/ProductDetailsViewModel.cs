using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrendLoop.Web.ViewModels
{
    public class ProductDetailsViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Price { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string AddedOn { get; set; } = null!;

        public string BrandName { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string SubcategoryName { get; set; } = null!;

        public string SellerAvatarUrl { get; set; } = null!;

        public string SellerName { get; set; } = null!;

        public double SellerRating { get; set; }

        public IEnumerable<AttributeTypeValueInfoViewModel> AttributeTypesWithValues { get; set; } = new HashSet<AttributeTypeValueInfoViewModel>();
    }
}
