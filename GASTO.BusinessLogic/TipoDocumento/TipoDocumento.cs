using System;
using System.Collections.Generic;
using System.Linq;

using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.TipoDocumento
{
    public class TipoDocumento
    {
        public ICollection<BE_GASTO.TipoDocumento> List(int idtipo)
        {
            List<BE_GASTO.TipoDocumento> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.TipoDocumento> entQuery = new DA_GASTO.TipoDocumento().List(idtipo);
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
