using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    interface ICategoriaProductoService : IDisposable
    {
        IEnumerable<CategoriaProducto> Get(Func<CategoriaProducto, bool> filter = null);
    }
}
