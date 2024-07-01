using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VuongLeTrung.Exercise02.DTOs
{
    public class ProductDTO
    {
        public Guid? Id { get; set; }
        [MaxLength(255)]
        public string? ProductName { get; set; }


        public decimal? SalePrice { get; set; }

        public decimal? BuyingPrice { get; set; }
        public int? Quantity { get; set; }
        public string? ShortDescription { get; set; }
        public string? ProductDescription { get; set; }
        public bool? Published { get; set; } = false;
        public string? Note { get; set; }

        public List<Guid> ProductCategoryIds { get; set; }
        public List<string> ProductCategoryNames { get; set; }
        public List<Guid> ProductTagIds { get; set; }
        public List<string> ProductTagsNames { get; set; }
    }
}