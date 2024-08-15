using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CancelarLiquidacionModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int UsuarioID { get; set; }
    }
}