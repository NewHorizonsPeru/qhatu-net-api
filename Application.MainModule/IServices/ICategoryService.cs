using System.Collections.Generic;
using Application.MainModule.DTO;

namespace Application.MainModule.IServices
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAll();
        CategoryDto GetById(int id);
        void Add(CategoryDto categoryDto);
        void Update(int id, CategoryDto categoryDto);
        void Remove(int id);
    }
}