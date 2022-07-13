using System.Collections.Generic;
using Application.MainModule.DTO;

namespace Application.MainModule.IServices
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetAll();
        OrderDto GetById(int id);
        void Add(OrderDto orderDto);
        void Update(int id, OrderDto orderDto);
        void Remove(int id);
    }
}