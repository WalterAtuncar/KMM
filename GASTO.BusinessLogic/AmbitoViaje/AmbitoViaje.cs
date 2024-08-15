using System;
using System.Collections.Generic;
using System.Linq;

using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.AmbitoViaje
{
    public class AmbitoViaje
    {
        public ICollection<BE_GASTO.AmbitoViaje> List()
        {
            List<BE_GASTO.AmbitoViaje> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.AmbitoViaje> entQuery = new DA_GASTO.AmbitoViaje().List();
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
