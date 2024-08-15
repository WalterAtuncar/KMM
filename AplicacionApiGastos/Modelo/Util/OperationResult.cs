using System;
using System.Collections.Generic;

namespace Modelo.Util
{
    public class OperationResult
    {
        public int NewID { get; set; }
        public int NewIDSon { get; set; }
        public int RowAffected { get; set; }
        public bool Success { get; set; }
        public Exception Exception { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public object ObjectResult { get; set; }

        //integracion
        public List<string> Errors { get; set; }
        public int RowAffeted { get; set; }
        public Exception ExceptionError { get; set; }
        public decimal Valor { get; set; }
    }
}
