using Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class PerfilViewModel
    {
        public Data.Common.Perfil Perfil { get; set; }
        public List<EstadoServicio> ListaEstadoProveedor { get; set; }
    }
}