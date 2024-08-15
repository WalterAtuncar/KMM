using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BE = Komatsu.SistemaTaxis.BusinessEntities;
using DA = Komatsu.SistemaTaxis.DataAccess;

namespace Komatsu.SistemaTaxis.BusinessLogic
{
    public class Solicitud
    {
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/07/2018)
        //Utilizado por	: WebApi.Solicitud.Grabar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite grabar un registro de la tabla Solicitud, retorna ID generado
        /// </summary>
        public BE.OperationResult Grabar(BE.Solicitud entSolicitud)
        {
            int intResultado = 0;
            int intId = 0;
            BE.Usuario lisUsuario = null;
            BE.OperationResult objResult = new BE.OperationResult();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                try
                {
                    entSolicitud.IdTipoPago = 1;
                    entSolicitud.IdEstadoServicioProveedor = 1;
                    entSolicitud.IdSituacionServicio = 1;
                    entSolicitud.Liquidado = false;
                    entSolicitud.NroOrdenServicio = entSolicitud.NroOrdenServicio;
                    intResultado = new DA.Solicitud().Grabar(entSolicitud);
                    objResult.NewID = intResultado;

                    entSolicitud.SolicitudDetalle.IdSolicitud = intResultado;
                    entSolicitud.SolicitudDetalle.IdSituacion = 1;
                    entSolicitud.SolicitudDetalle.UsuarioRegistro = entSolicitud.UsuarioRegistro;
                    intId = new DA.SolicitudDetalle().Grabar(entSolicitud.SolicitudDetalle);

                    foreach (BE.Destino entDestino in entSolicitud.DestinoList)
                    {
                        entDestino.IdSolicitud = intResultado;
                        new DA.Destino().Grabar(entDestino);
                    }

                    foreach (BE.Beneficiado entBeneficiado in entSolicitud.BeneficiadoList)
                    {
                        entBeneficiado.SolicitudID = intResultado;
                        lisUsuario = new DA.Usuario().Recuperar(new BE.Usuario { ID = entBeneficiado.UsersID });
                        entBeneficiado.Apellidos = lisUsuario.Apellidos;
                        entBeneficiado.Nombre = lisUsuario.Nombres;
                        entBeneficiado.IdTipoDocumento = Convert.ToInt32(lisUsuario.TipoDocumento);
                        entBeneficiado.NumeroDocumento = lisUsuario.NumeroDocumento;
                        entBeneficiado.CodigoCentroCosto = lisUsuario.CodigoCentroCosto;
                        new DA.Beneficiado().Grabar(entBeneficiado);
                    }

                    foreach (BE.SolicitudProveedorTaxi entSolicitudProveedorTaxi in entSolicitud.SolicitudProveedorTaxiList)
                    {
                        entSolicitudProveedorTaxi.SolicitudID = intResultado;
                        new DA.SolicitudProveedorTaxi().Grabar(entSolicitudProveedorTaxi);
                    }
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
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (25/07/2018)
        //Utilizado por	: WebApi.Solicitud.Actualizar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite actualizar un registro de la tabla Solicitud
        /// </summary>
        public BE.OperationResult Actualizar(BE.Solicitud entSolicitud)
        {
            int intResultado = 0;
            BE.OperationResult objResult = new BE.OperationResult();

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    new DA.Solicitud().Actualizar(entSolicitud);
                    intResultado = (int)entSolicitud.ID;

                    if (entSolicitud.DestinoList != null)
                    {
                        new DA.Destino().Eliminar(new BE.Destino { IdSolicitud = intResultado });

                        foreach (BE.Destino entDestino in entSolicitud.DestinoList)
                        {
                            entDestino.IdSolicitud = intResultado;
                            new DA.Destino().Grabar(entDestino);
                        }
                    }

                    if (entSolicitud.SolicitudProveedorTaxiList != null)
                    {
                        new DA.SolicitudProveedorTaxi().Eliminar(new BE.SolicitudProveedorTaxi { SolicitudID = intResultado });

                        foreach (BE.SolicitudProveedorTaxi entSolicitudProveedorTaxi in entSolicitud.SolicitudProveedorTaxiList)
                        {
                            entSolicitudProveedorTaxi.SolicitudID = intResultado;
                            new DA.SolicitudProveedorTaxi().Grabar(entSolicitudProveedorTaxi);
                        }
                    }
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
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/07/2018)
        //Utilizado por	: WebApi.Solicitud.Rechazar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite rechazar un registro de la tabla Solicitud
        /// </summary>
        public BE.OperationResult Rechazar(BE.Solicitud entSolicitud)
        {
            int intResultado = 0;
            int intId = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            BE.SolicitudDetalle objSolicitudDetalle = new BE.SolicitudDetalle();

            BE.Solicitud lisSolicitud = new DA.Solicitud().Recuperar(entSolicitud);
            if (lisSolicitud.IdSituacionServicio == 4)
            {
                return new BE.OperationResult() { Success = false, Message = "La solicitud ya fue rechazada." };
            }

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    new DA.Solicitud().Actualizar(entSolicitud);
                    intResultado = (int)entSolicitud.ID;

                    objSolicitudDetalle.IdSolicitud = intResultado;
                    intId = new DA.SolicitudDetalle().Grabar(entSolicitud.SolicitudDetalle);
                }
                catch (Exception e)
                {
                    ts.Dispose();
                    return new BE.OperationResult() { Success = false, Message = e.Message };
                }
                ts.Complete();
            }
            return objResult;
        }
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (18/07/2018)
        //Utilizado por	: WebApi.Solicitud.Cancelar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite cancelar un registro de la tabla Solicitud
        /// </summary>
        public BE.OperationResult Cancelar(BE.Solicitud entSolicitud)
        {
            int intResultado = 0;
            int intId = 0;
            BE.OperationResult objResult = new BE.OperationResult();
            BE.SolicitudDetalle objSolicitudDetalle = new BE.SolicitudDetalle();

            BE.Solicitud lisSolicitud = new DA.Solicitud().Recuperar(entSolicitud);
            if (lisSolicitud.IdSituacionServicio == 3)
            {
                return new BE.OperationResult() { Success = false, Message = "La solicitud ya fue cancelada." };
            }

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    new DA.Solicitud().Actualizar(entSolicitud);
                    intResultado = (int)entSolicitud.ID;

                    objSolicitudDetalle.IdSolicitud = intResultado;
                    intId = new DA.SolicitudDetalle().Grabar(entSolicitud.SolicitudDetalle);
                }
                catch (Exception e)
                {
                    ts.Dispose();
                    return new BE.OperationResult() { Success = false, Message = e.Message };
                }
                ts.Complete();
            }
            return objResult;
        }
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (01/08/2018)
        //Utilizado por	: WebApi.Solicitud.Recuperar
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// Permite recuperar un registro de la tabla Solicitud
        /// </summary>
        public BE.Solicitud Recuperar(BE.Solicitud entSolicitud)
        {
            BE.Solicitud lisQuery = null;
            try
            {
                lisQuery = new DA.Solicitud().Recuperar(entSolicitud);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lisQuery;
        }
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (08/05/2018)
        //Utilizado por	: WebApi.Solicitud.List
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// 
        /// </summary>
        public ICollection<BE.Solicitud> List(BE.Solicitud entSolicitud, out int intTotalRows, out int intCantidadRegistros)
        {
            List<BE.Solicitud> lisQuery = null;
            try
            {
                IEnumerable<BE.Solicitud> entQuery = new DA.Solicitud().List(entSolicitud, out intTotalRows, out intCantidadRegistros);
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
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (09/05/2018)
        //Utilizado por	: WebApi.Solicitud.List
        //――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// 
        /// </summary>
        public ICollection<BE.Solicitud> AprobadorList(BE.Solicitud entSolicitud, out int intTotalRows, out int intCantidadRegistros)
        {
            List<BE.Solicitud> lisQuery = null;
            try
            {
                IEnumerable<BE.Solicitud> entQuery = new DA.Solicitud().AprobadorList(entSolicitud, out intTotalRows, out intCantidadRegistros);
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
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Carlos Huallpa (14/06/2018)
        //Utilizado por	: WebApi.Solicitud.Paginado
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// 
        /// </summary>
        public ICollection<BE.SolicitudBuscarPaginado> Paginado(BE.Solicitud entSolicitud)
        {
            List<BE.SolicitudBuscarPaginado> lisQuery = null;
            try
            {
                IEnumerable<BE.SolicitudBuscarPaginado> entQuery = new DA.Solicitud().Paginado(entSolicitud);
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

        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        //Creado por	: Marcos Silverio (14/06/2018)
        //Utilizado por	: WebApi.Solicitud.GetUsuarioCentroCostoByCeco
        //―――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――――
        /// <summary>
        /// 
        /// </summary>
        public ICollection<BE.UsuarioCentroCosto> GetUsuarioCentroCostoByCeco(string centroCosto)
        {
            List<BE.UsuarioCentroCosto> lisQuery = null;
            try
            {
                IEnumerable<BE.UsuarioCentroCosto> entQuery = new DA.Solicitud().List_UsuarioCentroCostoByCeco(centroCosto);
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

        public bool Update_UsuarioAprobador(int idSolicitud, int idUsuarioAprobador)
        {
            var success = new DA.Solicitud().Update_UsuarioAprobador(idSolicitud, idUsuarioAprobador);
            return success;
        }

        public Dictionary<string, object> GetSolicitudById(int id)
        {
            var dataSolicitud = new DA.Solicitud().GetSolicitudById(id);
            return dataSolicitud;
        }
    }
}
