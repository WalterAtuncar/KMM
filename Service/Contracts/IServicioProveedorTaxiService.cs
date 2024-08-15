using Data;
using System.Collections.Generic;

namespace Service.Contracts
{
    public interface IServicioProveedorTaxiService
    {
        List<usp_ServicioProveedorTaxi_List_Result> GetListMethod(int idProveedor);
    }
}
