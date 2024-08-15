namespace GASTO.Domain
{
    public class Estados
    {
        public int IdEstado { get; set; }
        public int IdTipo { get; set; }
        public string Estado { get; set; }
        public int Orden { get; set; }
        public bool Activo { get; set; }
        public int IdEstadoSiguiente { get; set; }
    }
}
