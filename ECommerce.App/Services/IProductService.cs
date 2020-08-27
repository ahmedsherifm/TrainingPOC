using ECommerce.Main.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Main.Services
{
    public interface IProductService
    {
        IList<Product> GetProductsOrderedByPrice();

        IList<Product> GetProductsOrderedByPriceAndFiltered(int minPrice, int maxPrice);

        Product GetProductById(int id);
    }
}
