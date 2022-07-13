using System.Collections;
using System.Collections.Generic;
using Application.MainModule.DTO;

namespace Application.MainModule.IServices
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAll();
        ProductDto GetById(int id);
        void Add(ProductDto productDto);
        void Update(int id, ProductDto productDto);
        void Remove(int id);
    }
}