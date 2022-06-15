using System.Collections.Generic;
using TariffComparison.DataLayer.Model;
namespace TariffComparison.DataLayer.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
    }
}
