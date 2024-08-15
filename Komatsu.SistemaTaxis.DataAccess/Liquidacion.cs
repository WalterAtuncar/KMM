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
    public class Liquidacion : Base
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Erick Huamán (10/07/2018)
        //Utilizado por	: BusinessLogic.Liquidacion.List
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BE.Liquidacion> List(BE.Liquidacion entLiquidacion, out int intTotalRows, out int intCantidadRegistros)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.usp_Liquidacion_Listar_Paginado");
            db.AddInParameter(dbCommand, "@Estado", DbType.Int32, entLiquidacion.Estado);
            db.AddInParameter(dbCommand, "@ProveedorTaxiID", DbType.Int32, entLiquidacion.ProveedorTaxiID);
            db.AddInParameter(dbCommand, "@FechaInicio", DbType.DateTime, entLiquidacion.FechaInicio);
            db.AddInParameter(dbCommand, "@FechaFin", DbType.DateTime, entLiquidacion.FechaFin);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, entLiquidacion.PageSize);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, entLiquidacion.PageIndex);
            db.AddOutParameter(dbCommand, "@TotalRows", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "@CantidadRegistros", DbType.Int32, 4);

            List<BE.Liquidacion> lista = new List<BE.Liquidacion>();

            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                        lista.Add(new BE.Liquidacion(reader, BE.Liquidacion.Query.List));
                }

                intTotalRows = Convert.ToInt32(dbCommand.Parameters["@TotalRows"].Value);
                intCantidadRegistros = Convert.ToInt32(dbCommand.Parameters["@CantidadRegistros"].Value);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return lista;
        }

        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (14/06/2018)
        //Utilizado por	: BusinessLogic.Liquidacion.Grabar
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite grabar un registro de la tabla Liquidacion, retorna ID generado
        /// </summary>
        public int Grabar(BE.LiquidacionGrabar entLiquidacion)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.usp_Liquidacion_Ins_Grabar");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32, null);
            db.AddInParameter(dbCommand, "@Codigo", DbType.Int32, null);
            db.AddInParameter(dbCommand, "@Fecha", DbType.DateTime, entLiquidacion.Fecha);
            db.AddInParameter(dbCommand, "@ProveedorTaxiID", DbType.Int32, entLiquidacion.ProveedorTaxiID);
            db.AddInParameter(dbCommand, "@CodigoSociedad", DbType.String, null);
            db.AddInParameter(dbCommand, "@Total", DbType.String, entLiquidacion.Total);
            db.AddInParameter(dbCommand, "@Estado", DbType.Int32, entLiquidacion.Estado);
            db.AddInParameter(dbCommand, "@UsuarioRegistro", DbType.String, entLiquidacion.UsuarioRegistro);
            db.AddInParameter(dbCommand, "@UsuarioCancelo", DbType.String, null);
            db.AddInParameter(dbCommand, "@UserNameCancelo", DbType.String, null);
            db.AddInParameter(dbCommand, "@UsuarioLiquido", DbType.String, null);
            db.AddInParameter(dbCommand, "@FechaLiquidacion", DbType.DateTime, null);
            db.AddInParameter(dbCommand, "@FechaCancelacion", DbType.DateTime, null);
            db.AddInParameter(dbCommand, "@UserNameRegistro", DbType.String, entLiquidacion.UserNameRegistro);
            db.AddInParameter(dbCommand, "@UserNameLiquido", DbType.String, null);
            db.AddInParameter(dbCommand, "@FechaRegistro", DbType.DateTime, null);
            db.AddInParameter(dbCommand, "@UsuarioModifica", DbType.Int32, null);
            db.AddInParameter(dbCommand, "@FechaModifica", DbType.DateTime, null);
            db.AddInParameter(dbCommand, "@SociedadID", DbType.Int32, entLiquidacion.SociedadID);
            db.AddInParameter(dbCommand, "@Observacion", DbType.String, entLiquidacion.Observacion);
            db.AddInParameter(dbCommand, "@Factura", DbType.String, null);
            db.AddInParameter(dbCommand, "@FechaFactura", DbType.DateTime, null);
            db.AddInParameter(dbCommand, "@AsientoContable", DbType.String, null);
            db.AddInParameter(dbCommand, "@Ejercicio", DbType.String, null);
            db.AddInParameter(dbCommand, "@DiaRegistro", DbType.DateTime, null);
            db.AddInParameter(dbCommand, "@UsuarioRegistroSAP", DbType.String, null);
            db.AddInParameter(dbCommand, "@HoraRegistro", DbType.String, null);
            db.AddInParameter(dbCommand, "@Mensaje", DbType.String, null);
            db.AddInParameter(dbCommand, "@FacturaProvision", DbType.String, null);
            db.AddInParameter(dbCommand, "@Fecha_Contable", DbType.DateTime, null);
            db.AddInParameter(dbCommand, "@Moneda", DbType.String, entLiquidacion.Moneda);
            db.AddInParameter(dbCommand, "@Flag_Imp", DbType.String, null);
            db.AddInParameter(dbCommand, "@Indicadore_Igv", DbType.String, null);
            db.AddInParameter(dbCommand, "@Lugar_Comer", DbType.String, entLiquidacion.Lugar_Comer);
            db.AddInParameter(dbCommand, "@Indicador_Cme", DbType.String, null);
            db.AddInParameter(dbCommand, "@Cta_Mayor", DbType.String, null);
            db.AddInParameter(dbCommand, "@Indicador_D_H", DbType.String, null);
            db.AddInParameter(dbCommand, "@NombreFile", DbType.String, null);
            db.AddInParameter(dbCommand, "@RutaCompletaFile", DbType.String, null);
            db.AddOutParameter(dbCommand, "@RetVal", DbType.Int32, 4);
            try
            {
                db.ExecuteNonQuery(dbCommand);
                intResultado = Convert.ToInt32(db.GetParameterValue(dbCommand, "@RetVal"));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return intResultado;
        }
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (15/06/2018)
        //Utilizado por	: BusinessLogic.Liquidacion.ExportByID
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite exportar los datos al excel y al correo
        /// </summary>
        public IEnumerable<BE.LiquidacionExportar> ExportByID(BE.Liquidacion entLiquidacion)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.usp_Liquidacion_Sel_ExportByID");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32, entLiquidacion.ID);
            List<BE.LiquidacionExportar> lista = new List<BE.LiquidacionExportar>();
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                        lista.Add(new BE.LiquidacionExportar(reader, BE.LiquidacionExportar.Query.ExportByID));
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
