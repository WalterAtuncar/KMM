using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.General
{
    public class ComparerTracking: IEqualityComparer<Tracking>
    {
        public bool Equals(Tracking x, Tracking y)
        {
            return x.Latitud == y.Latitud && x.Longitud == y.Longitud;
        }

        public int GetHashCode(Tracking obj)
        {
            return obj.FechaTracking.GetHashCode();
        }
    }
}
