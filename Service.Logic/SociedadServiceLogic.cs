using Data;
using Data.Common;
using Service.Contracts;
using Service.Logic.Contract;
using System.Collections.Generic;

namespace Service.Logic
{
    public class SociedadServiceLogic : ISociedadServiceLogic
    {
        private readonly ISociedadService oISociedadService;
        public SociedadServiceLogic(ISociedadService oISociedadService)
        {
            this.oISociedadService = oISociedadService;
        }

        IEnumerable<Data.Common.Sociedad> ISociedadServiceLogic.Get()
        {
            return oISociedadService.Get();
        }
    }
}
