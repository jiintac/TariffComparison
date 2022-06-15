using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using TariffComparison.DataLayer.Model;
using TariffComparison.DataLayer.Repositories;

namespace TariffComparison.LogicLayer.Service
{
    public class ProductService: IProductService
    {
        public readonly IProductRepository productRepository;
        public ProductService(IProductRepository product)
        {
            productRepository = product;
        }

        public async Task<Model.ResponseObject<IEnumerable<Model.Product>>> GetListConsumptionAsync(double consumption)
        {
            try
            {
                List<Product> products = productRepository.GetProducts();
                List<Model.Product> returnProducts = new List<Model.Product>();
                foreach (Product product in products)
                {
                    double annualCost = await CalculateConsumption(product.calculation, consumption);
                    returnProducts.Add(new Model.Product
                    {
                        TariffName = product.name,
                        AnnualCost = annualCost,
                        UnitCost = product.calculation.costUnit.ToString()
                    });
                }
                //IEnumerable - not to be modified
                return Model.ResponseObject<IEnumerable<Model.Product>>.Success(returnProducts.OrderBy(p=>p.AnnualCost));
            } catch (Exception ex)
            {
                return Model.ResponseObject<IEnumerable<Model.Product>>.Error(500, ex.Message.ToString());
            }
        }

        private async Task<double> CalculateConsumption(Calculation calculation, double consumption)
        {

            var result = await Task.Run(() =>
            {
                double baseCost = calculation.baseCostUnit == BaseCostUnit.Monthly ? calculation.baseCost * 12 : calculation.baseCost;
                AdditionalCost additionalCost = calculation.additionalCosts.FirstOrDefault(cost => cost.boundAdditionalCost <= consumption);
                double addCost = additionalCost != null ? additionalCost.additionalCost * (consumption - additionalCost.boundAdditionalCost) : 0;
                return baseCost + addCost;
            });


            return result;
        }
    }
}
