using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegratedJobPortal.Models
{
    public class PortalAccount
    {
        public int UserId { get; set; }
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteaccStatus { get; set; }
        public string AccountId { get; set; }
        public string UserName { get; set; }
        public string GroupName { get; set; }
        public string TotalHours { get; set; }
        public string MaxUser { get; set; }
        public string Portalusesterms { get; set; }
        public string LoginTime { get; set; }
        public string TimeFormat { get; set; }
        public int TimeLeft { get; set; }
        public int TimeAllotted { get; set; }
        public string IPAddress { get; set; }
        public string Status { get; set; }
        public int IsValid { get; set; }
        public DateTime ActivateTime { get; set; }
        public string ResumeViewsLimit { get; set; }
        public int DefaultTime { get; set; }
        public int GroupId { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string LoginURL { get; set; }
        public string LogOutURL { get; set; }
        public string RedirectURL { get; set; }

        public int IsValidIP { get; set; }
        public string IPMessage { get; set; }

        public int IsAvailable { get; set; }
        public string OccupiedMessage { get; set; }
    }
}