using System;
using System.Collections.Generic;

namespace Service.Contracts
{
    public interface IMenuPaginasService : IDisposable
    {
        List<Data.Common.MenuPaginas> ListaMenus(string UserName);
        List<Data.Common.Paginas> ListaPaginas(string UserName);
    }
}
