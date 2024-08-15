using System;
using System.Collections.Generic;
using System.Linq;

using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.AprobadorExterior
{
    public class AprobadorExterior
    {
        public ICollection<BE_GASTO.AprobadorExterior> List()
        {
            List<BE_GASTO.AprobadorExterior> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.AprobadorExterior> entQuery = new DA_GASTO.AprobadorExterior().List();
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
