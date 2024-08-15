using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Service.Contracts
{
    interface ISituacionServicioService
    {
        IEnumerable<SituacionServicio> Get();
    }
}
