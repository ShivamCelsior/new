using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegratedJobPortal.Models
{
    public class ExtensionExceptionDetails
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Exception { get; set; }
        public string Source { get; set; }
        public string Version { get; set; }
        public string PageURL { get; set; }
    }
}