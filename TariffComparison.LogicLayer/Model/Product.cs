using System;
namespace TariffComparison.LogicLayer.Model
{
    public class Product
    {
        public string TariffName { get; set; }
        public double AnnualCost { get; set; }
        public string UnitCost { get; set; }
        public Product()
        {
        }
    }
}
