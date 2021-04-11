using ECommerce.DAL.Models;
using System.Collections.Generic;

namespace ECommerce.Business.Interfaces
{
    public interface IProductService
    {
        IList<Product> GetProductsOrderedByPrice();

        IList<Product> GetProductsOrderedByPriceAndFiltered(int minPrice, int maxPrice);

        Product GetProductById(int id);
    }
}
