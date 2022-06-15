using System.Collections.Generic;
using TariffComparison.DataLayer.Model;
namespace TariffComparison.DataLayer.Repositories
{
    public class ProductRepository: IProductRepository
    {
        public ProductRepository()
        {
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            Calculation calculation = new Calculation();

            calculation.additionalCosts.Add(new AdditionalCost
            {
                boundAdditionalCost = 0,
                additionalCost = 0.22
            });
            calculation.baseCost = 5;
            calculation.baseCostUnit = BaseCostUnit.Monthly;
            calculation.costUnit = CostUnit.kWh;

            products.Add(new Product
            {
                name = "basic electricity tariff",
                calculation = calculation
            });


            calculation = new Calculation();
            calculation.additionalCosts.Add(new AdditionalCost
            {
                boundAdditionalCost = 4000,
                additionalCost = 0.30
            });
            calculation.baseCost = 800;
            calculation.baseCostUnit = BaseCostUnit.Yearly;
            calculation.costUnit = CostUnit.kWh;

            products.Add(new Product
            {
                name = "Packaged tariff",
                calculation = calculation
            });

            return products;

        }
    }
}
