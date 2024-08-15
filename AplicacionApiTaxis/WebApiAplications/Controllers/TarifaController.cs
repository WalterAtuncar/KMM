using Modelo.Request;
using Servicio;
using Servicio.Logic;
using Servicio.Logic.Contract;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiAplications.Controllers
{
    //[Authorize]
    [RoutePrefix("api/tarifa")]
    public class TarifaController : ApiController
    {
        private readonly ITarifaLogic oITarifaLogic;
       
        public TarifaController(ITarifaLogic oITarifaLogic)
        {
            this.oITarifaLogic = oITarifaLogic;
        }
        public TarifaController()
        {
            if (oITarifaLogic == null) { oITarifaLogic = new TarifaLogic(new TarifaData(),new ProveedorData(),new ServicioData()); }
        }

       
        [Route("getListTarifa")]
        [HttpPost]
        public async Task<IHttpActionResult> GetListTarifa([FromBody]TarifaRequest modelo)
        {
            var lista = await oITarifaLogic.GetListTarifa(modelo);

            if (lista.Count == default(int))
            {
                return BadRequest(message : "No se encontro nigun servicio disponible");
            }
            else
            {
                return Ok(lista);
            }            
        }
    }   
}
