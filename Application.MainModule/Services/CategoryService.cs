using System.Collections.Generic;
using System.Linq;
using Application.MainModule.DTO;
using Application.MainModule.IServices;
using AutoMapper;
using Domain.MainModule.Entities;
using Domain.MainModule.IRepositories;

namespace Application.MainModule.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            var categories = _categoryRepository.List();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public CategoryDto GetById(int id)
        {
            var category = _categoryRepository.GetById(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public void Add(CategoryDto categoryDto)
        {
            var existsCategories = _categoryRepository.List(w => w.Description.Contains(categoryDto.Description));
            if (!existsCategories.Any())
            {
                var category = _mapper.Map<Category>(categoryDto);
                _categoryRepository.Add(category);
                _categoryRepository.Save();
            }
        }

        public void Update(int id, CategoryDto categoryDto)
        {
            var category = _categoryRepository.GetById(id);
            _mapper.Map(categoryDto, category);
            _categoryRepository.Save();
        }

        public void Remove(int id)
        {
            var category = _categoryRepository.GetById(id);
            _categoryRepository.Remove(category);
            _categoryRepository.Save();
        }
    }
}