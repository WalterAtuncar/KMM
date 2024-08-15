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
    public class SolicitudProveedorTaxi
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/07/2018)
        //Utilizado por	: WebApi.SolicitudProveedorTaxi.Grabar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite grabar un registro de la tabla SolicitudProveedorTaxi, retorna ID generado
        /// </summary>
        public int Grabar(BE.SolicitudProveedorTaxi entSolicitudProveedorTaxi)
        {
            int intResultado = 0;
            try
            {
                intResultado = new DA.SolicitudProveedorTaxi().Grabar(entSolicitudProveedorTaxi);
            }
            catch (Exception e)
            {
                throw e;
            }
            return intResultado;
        }
    }
}
