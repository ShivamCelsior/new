using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegratedJobPortal.Models
{
    public class SiteInfo
    {
        public string UserId { get; set; }
        public int SiteId { get; set; }
        public int GroupId { get; set; }
        public int AllocatedTime { get; set; }
        public string IPAddress { get; set; }
        public int Status { get; set; }
    }
}