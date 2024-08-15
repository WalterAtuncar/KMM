using System;
using System.Collections.Generic;

namespace Service
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public int NewID { get; set; }
        public int NewIDSon { get; set; }
        public int RowAffeted { get; set; }
        public Exception ExceptionError { get; set; }
        public string Message { get; set; }
        public object ObjectResult { get; set; }
        public decimal Valor { get; set; }
    }
}
