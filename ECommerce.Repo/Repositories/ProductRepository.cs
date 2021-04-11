using ECommerce.DAL.Models;
using ECommerce.DAL.XMLManager;
using ECommerce.Repo.Interfaces;
using System;
using System.Collections.Generic;

namespace ECommerce.Repo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IXMLManager _xmlManager;

        public ProductRepository(IXMLManager xmlManager)
        {
            _xmlManager = xmlManager;
        }

        public IEnumerable<Product> LoadProducts()
        {
            var products = _xmlManager.Load<Products>("Products");
            return products.ProductList;
        }
    }
}
