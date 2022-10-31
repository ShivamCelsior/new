using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegratedJobPortal.Models
{
    public class UserDetails
    {
        public string UserName { get; set; }

        public string Roles { get; set; }

        public string AuthToken { get; set; }

        public int IdleTimeoutMins { get; set; }

        public string LoginType { get; set; }
        public string UserId { get; set; }
        public int IsSuperUser { get; set; }
        public string GroupId { get; set; }
        public string Email { get; set; }
        public string CrintellPassword { get; set; }

        public int PCoreUserId { get; set; }
    }
}