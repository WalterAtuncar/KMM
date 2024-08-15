using System;
using System.Collections.Generic;
using System.Linq;

using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.Pais
{
    public class Pais
    {
        public ICollection<BE_GASTO.Pais> List()
        {
            List<BE_GASTO.Pais> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.Pais> entQuery = new DA_GASTO.Pais().List();
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
