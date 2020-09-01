using ECommerce.DAL.Models;
using ECommerce.DAL.XMLManager;
using ECommerce.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Repo.Interfaces;

namespace ECommerce.Business.Services
{
    public class ProductService : IProductService
    {
        private List<Product> _products = new List<Product>();

        public ProductService(IProductRepository productRepository)
        {
            _products = productRepository.LoadProducts().ToList();
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public IList<Product> GetProductsOrderedByPrice()
        {
            return _products.OrderBy(p => p.Price).ToList();
        }

        public IList<Product> GetProductsOrderedByPriceAndFiltered(int minPrice, int maxPrice)
        {
            return GetProductsOrderedByPrice()
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToList();
        }
    }
}
