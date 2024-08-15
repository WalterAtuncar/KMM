using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BE = Komatsu.SistemaTaxis.BusinessEntities;
using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.Delegaciones
{
    public class Delegaciones
    {
        public ICollection<BE_GASTO.Delegaciones> Paginado(BE_GASTO.Delegaciones entDelegaciones, out int intTotalRows, out int intCantidadRegistros)
        {
            List<BE_GASTO.Delegaciones> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.Delegaciones> entQuery = new DA_GASTO.Delegaciones().Paginado(entDelegaciones, out intTotalRows, out intCantidadRegistros);
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

        

        public IList<BE_GASTO.Delegaciones> RecuperarDetalleLista(int IdSolicitud)
        {
            List<BE_GASTO.Delegaciones> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.Delegaciones> entQuery = new DA_GASTO.Delegaciones().RecuperarDetalleLista(IdSolicitud);
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

        public BE_GASTO.Delegaciones RecuperarDetalle(BE_GASTO.Delegaciones entRendicionSolicitud)
        {
            BE_GASTO.Delegaciones lisQuery = null;
            try
            {
                lisQuery = new DA_GASTO.Delegaciones().RecuperarDetalle(entRendicionSolicitud);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lisQuery;
        }

        public BE.OperationResult Grabar(BE_GASTO.Delegaciones entRendicionSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {

                    intResultado = new DA_GASTO.Delegaciones().Grabar(entRendicionSolicitud);
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

        public BE.OperationResult Eliminar(BE_GASTO.Delegaciones entDelegaciones)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {

                    intResultado = new DA_GASTO.Delegaciones().Eliminar(entDelegaciones);
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

        public BE.OperationResult Revocar(BE_GASTO.Delegaciones entDelegaciones)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {

                    intResultado = new DA_GASTO.Delegaciones().Revocar(entDelegaciones);
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

        public BE.OperationResult Actualizar(BE_GASTO.Delegaciones entRendicionSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {
                    intResultado = new DA_GASTO.Delegaciones().Actualizar(entRendicionSolicitud);
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
