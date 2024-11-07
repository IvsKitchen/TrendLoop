﻿using System.ComponentModel.DataAnnotations;
using static TrendLoop.Common.EntityValidationConstants.Category;

namespace TrendLoop.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

        public virtual ICollection<CategoryAttributeType> AttributeTypes { get; set; } = new HashSet<CategoryAttributeType>();
    }
}