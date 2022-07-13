using System.Collections.Generic;
using Application.MainModule.DTO;

namespace Application.MainModule.IServices
{
    public interface IOrderDetailService
    {
        IEnumerable<OrderDetailDto> GetAll();
        OrderDetailDto GetById(int id);
        void Add(OrderDetailDto orderDetailDto);
        void Update(int id, OrderDetailDto orderDetailDto);
        void Remove(int id);
    }
}