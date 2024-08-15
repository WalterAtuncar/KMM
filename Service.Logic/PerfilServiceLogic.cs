using Data;
using Service.Contracts.Perfil;
using Service.Logic.Contract;
using System.Collections.Generic;

namespace Service.Logic
{
    public class PerfilServiceLogic : IPerfilServiceLogic
    {
        private readonly IPerfilService oIPerfilService;
        public PerfilServiceLogic(IPerfilService oIPerfilService)
        {
            this.oIPerfilService = oIPerfilService;
        }
        public List<Perfil> GetList()
        {
            return oIPerfilService.GetList();
        }
    }
}
