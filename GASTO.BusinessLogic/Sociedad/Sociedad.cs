using System;
using System.Collections.Generic;
using System.Linq;

using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.Sociedad
{
    public class Sociedad
    {
        public ICollection<BE_GASTO.Sociedad> List()
        {
            List<BE_GASTO.Sociedad> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.Sociedad> entQuery = new DA_GASTO.Sociedad().List();
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
