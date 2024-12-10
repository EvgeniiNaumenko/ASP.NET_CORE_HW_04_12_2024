using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1_HW_04_12_2024.ViewModels;

namespace WebApplication1_HW_04_12_2024.Services
{
    public class ProductService
    {
        private static readonly List<ProductViewModel> Products = new List<ProductViewModel>();

        public List<ProductViewModel> GetAllProducts()
        {
            return Products;
        }

        public ProductViewModel GetProductById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public ProductViewModel AddProduct(ProductViewModel product)
        {
            product.Id = Products.Any() ? Products.Max(p => p.Id) + 1 : 1;
            Products.Add(product);
            return product;
        }

        public ProductViewModel UpdateProduct(ProductViewModel updatedProduct)
        {
            var existingProduct = GetProductById(updatedProduct.Id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;

            return existingProduct;
        }

        public bool DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product == null)
            {
                return false;
            }

            Products.Remove(product);
            return true;
        }
    }
}
