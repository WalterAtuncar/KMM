using Komatsu.SistemaTaxis.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BE = Komatsu.SistemaTaxis.BusinessEntities;
using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.Solicitud
{
    public class Solicitud
    {

        public BE.OperationResult Grabar(BE_GASTO.Solicitud entSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {
                   
                    intResultado = new DA_GASTO.Solicitud().Grabar(entSolicitud);
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

        public BE.OperationResult Actualizar(BE_GASTO.Solicitud entSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {

                    intResultado = new DA_GASTO.Solicitud().Actualizar(entSolicitud);
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
        public BE.OperationResult Aprobar(BE_GASTO.Solicitud entSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {

                    intResultado = new DA_GASTO.Solicitud().Aprobar(entSolicitud);
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

        public BE.OperationResult Eliminar(BE_GASTO.Solicitud entSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {

                    intResultado = new DA_GASTO.Solicitud().Eliminar(entSolicitud);
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

        public BE.OperationResult RendicionSolicitud(BE_GASTO.RendicionSolicitud entRendicionSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {

                    intResultado = new DA_GASTO.Solicitud().RendicionSolicitud(entRendicionSolicitud);
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

        public BE.OperationResult Rechazar(BE_GASTO.Solicitud entSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {

                    intResultado = new DA_GASTO.Solicitud().Rechazar(entSolicitud);
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

        public BE.OperationResult AprobarContabilidad(BE_GASTO.Solicitud entSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {

                    intResultado = new DA_GASTO.Solicitud().AprobarContabilidad(entSolicitud);
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

        public BE.OperationResult AprobarContabilidadDetalle(BE_GASTO.DetRendicionSolicitud entDetRendicionSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {

                    intResultado = new DA_GASTO.Solicitud().AprobarContabilidadDetalle(entDetRendicionSolicitud);
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

        public BE.OperationResult RechazarContabilidad(BE_GASTO.Solicitud entSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {

                    intResultado = new DA_GASTO.Solicitud().RechazarContabilidad(entSolicitud);
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

        public BE.OperationResult CambiarEstadoSolicitud(BE_GASTO.Solicitud entSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {

                    intResultado = new DA_GASTO.Solicitud().CambiarEstadoSolicitud(entSolicitud);
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

        public BE_GASTO.Solicitud Recuperar(BE_GASTO.Solicitud entSolicitud)
        {
            BE_GASTO.Solicitud lisQuery = null;
            try
            {
                lisQuery = new DA_GASTO.Solicitud().Recuperar(entSolicitud);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lisQuery;
        }

        public BE_GASTO.Solicitud ValidarAprobadorExterior(BE_GASTO.Solicitud entSolicitud)
        {
            BE_GASTO.Solicitud lisQuery = null;
            try
            {
                lisQuery = new DA_GASTO.Solicitud().ValidarAprobadorExterior(entSolicitud);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lisQuery;
        }

        public BE_GASTO.Solicitud DatosEmail(int idSolicitud)
        {
            BE_GASTO.Solicitud solicitud;
            try
            {
                solicitud = new DA_GASTO.Solicitud().DatosEmail(idSolicitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return solicitud;
        }
        public BE_GASTO.Solicitud RecuperarDetalle(BE_GASTO.Solicitud entSolicitud)
        {
            BE_GASTO.Solicitud lisQuery = null;
            try
            {
                lisQuery = new DA_GASTO.Solicitud().RecuperarDetalle(entSolicitud);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lisQuery;
        }

        public ICollection<BE_GASTO.Solicitud> List(BE_GASTO.Solicitud entSolicitud, out int intTotalRows, out int intCantidadRegistros)
        {
            List<BE_GASTO.Solicitud> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.Solicitud> entQuery = new DA_GASTO.Solicitud().List(entSolicitud, out intTotalRows, out intCantidadRegistros);
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

        public ICollection<BE_GASTO.Solicitud> Paginado(BE_GASTO.Solicitud entSolicitud, string UsuarioSociedad, out int intTotalRows, out int intCantidadRegistros)
        {
            List<BE_GASTO.Solicitud> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.Solicitud> entQuery = new DA_GASTO.Solicitud().Paginado(entSolicitud, UsuarioSociedad ,out intTotalRows, out intCantidadRegistros);
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

        public ICollection<BE_GASTO.Solicitud> PaginadoAprobacion(BE_GASTO.Solicitud entSolicitud,string UsuarioSociedad, out int intTotalRows, out int intCantidadRegistros, out int intIdDestino)
        {
            List<BE_GASTO.Solicitud> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.Solicitud> entQuery = new DA_GASTO.Solicitud().PaginadoAprobacion(entSolicitud, UsuarioSociedad, out intTotalRows, out intCantidadRegistros, out intIdDestino);
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
        public ICollection<BE_GASTO.Solicitud> PaginadoAprobacionDelegaciones(BE_GASTO.Solicitud entSolicitud, out int intTotalRows, out int intCantidadRegistros, out int intIdDestino)
        {
            List<BE_GASTO.Solicitud> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.Solicitud> entQuery = new DA_GASTO.Solicitud().PaginadoAprobacionDelegaciones(entSolicitud, out intTotalRows, out intCantidadRegistros, out intIdDestino);
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

        public ICollection<BE_GASTO.Solicitud> PaginadoAprobacionContabilidad(BE_GASTO.Solicitud entSolicitud,string UsuarioSociedad, out int intTotalRows, out int intCantidadRegistros)
        {
            List<BE_GASTO.Solicitud> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.Solicitud> entQuery = new DA_GASTO.Solicitud().PaginadoAprobacionContabilidad(entSolicitud, UsuarioSociedad, out intTotalRows, out intCantidadRegistros);
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

        public ICollection<BE_GASTO.Solicitud> PaginadoAprobacionDelegacion(BE_GASTO.Solicitud entSolicitud, out int intTotalRows, out int intCantidadRegistros)
        {
            List<BE_GASTO.Solicitud> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.Solicitud> entQuery = new DA_GASTO.Solicitud().PaginadoDelegacion(entSolicitud, out intTotalRows, out intCantidadRegistros);
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
        public ICollection<UsuarioCentroCosto> List_UsuarioBySucursal(string idsociedad,int idsucursal)
        {
            List<UsuarioCentroCosto> lisQuery = null;
            try
            {
                IEnumerable<UsuarioCentroCosto> entQuery = new DA_GASTO.Solicitud().List_UsuarioBySucursal(idsociedad,idsucursal);
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
        public ICollection<UsuarioCentroCosto> List_UsuarioByCentroCosto(string codigocentrocosto)
        {
            List<UsuarioCentroCosto> lisQuery = null;
            try
            {
                IEnumerable<UsuarioCentroCosto> entQuery = new DA_GASTO.Solicitud().List_UsuarioByCentroCosto(codigocentrocosto);
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
    }
}
