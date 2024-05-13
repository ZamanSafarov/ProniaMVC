using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddCategory(Category category)
        {
            if (!_categoryRepository.GetAll().Any(x => x.Name == category.Name))
            {
                await _categoryRepository.AddAsync(category);
                await _categoryRepository.CommitAsync();
            }
            else
            {
                throw new DuplicateEntityException("Category Already Has");
            }
        }

        public void DeleteCategory(int id)
        {
            var existCategory = _categoryRepository.Get(x => x.Id == id);
            if (existCategory == null) throw new EntityNotFoundException("Category does not exsist");

            _categoryRepository.Delete(existCategory);
            existCategory.DeletedDate = DateTime.UtcNow.AddHours(4);
            _categoryRepository.Commit();

        }

        public List<Category> GetAllCategorys(Func<Category, bool>? func = null)
        {
            return _categoryRepository.GetAll(func);
        }

        public Category GetCategory(Func<Category, bool>? func = null)
        {
            return _categoryRepository.Get(func);
        }

        public void UpdateCategory(int id, Category newCategory)
        {
            Category oldCategory = _categoryRepository.Get(x => x.Id == id);
            if (oldCategory == null) throw new EntityNotFoundException("Category does not exsist");
            if (!_categoryRepository.GetAll().Any(x => x.Name == newCategory.Name && x.Id !=id))
            {
                oldCategory.Name = newCategory.Name;

            }
            else
            {
                throw new DuplicateEntityException("Category Already Has");
            }
            _categoryRepository.Commit();
        }
    }
}
