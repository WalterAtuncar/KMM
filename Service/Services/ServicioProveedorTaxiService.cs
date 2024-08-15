using System.Collections.Generic;
using System.Linq;
using Data;
using Service.Contracts;

namespace Service.Services
{
    public class ServicioProveedorTaxiService : IServicioProveedorTaxiService
    {
        public List<usp_ServicioProveedorTaxi_List_Result> GetListMethod(int idProveedor)
        {
            using (var context = new Entities())
            {
                return context.usp_ServicioProveedorTaxi_List(idProveedor).ToList();
            }
        }
    }
}
