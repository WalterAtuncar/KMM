using System;
using System.Collections.Generic;
using Data.Common;
using Service.Logic.Contract;
using Service.Contracts;
using Data.Util;

namespace Service.Logic
{
    public class MenuPaginasServiceLogic : IMenuPaginasServiceLogic
    {
        private readonly IMenuPaginasService oIMenuPaginasService;
        public MenuPaginasServiceLogic(IMenuPaginasService oIMenuPaginasService)
        {
            this.oIMenuPaginasService = oIMenuPaginasService;
        }
        public List<MenuPaginas> ListaMenus(string UserName)
        {
            return oIMenuPaginasService.ListaMenus(UserName);
        }

        public List<Paginas> ListaPaginas(string UserName)
        {
            return oIMenuPaginasService.ListaPaginas(UserName);
        }
    }
}
