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
    public class Destino
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/07/2018)
        //Utilizado por	: WebApi.Destino.Grabar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite grabar un registro de la tabla Destino, retorna ID generado
        /// </summary>
        public int Grabar(BE.Destino entDestino)
        {
            int intResultado = 0;
            try
            {
                intResultado = new DA.Destino().Grabar(entDestino);
            }
            catch (Exception e)
            {
                throw e;
            }
            return intResultado;
        }
    }
}
