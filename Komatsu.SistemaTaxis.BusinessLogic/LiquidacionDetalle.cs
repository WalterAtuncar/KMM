using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Threading.Tasks;
using BE = Komatsu.SistemaTaxis.BusinessEntities;
using DA = Komatsu.SistemaTaxis.DataAccess;

namespace Komatsu.SistemaTaxis.BusinessLogic
{
    public class LiquidacionDetalle
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/06/2018)
        //Utilizado por	: WebApi.LiquidacionDetalle.Liquidacion
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// 
        /// </summary>
        public ICollection<BE.LiquidacionDetalleLiquidar> Liquidacion(BE.LiquidacionDetalle entLiquidacionDetalle)
        {
            List<BE.LiquidacionDetalleLiquidar> lisQuery = null;
            try
            {
                IEnumerable<BE.LiquidacionDetalleLiquidar> entQuery = new DA.LiquidacionDetalle().Liquidacion(entLiquidacionDetalle);
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
