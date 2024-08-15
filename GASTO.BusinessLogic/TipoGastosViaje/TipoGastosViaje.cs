using System;
using System.Collections.Generic;
using System.Linq;

using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.TipoGastosViaje
{
    public class TipoGastosViaje
    {
        public ICollection<BE_GASTO.TipoGastosViaje> List()
        {
            List<BE_GASTO.TipoGastosViaje> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.TipoGastosViaje> entQuery = new DA_GASTO.TipoGastosViaje().List();
                if (entQuery != null)
                {
                    lisQuery = entQuery.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lisQuery;
        }
    }
}
