namespace Application.MainModule.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string PaymentMethod { get; set; }
        public string Comment { get; set; }
    }
}