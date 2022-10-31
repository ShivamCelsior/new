using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegratedJobPortal.Models
{
    public class ApiResponse
    {
        public CustomStatusCode StatusCode { get; set; }
        public String Message { get; set; }
        public dynamic Data { get; set; }
    }

    public enum CustomStatusCode
    {
        Success = 1,
        Error = 0
    }
}