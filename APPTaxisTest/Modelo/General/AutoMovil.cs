namespace Modelo.General
{
    public class AutoMovil
    {
        public int ID { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Color { get; set; }
        public string Modelo { get; set; }
        public byte[] Foto { get; set; }
        public int CantidadPasajeros { get; set; }
    }
}
