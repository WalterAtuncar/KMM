//using Dapper;
//using Data;
//using Data.Util;
//using Service.Contracts.Proveedor;
//using Service.Util;
//using Service.Xml.ArchivoPlano;
//using Service.Xml.Transaccion;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Transactions;

using Dapper;
using Service.Contracts.Proveedor;
using Servicio.Util;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProveedorService : IProveedorService
    {
        //public async Task<int> Add(Data.Common.ProveedorTaxi proveedor)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@RazonSocial", proveedor.RazonSocial, dbType: DbType.String);
        //        parameters.Add("@RUC", proveedor.RUC, dbType: DbType.String);
        //        parameters.Add("@TiempoReserva", proveedor.TiempoReserva, dbType: DbType.Double);
        //        parameters.Add("@TiempoEspera", proveedor.TiempoEspera, dbType: DbType.Double);
        //        parameters.Add("@Telefono", proveedor.Telefono, dbType: DbType.Int32);
        //        parameters.Add("@Direccion", proveedor.Direccion, dbType: DbType.String);
        //        parameters.Add("@CodigoSAP", proveedor.CodigoSAP, dbType: DbType.String);
        //        parameters.Add("@Contacto", proveedor.Contacto, dbType: DbType.String);
        //        parameters.Add("@Email", proveedor.Email, dbType: DbType.String);
        //        parameters.Add("@IdEstado", proveedor.IdEstado, dbType: DbType.Int32);
        //        parameters.Add("@IdIndicadorIGVFactura", proveedor.IdIndicadorIGVFactura, dbType: DbType.Int32);
        //        parameters.Add("@IdIndicadorIGVProvision", proveedor.IdIndicadorIGVProvision, dbType: DbType.Int32);

        //        var result = await cn.ExecuteAsync("dbo.usp_Proveedor_Insert", parameters, commandType: CommandType.StoredProcedure);

        //        return result;
        //    }
        //}

        //public async Task<int> Modificar(Data.Common.ProveedorTaxi proveedor)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@ID", proveedor.ID, dbType: DbType.Int32);
        //        parameters.Add("@RazonSocial", proveedor.RazonSocial, dbType: DbType.String);
        //        parameters.Add("@RUC", proveedor.RUC, dbType: DbType.String);
        //        parameters.Add("@TiempoReserva", proveedor.TiempoReserva, dbType: DbType.Double);
        //        parameters.Add("@TiempoEspera", proveedor.TiempoEspera, dbType: DbType.Double);
        //        parameters.Add("@Telefono", proveedor.Telefono, dbType: DbType.Int32);
        //        parameters.Add("@Direccion", proveedor.Direccion, dbType: DbType.String);
        //        parameters.Add("@CodigoSAP", proveedor.CodigoSAP, dbType: DbType.String);
        //        parameters.Add("@Contacto", proveedor.Contacto, dbType: DbType.String);
        //        parameters.Add("@Email", proveedor.Email, dbType: DbType.String);
        //        parameters.Add("@IdEstado", proveedor.IdEstado, dbType: DbType.Int32);
        //        parameters.Add("@IdIndicadorIGVFactura", proveedor.IdIndicadorIGVFactura, dbType: DbType.Int32);
        //        parameters.Add("@IdIndicadorIGVProvision", proveedor.IdIndicadorIGVProvision, dbType: DbType.Int32);

        //        var result = await cn.ExecuteAsync("dbo.usp_Proveedor_Update", parameters, commandType: CommandType.StoredProcedure);
        //        return result;
        //    }
        //}

        //public async Task<bool> ValidarProveedor(string ruc , int id)
        //{
        //    bool result = true;
        //    using (var context = new Entities())
        //    {
        //        var proveedor = await context.ProveedorTaxi.AsNoTracking().Where(m => m.ID != id && m.RUC == ruc).FirstOrDefaultAsync();
        //        if (proveedor != null) result = false;
        //    }
        //    return result;
        //}

        //public async Task<OperationResult> AddCredencial(Data.CredencialesProveedorTaxi credencial)
        //{
        //    using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //    {
        //        try
        //        {
        //            using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //            {
        //                var parameters = new DynamicParameters();
        //                parameters.Add("@IdProveedor", credencial.IdProveedor, DbType.Int32);
        //                parameters.Add("@UserName", credencial.UserName, DbType.String);
        //                parameters.Add("@Contraseña", credencial.Contraseña, DbType.String);
        //                parameters.Add("@GrantType", credencial.GrantType, DbType.String);
        //                parameters.Add("@KeyApi", credencial.KeyApi, DbType.String);


        //                await cn.ExecuteAsync("dbo.usp_CredencialesProveedorTaxi_Insert", parameters, commandType: CommandType.StoredProcedure);
        //                ts.Complete();
        //                return new OperationResult() { Success = true };
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            ts.Dispose();
        //            return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
        //        }
        //    }
        //}

        //public async Task<OperationResult> ModificarCredencial(Data.CredencialesProveedorTaxi credencial)
        //{
        //    using (var context = new Entities())
        //    {
        //        using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //        {
        //            try
        //            {
        //                using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //                {
        //                    var parameters = new DynamicParameters();
        //                    parameters.Add("@ID", credencial.ID, DbType.Int32);
        //                    parameters.Add("@IdProveedor", credencial.IdProveedor, DbType.Int32);
        //                    parameters.Add("@UserName", credencial.UserName, DbType.String);
        //                    parameters.Add("@Contraseña", credencial.Contraseña, DbType.String);
        //                    parameters.Add("@GrantType", credencial.GrantType, DbType.String);
        //                    parameters.Add("@KeyApi", credencial.KeyApi, DbType.String);

        //                    await cn.ExecuteAsync("dbo.usp_CredencialesProveedorTaxi_Update", parameters, commandType: CommandType.StoredProcedure);
        //                    ts.Complete();
        //                    return new OperationResult() { Success = true };
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                ts.Dispose();
        //                return new OperationResult() { Success = false, Errors = new List<string>() { ex.Message } };
        //            }
        //        }
        //    }
        //}
        //public OperationResult AddMethodService(List<usp_ServicioProveedorTaxi_List_Result> listaMethod , int IdProveedor)
        //{

        //    using (var context = new Entities())
        //    {
        //        var servicioXML = new ServicioProveedorTaxiTransaccionXML();
        //        var ListaMethod = ObtenerDatosServicioProveedorTaxi(listaMethod);

        //        servicioXML.ServicioProveedorTaxiXML = ConvertFormat.ObjectToXml(ListaMethod);

        //        using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //        {
        //            try
        //            {
        //                var result = context.usp_ServicioProveedorTaxi_Insert(servicioXML.ServicioProveedorTaxiXML.DocumentElement.InnerXml, IdProveedor);
        //                ts.Complete();
        //                return new OperationResult() { Success =  true };
        //            }
        //            catch (Exception ex )
        //            {
        //                ts.Dispose();
        //                return new OperationResult() { Success = false , Errors = new List<string>() { ex.Message} };
        //            }
        //        }
        //    }
        //}

        //private ServicioProveedorTaxiTransaccion ObtenerDatosServicioProveedorTaxi(List<usp_ServicioProveedorTaxi_List_Result> listaMethod)
        //{
        //    var modelo = new ServicioProveedorTaxiTransaccion
        //    {
        //        ListaArchivoPlanoServicioProveedorTaxi = new List<ArchivoPlanoServicioProveedorTaxi>()
        //    };
        //    foreach (var detalle in listaMethod)
        //    {
        //        var method = new ArchivoPlanoServicioProveedorTaxi
        //        {
        //            IdMetdoProveedorTaxi = detalle.IdMetodoProveedorTaxi.Value,
        //            IdProveedor = detalle.IdProveedor.Value,
        //            URL = detalle.URL,
        //            Metodo = detalle.Metodo
        //        };

        //        modelo.ListaArchivoPlanoServicioProveedorTaxi.Add(method);
        //    }
        //    return modelo;
        //}

        //public async Task<CredencialesProveedorTaxi> GetCredencialesById(int id)
        //{
        //    var credenciales = new CredencialesProveedorTaxi();
        //    using (var context = new Entities())
        //    {
        //        credenciales = await context.Set<CredencialesProveedorTaxi>().Where(m => m.IdProveedor == id).FirstOrDefaultAsync();
        //    }
        //    return credenciales;
        //}

        //public async Task<ProveedorTaxi> GetById(int id)
        //{
        //    using (var context = new Entities())
        //    {
        //        return await context.Set<ProveedorTaxi>().Where(m => m.ID == id).FirstOrDefaultAsync();
        //    }
        //}

        public async Task<Data.Common.ProveedorTaxi> GetProveedorById(int id)
        {
            using (var cn = new SqlConnection(BaseConexion.ConexionTaxi))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ID", id, dbType: DbType.Int32);

                var result = await cn.QueryFirstOrDefaultAsync<Data.Common.ProveedorTaxi>("dbo.usp_Proveedor_GetByID", parameters, commandType: CommandType.StoredProcedure);

                return result ?? new Data.Common.ProveedorTaxi();
            }
        }

        //public List<Data.Common.ProveedorTaxi> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros)
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@PageSize", parametros.Collection["Row"], DbType.Int32);
        //        parameters.Add("@PageIndex", parametros.Collection["Page"], DbType.Int32);
        //        parameters.Add("@Ruc", parametros.Collection["Ruc"], DbType.String);
        //        parameters.Add("@RazonSocial", parametros.Collection["RazonSocial"], DbType.String);
        //        parameters.Add("@CodigoSap", parametros.Collection["CodigoSap"], DbType.String);
        //        parameters.Add("@TotalRows", 1, DbType.Int32, ParameterDirection.InputOutput);
        //        parameters.Add("@CantidadRegistros", 1, DbType.Int32, ParameterDirection.InputOutput);

        //        var lista = cn.Query<Data.Common.ProveedorTaxi>("dbo.usp_Proveedor_Listar_Paginado", parameters, commandType: CommandType.StoredProcedure);
        //        var result = lista.ToList();
        //        totalRows = parameters.Get<int>("@TotalRows");
        //        cantidadRegistros = parameters.Get<int>("@CantidadRegistros");
        //        return result;
        //    }
        //}
        //public IEnumerable<ProveedorTaxi> Listar()
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var result =  cn.Query<ProveedorTaxi>("SELECT * FROM dbo.ProveedorTaxi");

        //        return result;
        //    }
        //}

        //public async Task<string> Key()
        //{
        //    using (var cn = new SqlConnection(BaseConexcion.ConexionTaxi))
        //    {
        //        var result = (await cn.ExecuteScalarAsync("dbo.usp_GoogleMapsKey_Select", commandType: CommandType.StoredProcedure)) ?? string.Empty;

        //        return result.ToString();
        //    }
        //}
    }
}
