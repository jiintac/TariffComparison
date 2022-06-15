using System.Collections.Generic;
namespace TariffComparison.DataLayer.Model
{
    public class Product
    {
        public string name { get; set; }
        public Calculation calculation { get; set; }
        public Product()
        {
        }
    }
}
