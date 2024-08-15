using Modelo.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IEmailService
    {
        //Task<Data.Common.Usuario> GetListResponsableByCentroCosto(string codigo);
        //Task<List<Data.Common.Usuario>> GetListAprobadorByCentroCosto(string codigo);
        //Task<List<Data.Common.Usuario>> GetListSoporteAdministrativo();
        //Task<Data.Common.Usuario> GetUsuarioRegistroBySolicitud(int id);
        Task<List<EmailAdjunto>> GetListEmailBySolicitud(int newID, int? IdServicioProveedor, int IdTipoCorreo);
    }
}
