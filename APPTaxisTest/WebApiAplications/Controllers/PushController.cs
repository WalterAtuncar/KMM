using Common.Enum;
using Modelo.General;
using Modelo.Request;
using Servicio;
using Servicio.Contract;
using Servicio.Logic;
using Servicio.Logic.Contract;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiAplications.Controllers
{
    //[Authorize]
    [RoutePrefix("api/push")]
    public class PushController : ApiController
    {
        private readonly IServicioLogic oIServicioLogic;
        private readonly IServicioData oIServicioData;
        private readonly IProveedorData oIProveedorData;
        public PushController()
        {
            if (oIServicioLogic == null) { oIServicioLogic = new ServicioLogic(new ServicioData(), new ProveedorData()); }
            if(oIServicioData == null) { oIServicioData = new ServicioData(); }
            if (oIProveedorData == null) { oIProveedorData = new ProveedorData(); }
        }
        public PushController(IServicioLogic oIServicioLogic, IServicioData oIServicioData, IProveedorData oIProveedorData)
        {
            this.oIServicioLogic = oIServicioLogic;
            this.oIServicioData = oIServicioData;
            this.oIProveedorData = oIProveedorData;
        }

        [Route("servicePush")]
        [HttpPost]
        public async Task<IHttpActionResult> PushService([FromBody]PushRequest modelo)
        {
            var opertation = await oIServicioLogic.PushServie(modelo);

            if (opertation.Success)
            {
                if(modelo.IdEstado == (int)EstadoProveedorEnum.ServicioFinalizado)
                {
                    /*Correo Asíncrono*/
                    var correo = new Correo();
                    
                    correo = await oIServicioData.GetSolicitudCorreoFinalizado(modelo.IdServicio);
                    correo.Responsable = new Usuario();
                    correo.Mensaje = ConfigurationManager.AppSettings["mensajeFinalizado"].Replace("{0}", correo.Codigo);
                    correo.ListaEmailAdjunto = await oIServicioData.GetListEmailBySolicitud(null, modelo.IdServicio, (int)TipoCorreoEnum.Finalizado);
                    correo.Conductor = modelo.Conductor;
                    correo.Automovil = modelo.AutoMovil;
                    correo.ListaDestino = modelo.ListaDestino;
                    correo.ListaGastoAdicional = modelo.ListaGastoAdicional;
                    correo.ListaBeneficiado = await oIServicioData.GetListBeneficiadoByServicioProveedor(modelo.IdServicio);

                    var listaProveedor = await oIProveedorData.GetListProveedor();
                    Proveedor proveedor = listaProveedor.Where(x => x.RUC == modelo.RUCProveedor).FirstOrDefault();
                    correo.RazonSocialProveedor = proveedor.RazonSocial;
                    correo.RucProveedor = proveedor.RUC;

                    ServicioController oServicioController = new ServicioController();
                    var result = await oServicioController.EmailService(correo);
                }

                return Ok(opertation.Message);
            }
            else
            {
                return BadRequest(message: opertation.Message);
            }
        }

    }
}
