using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IMotivoCreacionSolicitudService :  IDisposable
    {
        IEnumerable<MotivoCreacionSolicitud> Get(Func<MotivoCreacionSolicitud, bool> filter = null);
        List<Data.MotivoCreacionSolicitud> List();
    }
}
