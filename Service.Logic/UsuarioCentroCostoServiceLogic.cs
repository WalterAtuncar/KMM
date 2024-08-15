using Data.Common;
using Service.Contracts;
using Service.Logic.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Logic
{
    public class UsuarioCentroCostoServiceLogic : IUsuarioCentroCostoServiceLogic
    {
        private readonly IUsuarioCentroCostoService oIUsuarioCentroCostoService;
        public UsuarioCentroCostoServiceLogic(IUsuarioCentroCostoService oIUsuarioCentroCostoService)
        {
            this.oIUsuarioCentroCostoService = oIUsuarioCentroCostoService;
        }
        public async Task<List<UsuarioCentroCosto>> GetUsuarioCentroCostoById(int IdUsuario)
        {
            var result = await oIUsuarioCentroCostoService.GetUsuarioCentroCostoById(IdUsuario);
            return result;
        }
    }
}
