using System;
using System.Collections.Generic;
using System.Linq;

using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.Impuesto
{
    public class Impuesto
    {
        public ICollection<BE_GASTO.Impuesto> List()
        {
            List<BE_GASTO.Impuesto> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.Impuesto> entQuery = new DA_GASTO.Impuesto().List();
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
