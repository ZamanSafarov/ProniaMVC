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

        public async Task AddCategory(Category Category)
        {
            if (!_categoryRepository.GetAll().Any(x => x.Name == Category.Name))
            {
                await _categoryRepository.AddAsync(Category);
                await _categoryRepository.CommitAsync();
            }
        }

        public void DeleteCategory(int id)
        {
            var existCategory = _categoryRepository.Get(x => x.Id == id);
            if (existCategory == null) throw new NullReferenceException();

            _categoryRepository.Delete(existCategory);
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
            if (oldCategory == null) throw new NullReferenceException();
            if (!_categoryRepository.GetAll().Any(x => x.Name == newCategory.Name))
            {
                oldCategory.Name = newCategory.Name;

            }
            _categoryRepository.Commit();
        }
    }
}
