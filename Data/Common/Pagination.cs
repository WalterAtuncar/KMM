using System.Collections.Generic;

namespace Data.Common
{
    public class Pagination
    {
        public List<ParameterFiler> ListaParameterFiler { get; set; }
        public List<object> Lista { get; set; }
        public int Rows { get; set; }
        public int Page { get; set; }
        public int TotalRows { get; set; }
        public int NumberRecords { get; set; }
    }
}
