using System;

using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.RendicionSolicitud
{
    public class RendicionSolicitud
    {
        

        public BE_GASTO.RendicionSolicitud RecuperarRendicion(BE_GASTO.RendicionSolicitud entRendicionSolicitud)
        {
            BE_GASTO.RendicionSolicitud lisQuery = null;
            try
            {
                lisQuery = new DA_GASTO.RendicionSolicitud().RecuperarRendicion(entRendicionSolicitud);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lisQuery;
        }

       


    }
}
