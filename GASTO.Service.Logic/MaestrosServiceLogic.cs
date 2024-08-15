using GASTO.Domain;
using GASTO.Domain.Util;
using GASTO.Service.Contracts;
using GASTO.Service.Logic.Contract;
using GASTO.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GASTO.Service.Logic
{
    public class MaestrosServiceLogic : IMaestrosServiceLogic
    {
        private readonly IMaestrosService oIMaestrosService;
        public MaestrosServiceLogic()
        {
            this.oIMaestrosService = new MaestrosService();
        }

        public List<Maestros> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros)
        {
            var result = oIMaestrosService.ListarPaginado(parametros, out totalRows, out cantidadRegistros);
            return result;
        }
    }
}
