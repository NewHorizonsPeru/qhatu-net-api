using System.Collections.Generic;
using System.Linq;
using Application.MainModule.DTO;
using Application.MainModule.IServices;
using AutoMapper;
using Domain.MainModule.Entities;
using Domain.MainModule.IRepositories;

namespace Application.MainModule.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public IEnumerable<ProductDto> GetAll()
        {
            var products = _productRepository.List();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public ProductDto GetById(int id)
        {
            var product = _productRepository.GetById(id);
            return _mapper.Map<ProductDto>(product);
        }

        public void Add(ProductDto productDto)
        {
            var existsProducts = _productRepository.List(w => w.Description.Contains(productDto.Description));
            if (!existsProducts.Any())
            {
                var product = _mapper.Map<Product>(productDto);
                _productRepository.Add(product);
                _productRepository.Save();
            }
        }

        public void Update(int id, ProductDto productDto)
        {
            var product = _productRepository.GetById(id);
            _mapper.Map(productDto, product);
            _productRepository.Save();
        }

        public void Remove(int id)
        {
            var product = _productRepository.GetById(id);
            _productRepository.Remove(product);
            _productRepository.Save();
        }
    }
}