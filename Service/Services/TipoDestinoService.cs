using Data;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TipoDestinoService: ITipoDestinoService
    {
        public List<Data.TipoDestino> List()
        {
            using (var context = new Entities())
            {
                return context.TipoDestino.ToList();
            }
        }
    }
}
