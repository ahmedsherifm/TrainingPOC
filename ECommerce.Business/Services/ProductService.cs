﻿using ECommerce.DAL.Models;
using ECommerce.DAL.XMLManager;
using ECommerce.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IXMLManager _xmlManager;
        private IList<Product> _products = new List<Product>();

        public ProductService(IXMLManager xmlManager)
        {
            _xmlManager = xmlManager;
            LoadProductsFromXML();
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

        private void LoadProductsFromXML()
        {
            var products = _xmlManager.Load<Products>("Products");
            _products = products.ProductList;
        }
    }
}
