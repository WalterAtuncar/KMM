using GASTO.Domain;
using System.Collections.Generic;

namespace GASTO.Service.Contracts
{
    public interface IUsuarioCajaService
    {
        List<Usuario> Listar();
    }
}
