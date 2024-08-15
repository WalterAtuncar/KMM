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
    public class Liquidacion
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Erick Huamán (10/07/2018)
        //Utilizado por	: WebApi.Liquidacion.List
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// 
        /// </summary>
        public ICollection<BE.Liquidacion> List(BE.Liquidacion entLiquidacion, out int intTotalRows, out int intCantidadRegistros)
        {
            List<BE.Liquidacion> lisQuery = null;
            try
            {
                IEnumerable<BE.Liquidacion> entQuery = new DA.Liquidacion().List(entLiquidacion, out intTotalRows, out intCantidadRegistros);
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

        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (14/06/2018)
        //Utilizado por	: WebApi.Liquidacion.Grabar
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite grabar un registro de la tabla Liquidacion, retorna ID generado
        /// </summary>
        public int Grabar(BE.LiquidacionGrabar entLiquidacion)
        {
            int intResultado = 0;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {
                    intResultado = new DA.Liquidacion().Grabar(entLiquidacion);

                    foreach (BE.LiquidacionDetalle item in entLiquidacion.LiquidacionDetalleLis)
                    {
                        item.LiquidacionID = intResultado;
                        new DA.LiquidacionDetalle().Grabar(item);
                    }
                }
                catch (Exception e)
                {
                    ts.Dispose();
                    throw e;
                }
                ts.Complete();
            }
            return intResultado;
        }
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (15/06/2018)
        //Utilizado por	: WebApi.Liquidacion.ExportByID
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite exportar los datos al excel y al correo
        /// </summary>
        public ICollection<BE.LiquidacionExportar> ExportByID(BE.Liquidacion entLiquidacion)
        {
            List<BE.LiquidacionExportar> lisQuery = null;
            try
            {
                IEnumerable<BE.LiquidacionExportar> entQuery = new DA.Liquidacion().ExportByID(entLiquidacion);
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
