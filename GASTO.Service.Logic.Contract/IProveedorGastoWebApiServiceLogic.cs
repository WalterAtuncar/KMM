using System.Threading.Tasks;

namespace GASTO.Service.Logic.Contract
{
    public interface IProveedorGastoWebApiServiceLogic
    {
        Task<OperationResult> GetOrdenServicio(string ordenServicio, string sociedad);
       
    }
}
