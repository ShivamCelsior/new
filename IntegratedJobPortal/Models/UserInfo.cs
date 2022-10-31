using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegratedJobPortal.Models
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public int UserNameId { get; set; }
        public string SecurityCode { get; set; }
       
        public string UserName { get; set; }
        public string LoginId { get; set; }

        public string Password { get; set; }
    }
}