using System;

#nullable disable

namespace Domain.MainModule.Entities
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Sku { get; set; }
        public string ImageUrl { get; set; }
        public decimal SalePrice { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Category Category { get; set; }
    }
}
