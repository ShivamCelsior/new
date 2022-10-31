using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegratedJobPortal.Entity
{
    public class PcoreUserDetails
    {
        public int empid { get; set; }
        public string empssn { get; set; }
        public string emailid { get; set; }
        public int empstatus { get; set; }
        public string empname { get; set; }
        public int businesspractice { get; set; }
        public int department { get; set; }
        public string username { get; set; }
        public string passwd { get; set; }
        public bool IsSupervisor { get; set; }
        public bool IsPMO { get; set; }
        public string PMODepartments { get; set; }
    }
}