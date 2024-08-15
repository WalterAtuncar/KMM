using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Logic.Contract
{
    public interface IUsuarioCentroCostoServiceLogic
    {
        Task<List<Data.Common.UsuarioCentroCosto>> GetUsuarioCentroCostoById(int IdUsuario);
    }
}
