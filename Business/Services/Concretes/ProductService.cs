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
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }
        public async Task AddProduct(Product Product)
        {
            await _productRepository.AddAsync(Product);
            await _productRepository.CommitAsync();

        }

        public void DeleteProduct(int id)
        {
            var existProduct = _productRepository.Get(x => x.Id == id);
            if (existProduct == null) throw new NullReferenceException();

            _productRepository.Delete(existProduct);
            _productRepository.Commit();

        }

        public List<Product> GetAllProducts(Func<Product, bool>? func = null)
        {
            return _productRepository.GetAll(func);
        }

        public Product GetProduct(Func<Product, bool>? func = null)
        {
            return _productRepository.Get(func);
        }

        public void UpdateProduct(int id, Product newProduct)
        {
            Product oldProduct = _productRepository.Get(x => x.Id == id);
            if (oldProduct == null) throw new NullReferenceException();


            oldProduct.Name = newProduct.Name;
            oldProduct.Price = newProduct.Price;
            oldProduct.Description = newProduct.Description;
            oldProduct.CategoryId = newProduct.CategoryId;
            oldProduct.ImageUrl = newProduct.ImageUrl;
            oldProduct.ImageFile = newProduct.ImageFile;


            _productRepository.Commit();
        }
    }
}
