using System.Collections.Generic;
namespace TariffComparison.DataLayer.Model
{
    public class Calculation
    {
        public double baseCost { get; set; }
        public BaseCostUnit baseCostUnit { get; set; }
        public List<AdditionalCost> additionalCosts { get; set; }
        public CostUnit costUnit { get; set; }
        public Calculation()
        {
            additionalCosts = new List<AdditionalCost>();
        }
    }

    public enum BaseCostUnit
    {
        Monthly,
        Yearly
    }

    public enum CostUnit
    {
        kWh
    }
}
