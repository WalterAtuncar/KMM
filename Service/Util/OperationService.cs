﻿using System;
using System.Collections.Generic;

namespace Service.Util
{
    public class OperationService
    {
        public bool Success { get; set; }
        public object ResponseObject { get; set; }
        public string Message { get; set; }
        public Exception ExceptionError { get; set; }
    }
}
