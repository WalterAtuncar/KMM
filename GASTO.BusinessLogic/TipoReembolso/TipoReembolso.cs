using System;
using System.Collections.Generic;
using System.Linq;

using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.TipoReembolso
{
    public class TipoReembolso
    {
        public ICollection<BE_GASTO.TipoReembolso> List()
        {
            List<BE_GASTO.TipoReembolso> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.TipoReembolso> entQuery = new DA_GASTO.TipoReembolso().List();
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
