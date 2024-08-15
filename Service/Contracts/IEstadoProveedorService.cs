using Data;
using System.Collections.Generic;

namespace Service.Contracts
{
    public interface IEstadoProveedorService
    {
        List<EstadoServicio> GetList();
    }
}
