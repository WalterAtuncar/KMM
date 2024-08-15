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
    public class Usuario : Base
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (20/07/2018)
        //Utilizado por	: BusinessLogic.Usuario.Recuperar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite recuperar un registro de la tabla Usuario
        /// </summary>
        public BE.Usuario Recuperar(BE.Usuario entUsuario)
        {
            Database db = new GenericDatabase(ConexionSQL, System.Data.SqlClient.SqlClientFactory.Instance);
            DbCommand dbCommand = db.GetStoredProcCommand("dbo.Usuario_Sel_Recuperar");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32, entUsuario.ID);
            BE.Usuario lista = null;
            try
            {
                using (IDataReader reader = db.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                        lista = new BE.Usuario(reader, BE.Usuario.Query.Recuperar);
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
