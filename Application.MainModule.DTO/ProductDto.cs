namespace Application.MainModule.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Sku { get; set; }
        public string ImageUrl { get; set; }
        public decimal SalePrice { get; set; }
    }
}