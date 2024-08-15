using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common
{
    public class Adjunto
    {
        public int IdAdjunto { get; set; }

        public string PathAdjunto { get; set; }

        public string NombreAdjunto { get; set; }

        public byte[] Data { get; set; }

        public string Alias { get; set; }

        public int Index { get; set; }

        public int IdProyecto { get; set; }

        public int IdEntrega { get; set; }

        public bool ExistEdit { get; set; }
    }
}
