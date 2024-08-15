using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Service.Services
{
    public class CategoriaProductoService : ICategoriaProductoService
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoriaProducto> Get(Func<CategoriaProducto, bool> filter = null)
        {
            using (var context = new Entities())
            {
                if (filter != null)
                {
                    return context.CategoriaProducto.Where(filter).ToList();
                }
                else
                {
                    return context.CategoriaProducto.ToList();
                }
            }
        }
    }
}
