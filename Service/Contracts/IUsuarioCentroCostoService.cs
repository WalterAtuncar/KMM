using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IUsuarioCentroCostoService
    {
        Task<List<Data.Common.UsuarioCentroCosto>> GetUsuarioCentroCostoById(int IdUsuario);
    }
}
