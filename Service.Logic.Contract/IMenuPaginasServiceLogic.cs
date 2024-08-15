using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Logic.Contract
{
    public interface IMenuPaginasServiceLogic
    {
        List<Data.Common.MenuPaginas> ListaMenus(string UserName);
        List<Data.Common.Paginas> ListaPaginas(string UserName);
    }
}
