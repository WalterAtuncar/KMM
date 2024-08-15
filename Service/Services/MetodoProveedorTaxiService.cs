using Data;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class MetodoProveedorTaxiService
    {
        public List<MetodoProveedorTaxi> GetList()
        {
            using (var context = new Entities())
            {
                return context.Set<MetodoProveedorTaxi>().ToList();
            }
        }
    }
}
