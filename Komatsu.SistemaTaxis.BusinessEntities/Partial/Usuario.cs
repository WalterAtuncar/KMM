using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komatsu.SistemaTaxis.BusinessEntities
{
    public partial class Usuario
    {
        public enum Query
        {
            None,
            Recuperar,
        }

        #region Campos aumentados
        #endregion

        public Usuario() { }

        public Usuario(IDataReader reader, Query query = Query.None)
        {
            switch (query)
            {
                case Query.Recuperar:
                    ID = reader.GetSafeInt32("ID");
                    SubjectID = reader.GetSafeString("SubjectID");
                    Nombres = reader.GetSafeString("Nombres");
                    Apellidos = reader.GetSafeString("Apellidos");
                    UserName = reader.GetSafeString("UserName");
                    Email = reader.GetSafeString("Email");
                    Telefono = reader.GetSafeString("Telefono");
                    TipoDocumento = reader.GetSafeString("TipoDocumento");
                    NumeroDocumento = reader.GetSafeString("NumeroDocumento");
                    IdPerfil = reader.GetSafeInt32("IdPerfil");
                    CodigoCentroCosto = reader.GetSafeString("CodigoCentroCosto");
                    CodigoSociedad = reader.GetSafeString("CodigoSociedad");
                    IdEstado = reader.GetSafeString("IdEstado");
                    break;
                default:
                    break;
            }
        }
    }
}
