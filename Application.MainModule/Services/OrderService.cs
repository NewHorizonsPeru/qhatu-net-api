using System.Collections.Generic;
using Application.MainModule.DTO;
using Application.MainModule.IServices;

namespace Application.MainModule.Services
{
    public class OrderService : IOrderService
    {
        public IEnumerable<OrderDto> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public OrderDto GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(OrderDto orderDto)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, OrderDto orderDto)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}