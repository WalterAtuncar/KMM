using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.General;
using Data;

namespace Service.Services
{
    public class ClienteService : IClienteService
    {
        public usp_Cliente_Select_Result Find(int id)
        {
            using (var context = new Entities())
            {
                return context.usp_Cliente_Select(id).FirstOrDefault() ;
            }
        }
    }
}
