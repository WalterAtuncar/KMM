using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class Destino
    {
        public enum Query
        {
            None
        }

        #region Campos aumentados
        #endregion

        public Destino() { }

        public Destino(IDataReader reader, Query query = Query.None)
        {
            switch (query)
            {
                case Query.None:
                    break;
                default:
                    break;
            }
        }
    }
}
