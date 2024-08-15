using System;
using System.Collections.Generic;
using System.Linq;

using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.TipoMoneda
{
    public class TipoMoneda
    {
        public ICollection<BE_GASTO.TipoMoneda> List()
        {
            List<BE_GASTO.TipoMoneda> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.TipoMoneda> entQuery = new DA_GASTO.TipoMoneda().List();
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
