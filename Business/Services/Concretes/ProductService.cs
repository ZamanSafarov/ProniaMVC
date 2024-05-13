using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Data.RepositoryConcretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }
        public async Task AddProduct(Product product)
        {
            if (!_productRepository.GetAll().Any(x => x.Name == product.Name))
            {
                await _productRepository.AddAsync(product);
                await _productRepository.CommitAsync();
            }
            else
            {
                throw new DuplicateEntityException("Product Already Has");
            }
           

        }

        public void DeleteProduct(int id)
        {
            var existProduct = _productRepository.Get(x => x.Id == id);
            if (existProduct == null) throw new EntityNotFoundException("Product does not exsist");

            _productRepository.Delete(existProduct);
            existProduct.DeletedDate = DateTime.UtcNow.AddHours(4);
            _productRepository.Commit();

        }

        public List<Product> GetAllProducts(Func<Product, bool>? func = null)
        {
            var products =  _productRepository.GetAll(func);

            if (products is null) throw new EntityNotFoundException("Product does not exsist");

            return products;
           
        }

        public Product GetProduct(Func<Product, bool>? func = null)
        {
            var product = _productRepository.Get(func);

            if (product is null) throw new EntityNotFoundException("Product does not exsist");

            return product;
        }

        public void UpdateProduct(int id, Product newProduct)
        {
            Product oldProduct = _productRepository.Get(x => x.Id == id);
            if (oldProduct == null) throw new EntityNotFoundException("Product does not exsist");


            if (!_productRepository.GetAll().Any(x => x.Name == newProduct.Name && x.Id != id))
            {
                oldProduct.Name = newProduct.Name;
                oldProduct.Price = newProduct.Price;
                oldProduct.Description = newProduct.Description;
                oldProduct.ImageUrl = newProduct.ImageUrl;
                oldProduct.ImageFile = newProduct.ImageFile;
            }
            else
            {
                throw new DuplicateEntityException("Product Already Has");
            }


            _productRepository.Commit();
        }
    }
}
