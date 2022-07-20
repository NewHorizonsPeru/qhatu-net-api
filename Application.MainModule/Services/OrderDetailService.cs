using System.Collections.Generic;
using Application.MainModule.DTO;
using Application.MainModule.IServices;
using AutoMapper;
using Domain.MainModule.Entities;
using Domain.MainModule.IRepositories;

namespace Application.MainModule.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        public IEnumerable<OrderDetailDto> GetAll()
        {
            var orders = _orderDetailRepository.List();
            return _mapper.Map<IEnumerable<OrderDetailDto>>(orders);
        }

        public OrderDetailDto GetById(int id)
        {
            var order = _orderDetailRepository.GetById(id);
            return _mapper.Map<OrderDetailDto>(order);
        }

        public void Add(OrderDetailDto orderDetailDto)
        {
            var order = _mapper.Map<OrderDetail>(orderDetailDto);
            _orderDetailRepository.Add(order);
            _orderDetailRepository.Save();
        }

        public void Update(int id, OrderDetailDto orderDetailDto)
        {
            var order = _orderDetailRepository.GetById(id);
            _mapper.Map(orderDetailDto, order);
            _orderDetailRepository.Save();
        }

        public void Remove(int id)
        {
            var order = _orderDetailRepository.GetById(id);
            _orderDetailRepository.Remove(order);
            _orderDetailRepository.Save();
        }
    }
}