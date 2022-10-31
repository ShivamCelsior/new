using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegratedJobPortal.Models
{
    public class Portal
    {
        public Int32 PortalId { get; set; }
        public String PortalName { get; set; }
        public String CreateBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class PortalEntity
    {
        public Int32 PortalId { get; set; }
        public String PortalName { get; set; }
        public Int32 CreateBy { get; set; }
        public Int64 UAID { get; set; }
        public String LoginURL { get; set; }
        public String LogOutURL { get; set; }
        public String RedirectURL { get; set; }
    }

    public class JobPortal
    {
        public Int32 PortalId { get; set; }
        public Int32 PortalName { get; set; }
        public String PortalAccountId { get; set; }
        public String PortalLoginId { get; set; }
        public String PortalPassword { get; set; }
        public Boolean IsFreeze { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Int32 CreatedBy { get; set; }
        public Int32 ModifiedBy { get; set; }
        public String AccountStartDate { get; set; }
        public String AccountExpiryDate { get; set; }
        public String ContactPerson { get; set; }
        public String Remarks { get; set; }
        public String PortalUsesTerms { get; set; }
        public Int32 DefaultSiteTime { get; set; }
        public Boolean IsInActiveForADay { get; set; }
        public DateTime PortalInactiveTillDate { get; set; }
        public Int32 ResumeViewsLimit { get; set; }

    }

    public class JobPortalEntity
    {
        public Int32 PortalId { get; set; }
        public String PortalAccountId { get; set; }
        public String PortalLoginId { get; set; }
        public String PortalPassword { get; set; }
        public Boolean IsFreeze { get; set; }
        public Int32 CreatedBy { get; set; }
        public Int32 ModifiedBy { get; set; }
        public String AccountStartDate { get; set; }
        public String AccountExpiryDate { get; set; }
        public String ContactPerson { get; set; }
        public String Remarks { get; set; }
        public String PortalUsesTerms { get; set; }
        public Int32 DefaultSiteTime { get; set; }
        public Boolean IsInActiveForADay { get; set; }
        public DateTime PortalInactiveTillDate { get; set; }
        public Int32 ResumeViewsLimit { get; set; }

    }
}