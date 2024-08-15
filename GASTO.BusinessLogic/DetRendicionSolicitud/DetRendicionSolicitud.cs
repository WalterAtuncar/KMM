using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BE = Komatsu.SistemaTaxis.BusinessEntities;
using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.DetRendicionSolicitud
{
    public class DetRendicionSolicitud
    {
        public ICollection<BE_GASTO.DetRendicionSolicitud> Paginado(BE_GASTO.DetRendicionSolicitud entDetRendicionSolicitud, out int intTotalRows, out int intCantidadRegistros)
        {
            List<BE_GASTO.DetRendicionSolicitud> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.DetRendicionSolicitud> entQuery = new DA_GASTO.DetRendicionSolicitud().Paginado(entDetRendicionSolicitud, out intTotalRows, out intCantidadRegistros);
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

        public IList<BE_GASTO.DetRendicionSolicitud> PaginadoAll(int IdSolicitud)
        {
            List<BE_GASTO.DetRendicionSolicitud> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.DetRendicionSolicitud> entQuery = new DA_GASTO.DetRendicionSolicitud().PaginadoAll(IdSolicitud);
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

        public IList<BE_GASTO.DetRendicionSolicitud> RecuperarDetalleLista(int IdSolicitud)
        {
            List<BE_GASTO.DetRendicionSolicitud> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.DetRendicionSolicitud> entQuery = new DA_GASTO.DetRendicionSolicitud().RecuperarDetalleLista(IdSolicitud);
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

        public BE_GASTO.DetRendicionSolicitud RecuperarDetalle(BE_GASTO.DetRendicionSolicitud entRendicionSolicitud)
        {
            BE_GASTO.DetRendicionSolicitud lisQuery = null;
            try
            {
                lisQuery = new DA_GASTO.DetRendicionSolicitud().RecuperarDetalle(entRendicionSolicitud);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lisQuery;
        }

        public BE.OperationResult Grabar(BE_GASTO.DetRendicionSolicitud entRendicionSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {

                    intResultado = new DA_GASTO.DetRendicionSolicitud().Grabar(entRendicionSolicitud);
                    objResult.NewID = intResultado;

                }
                catch (Exception e)
                {
                    ts.Dispose();
                    objResult.Success = false;
                    objResult.Message = e.Message;
                    return objResult;
                }
                ts.Complete();
            }
            return objResult;
        }

        public BE.OperationResult Eliminar(BE_GASTO.DetRendicionSolicitud entDetRendicionSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {

                    intResultado = new DA_GASTO.DetRendicionSolicitud().Eliminar(entDetRendicionSolicitud);
                    objResult.NewID = intResultado;

                }
                catch (Exception e)
                {
                    ts.Dispose();
                    objResult.Success = false;
                    objResult.Message = e.Message;
                    return objResult;
                }
                ts.Complete();
            }
            return objResult;
        }

        public BE.OperationResult Actualizar(BE_GASTO.DetRendicionSolicitud entRendicionSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {

                    intResultado = new DA_GASTO.DetRendicionSolicitud().Actualizar(entRendicionSolicitud);
                    objResult.NewID = intResultado;

                }
                catch (Exception e)
                {
                    ts.Dispose();
                    objResult.Success = false;
                    objResult.Message = e.Message;
                    return objResult;
                }
                ts.Complete();
            }
            return objResult;
        }



    }
}
