using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegratedJobPortal.Models
{
    public class IPStatus
    {
        public int IsValid { get; set; }
        public string IPMessage { get; set; }
        public string IPAddress { get; set; }
    }
}