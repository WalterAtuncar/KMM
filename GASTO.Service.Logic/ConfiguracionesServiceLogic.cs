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
    public class ConfiguracionesServiceLogic : IConfiguracionesServiceLogic
    {
        public readonly IConfiguracionesService oIConfiguracionesService;
        public ConfiguracionesServiceLogic()
        {
            this.oIConfiguracionesService = new ConfiguracionesService();
        }

        public Task<int> Add(Configuraciones configuraciones)
        {
            return oIConfiguracionesService.Add(configuraciones);
        }

        public Task<int> Edit(Configuraciones configuraciones)
        {
            return oIConfiguracionesService.Edit(configuraciones);
        }

        public Task<Configuraciones> GetById(int id)
        {
            return oIConfiguracionesService.GetById(id);
        }

        public List<Configuraciones> ListarPaginado(Parameter parametros, out int totalRows, out int cantidadRegistros)
        {
            return oIConfiguracionesService.ListarPaginado(parametros, out totalRows, out cantidadRegistros);
        }

        public Task<bool> Remove(Configuraciones configuraciones)
        {
            return oIConfiguracionesService.Remove(configuraciones);
        }
    }
}
