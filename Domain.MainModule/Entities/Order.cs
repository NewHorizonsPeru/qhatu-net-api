using System;

#nullable disable

namespace Domain.MainModule.Entities
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string PaymentMethod { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; }
    }
}
