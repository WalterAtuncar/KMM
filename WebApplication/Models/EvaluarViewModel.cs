using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class EvaluarViewModel
    {
        public List<Data.Common.SolicitudProveedorTaxi> ListaSolicitudProveedorTaxi { get; set; }
        public Data.Common.Solicitud Solicitud { get; set; }
        public string ListaUser { get; set; }
    }
}