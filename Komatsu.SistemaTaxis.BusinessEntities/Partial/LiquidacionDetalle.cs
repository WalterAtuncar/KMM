using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class LiquidacionDetalle
    {
        public enum Query
        {
            None,
        }

        #region Campos aumentados
        #endregion

        public LiquidacionDetalle() { }

        public LiquidacionDetalle(IDataReader reader, Query query = Query.None)
        {
            switch (query)
            {
                case Query.None:
                default:
                    break;
            }
        }
    }
}
