namespace Data.Common
{
    public class IndicadorIGV
    {
        public int ID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string NombreCompleto { get { return $"{this.Codigo}-{this.Nombre}"; } }
    }
}
