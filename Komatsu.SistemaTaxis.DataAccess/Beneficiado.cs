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
    public class Beneficiado : Base
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/07/2018)
        //Utilizado por	: BusinessLogic.Beneficiado.Grabar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite grabar un registro de la tabla Beneficiado, retorna ID generado
        /// </summary>
        public int Grabar(BE.Beneficiado entBeneficiado)
        {
            int intResultado = 0;
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.Beneficiado_Ins_Grabar");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32, entBeneficiado.ID);
            db.AddInParameter(dbCommand, "@SolicitudID", DbType.Int32, entBeneficiado.SolicitudID);
            db.AddInParameter(dbCommand, "@IdTipoDocumento", DbType.Int32, entBeneficiado.IdTipoDocumento);
            db.AddInParameter(dbCommand, "@UsersID", DbType.Int32, entBeneficiado.UsersID);
            db.AddInParameter(dbCommand, "@ApellidoPaterno", DbType.String, entBeneficiado.ApellidoPaterno);
            db.AddInParameter(dbCommand, "@ApellidoMaterno", DbType.String, entBeneficiado.ApellidoMaterno);
            db.AddInParameter(dbCommand, "@Nombre", DbType.String, entBeneficiado.Nombre);
            db.AddInParameter(dbCommand, "@Telefono", DbType.String, entBeneficiado.Telefono);
            db.AddInParameter(dbCommand, "@FlagPrincipal", DbType.Boolean, entBeneficiado.FlagPrincipal);
            db.AddInParameter(dbCommand, "@NumeroDocumento", DbType.String, entBeneficiado.NumeroDocumento);
            db.AddInParameter(dbCommand, "@CodigoCentroCosto", DbType.String, entBeneficiado.CodigoCentroCosto);
            db.AddInParameter(dbCommand, "@Apellidos", DbType.String, entBeneficiado.Apellidos);
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
    }
}
