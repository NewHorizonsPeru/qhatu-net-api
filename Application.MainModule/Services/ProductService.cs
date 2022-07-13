using System.Collections.Generic;
using Application.MainModule.DTO;
using Application.MainModule.IServices;

namespace Application.MainModule.Services
{
    public class ProductService : IProductService
    {
        public IEnumerable<ProductDto> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public ProductDto GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(ProductDto productDto)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, ProductDto productDto)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}