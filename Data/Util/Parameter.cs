using System.Collections.Generic;

namespace Data.Util
{
    public class Parameter
    {
        public Parameter()
        {
            Collection = new Dictionary<string, object>();
        }
        public Dictionary<string, object> Collection { get; set; }
    }
}
