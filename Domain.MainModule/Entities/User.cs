using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.MainModule.Entities
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DocumentNumber { get; set; }
        public string MobilePhone { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
