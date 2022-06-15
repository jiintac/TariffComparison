using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace TariffComparison.LogicLayer.Service
{
    public interface IProductService
    {
        Task<Model.ResponseObject<IEnumerable<Model.Product>>> GetListConsumptionAsync(double consumption);
    }
}
