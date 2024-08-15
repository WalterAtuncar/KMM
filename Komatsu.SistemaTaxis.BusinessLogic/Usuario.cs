using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Threading.Tasks;
using BE = Komatsu.SistemaTaxis.BusinessEntities;
using DA = Komatsu.SistemaTaxis.DataAccess;
//using HP = Komatsu.SistemaTaxis.Helpers;

namespace Komatsu.SistemaTaxis.BusinessLogic
{
    public class Usuario
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (20/07/2018)
        //Utilizado por	: WebApi.Usuario.Recuperar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite recuperar un registro de la tabla Usuario
        /// </summary>
        public BE.Usuario Recuperar(BE.Usuario entUsuario)
        {
            BE.Usuario lisQuery = null;
            try
            {
                lisQuery = new DA.Usuario().Recuperar(entUsuario);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lisQuery;
        }
    }
}
