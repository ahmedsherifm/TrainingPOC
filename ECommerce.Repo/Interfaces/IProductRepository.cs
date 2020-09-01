using ECommerce.DAL.Models;
using System.Collections.Generic;

namespace ECommerce.Repo.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> LoadProducts();
    }
}
