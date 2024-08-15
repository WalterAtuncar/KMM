using System.Collections.Generic;

namespace GASTO.BusinessLogic.Solicitud.Consultas
{
    public interface IListSolicitudBySearchQuery
    {
        List<ListSolicitudItem> Execute(
            int page, 
            int pageSize, 
            out int total);
    }
}
