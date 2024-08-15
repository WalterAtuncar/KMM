using System;

namespace Service.Entidad
{
    public class Solicitud
    {
        public int IdSolicitud { get; set; }
        public string Codigo { get; set; }
        public int IdTipo { get; set; }
        public string AsientoContable { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public string IdSociedad { get; set; }
        public int IdSituacionServicio { get; set; }
        public string AnioEjercicio { get; set; }
        public string CodigoPago { get; set; }

        //NEY TANGOA 16/07/2020
        public Nullable<System.DateTime> FechaPago { get; set; }

    }
}
