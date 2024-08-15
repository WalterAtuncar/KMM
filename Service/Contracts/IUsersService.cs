using Data;
using Data.Util;
using System;
using System.Collections.Generic;

namespace Service.Contracts
{
    public interface IUsersService 
    {
        IEnumerable<Usuario> Get(Func<Usuario, bool> filter = null);
        List<Data.Common.Usuario> GetList();
        List<Data.Common.Usuario> GetBySociedad(string CodigoSociedad);
        List<Data.Common.Usuario> GetBySociedadGastos(string CodigoSociedad);
        Data.Common.Usuario GetById(int id);
        Data.Common.Usuario GetByUserName(string username);
        int Modificar(Data.Common.Usuario usuario, string username);
        List<Data.Common.Usuario> GetListResponsable();
        List<Data.Common.Usuario> GetLisPaginado(Parameter parameter, out int totalRows, out int cantidadRegistros);
        int GetValidateByUsername(string username);
    }
}
