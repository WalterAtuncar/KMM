using System;
using System.Collections.Generic;
using System.Linq;

using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.TipoDocumentoImpuesto
{
    public class TipoDocumentoImpuesto
    {
        public ICollection<BE_GASTO.TipoDocumentoImpuesto> List()
        {
            List<BE_GASTO.TipoDocumentoImpuesto> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.TipoDocumentoImpuesto> entQuery = new DA_GASTO.TipoDocumentoImpuesto().List();
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
