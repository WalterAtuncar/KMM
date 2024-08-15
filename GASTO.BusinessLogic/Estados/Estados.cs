using System;
using System.Collections.Generic;
using System.Linq;
using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.Estados
{
    public class Estados
    {
        public ICollection<BE_GASTO.Estados> List()
        {
            List<BE_GASTO.Estados> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.Estados> entQuery = new DA_GASTO.Estados().List();
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
