using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Service.Contracts;

namespace Service.Services
{
    public class SituacionServicioService : ISituacionServicioService
    {
        public IEnumerable<SituacionServicio> Get()
        {
            using (var context = new Entities())
            {
                return context.SituacionServicio.ToList();
            }
        }
    }
}
