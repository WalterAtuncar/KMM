using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Service.Services
{
    public class SolicitudProveedorTaxiService : ISolicitudProveedorTaxiService
    {
        public List<SolicitudProveedorTaxi> GetListBySolicitudID(int solicitudID)
        {
            using (var context = new Entities())
            {
                return context.SolicitudProveedorTaxi.Include("Solicitud").Include("ProveedorTaxi").Where(x => x.SolicitudID == solicitudID).ToList();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
