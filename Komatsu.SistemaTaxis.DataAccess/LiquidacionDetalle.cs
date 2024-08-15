using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE = Komatsu.SistemaTaxis.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Komatsu.SistemaTaxis.DataAccess
{
    public class LiquidacionDetalle : Base
    {
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (14/06/2018)
        //Utilizado por	: BusinessLogic.LiquidacionDetalle.Grabar
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite grabar un registro de la tabla LiquidacionDetalle, retorna ID generado
        /// </summary>
        public void Grabar(BE.LiquidacionDetalle entLiquidacionDetalle)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.usp_LiquidacionDetalle_Ins_Grabar");
            db.AddInParameter(dbCommand, "@LiquidacionDetalleID", DbType.Int32, entLiquidacionDetalle.LiquidacionDetalleID);
            db.AddInParameter(dbCommand, "@LiquidacionID", DbType.Int32, entLiquidacionDetalle.LiquidacionID);
            db.AddInParameter(dbCommand, "@SolicitudID", DbType.Int32, entLiquidacionDetalle.SolicitudID);
            db.AddInParameter(dbCommand, "@OrdenServicio", DbType.String, entLiquidacionDetalle.OrdenServicio);
            db.AddInParameter(dbCommand, "@CentroCostoAfecto", DbType.String, entLiquidacionDetalle.CentroCostoAfecto);
            db.AddInParameter(dbCommand, "@PrecioTarifa", DbType.Double, entLiquidacionDetalle.PrecioTarifa);
            db.AddInParameter(dbCommand, "@PrecioEspera", DbType.Double, entLiquidacionDetalle.PrecioEspera);
            db.AddInParameter(dbCommand, "@PrecioDesvioRuta", DbType.Double, entLiquidacionDetalle.PrecioDesvioRuta);
            db.AddInParameter(dbCommand, "@PrecioPeaje", DbType.Double, entLiquidacionDetalle.PrecioPeaje);
            db.AddInParameter(dbCommand, "@PrecioEstacionamiento", DbType.Double, entLiquidacionDetalle.PrecioEstacionamiento);
            db.AddInParameter(dbCommand, "@TotalGeneral", DbType.Double, entLiquidacionDetalle.TotalGeneral);
            db.AddInParameter(dbCommand, "@PrecioDesplazamiento", DbType.Double, entLiquidacionDetalle.PrecioDesplazamiento);
            db.AddOutParameter(dbCommand, "@RetVal", DbType.Int32, 4);
            try
            {
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/06/2018)
        //Utilizado por	: BusinessLogic.LiquidacionDetalle.Liquidacion
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BE.LiquidacionDetalleLiquidar> Liquidacion(BE.LiquidacionDetalle entLiquidacionDetalle)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.usp_LiquidacionDetalle_Sel_Liquidacion");
            db.AddInParameter(dbCommand, "@IdLiquidacion", DbType.Int32, entLiquidacionDetalle.LiquidacionID);
            List<BE.LiquidacionDetalleLiquidar> lista = new List<BE.LiquidacionDetalleLiquidar>();
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                        lista.Add(new BE.LiquidacionDetalleLiquidar(reader, BE.LiquidacionDetalleLiquidar.Query.Liquidacion));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return lista;
        }

    }
}
