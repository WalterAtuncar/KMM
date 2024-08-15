using System;
using System.Collections.Generic;
using System.Linq;

using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.Sucursal
{
    public class Sucursal
    {
        public ICollection<BE_GASTO.Sucursal> List()
        {
            List<BE_GASTO.Sucursal> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.Sucursal> entQuery = new DA_GASTO.Sucursal().List();
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
