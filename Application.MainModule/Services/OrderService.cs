using System.Collections.Generic;
using Application.MainModule.DTO;
using Application.MainModule.IServices;
using AutoMapper;
using Domain.MainModule.Entities;
using Domain.MainModule.IRepositories;

namespace Application.MainModule.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public IEnumerable<OrderDto> GetAll()
        {
            var orders = _orderRepository.List();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public OrderDto GetById(int id)
        {
            var order = _orderRepository.GetById(id);
            return _mapper.Map<OrderDto>(order);
        }

        public void Add(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            _orderRepository.Add(order);
            _orderRepository.Save();
        }

        public void Update(int id, OrderDto orderDto)
        {
            var order = _orderRepository.GetById(id);
            _mapper.Map(orderDto, order);
            _orderRepository.Save();
        }

        public void Remove(int id)
        {
            var order = _orderRepository.GetById(id);
            _orderRepository.Remove(order);
            _orderRepository.Save();
        }
    }
}