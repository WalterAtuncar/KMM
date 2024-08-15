using Service.Contracts.Proveedor;
using Service.Logic.Contract;
using System.Threading.Tasks;

namespace Service.Logic
{
    public class ProveedorServiceLogic : IProveedorServiceLogic
    {
        private readonly IProveedorService _IProveedorService;
        public ProveedorServiceLogic(IProveedorService _IProveedorService)
        {
            this._IProveedorService = _IProveedorService;
        }

        //public async Task<OperationResult> Add(Data.Common.ProveedorTaxi proveedor)
        //{
        //    var result = new OperationResult();
        //    using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //    {
        //        try
        //        {
        //            int rowAffected = await _IProveedorService.Add(proveedor);
        //            ts.Complete();
        //            result.Success = true;
        //        }
        //        catch (Exception es)
        //        {
        //            ts.Dispose();
        //            result.Success = false;
        //            result.Errors = new List<string>() { es.Message };
        //        }
        //    }

        //    return result;
        //}

        //public async Task<OperationResult> AddCredencial(Data.CredencialesProveedorTaxi credencial)
        //{
        //    return await _IProveedorService.AddCredencial(credencial);
        //}

        //public OperationResult AddMethodService(List<usp_ServicioProveedorTaxi_List_Result> listaMethod, int IdProveedor)
        //{
        //    return _IProveedorService.AddMethodService(listaMethod, IdProveedor);
        //}

        //public async Task<Data.CredencialesProveedorTaxi> GetCredencialesById(int id)
        //{
        //    return await _IProveedorService.GetCredencialesById(id);
        //}

        public async Task<Data.Common.ProveedorTaxi> GetProveedorById(int id)
        {
            return await _IProveedorService.GetProveedorById(id);
        }

        //public IEnumerable<Data.ProveedorTaxi> Listar()
        //{
        //    return  _IProveedorService.Listar();
        //}

        //public List<Data.Common.ProveedorTaxi> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros)
        //{
        //    return _IProveedorService.ListarPaginado(parametros, out totalRows, out cantidadRegistros);
        //}

        //public async Task<OperationResult> Modificar(Data.Common.ProveedorTaxi proveedor)
        //{
        //    var result = new OperationResult();
        //    using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //    {
        //        try
        //        {
        //            int rowAffected = await _IProveedorService.Modificar(proveedor);
        //            ts.Complete();
        //            result.Success = true;
        //            result.RowAffeted = rowAffected;
        //        }
        //        catch (Exception es)
        //        {
        //            ts.Dispose();
        //            result.Success = false;
        //            result.Errors = new List<string>() { es.Message };
        //        }
        //    }

        //    return result;
        //}

        //public async Task<OperationResult> ModificarCredencial(Data.CredencialesProveedorTaxi credencial)
        //{
        //    return await _IProveedorService.ModificarCredencial(credencial);
        //}

        //public async Task<string> Key()
        //{
        //    return await _IProveedorService.Key();
        //}

        //public async Task<bool> ValidarProveedor(string ruc, int id)
        //{
        //    return await _IProveedorService.ValidarProveedor(ruc, id);
        //}
    }
}
