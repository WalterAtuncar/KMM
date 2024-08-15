using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common
{
    public class UsuarioCentroCosto
    {
        public int ID { get; set; }
        public int IdUsuario { get; set; }
        public string CodigoCentroCosto { get; set; }
        public Boolean FlagAprobador { get; set; }
        public Boolean FlagResponsable { get; set; }
    }
}
