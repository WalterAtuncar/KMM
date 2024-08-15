using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class LiquidarModel : IValidatableObject
    {
        public LiquidarModel()
        {

        }
        [Required]
        public int ID { get; set; }

        public string Factura { get; set; }

        public string FechaFactura { get; set; }

        [Required]
        public int UsuarioID { get; set; }

        [Required]
        public string FacturaProvision { get; set; }


        public List<FacturaProvision> Radios = new List<FacturaProvision>() { new FacturaProvision() { ID = "01", Text = "Factura" }, new FacturaProvision() { ID = "SP", Text = "Provisión" } };

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FacturaProvision == "F")
            {
                if (string.IsNullOrWhiteSpace(Factura) || string.IsNullOrWhiteSpace(FechaFactura))
                {
                    yield return new ValidationResult("Complete los campos obligatorios");
                }
            }
        }
    }

    public class FacturaProvision
    {
        public string ID { get; set; }
        public string Text { get; set; }
    }
}