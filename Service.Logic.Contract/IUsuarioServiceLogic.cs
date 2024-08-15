using System.Collections.Generic;

namespace Service.Logic.Contract
{
    public interface IUsuarioServiceLogic
    {
        List<Data.Common.Usuario> GetList();
        OperationResult Modificar(Data.Common.Usuario usuario, string username);
        Data.Common.Usuario GetById(int id);
    }
}
