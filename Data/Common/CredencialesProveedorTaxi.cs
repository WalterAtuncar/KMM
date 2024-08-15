using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common
{
    [Table("dbo].[CredencialesProveedorTaxi")]
    public class CredencialesProveedorTaxi
    {
        public int ID { get; set; }
        public int IdProveedor { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string GrantType { get; set; }
        public string KeyApi { get; set; }
    }
}
