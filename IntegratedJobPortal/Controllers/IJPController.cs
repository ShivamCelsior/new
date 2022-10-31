using IntegratedJobPortal.Common;
using IntegratedJobPortal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace IntegratedJobPortal.Controllers
{
    [RoutePrefix("ijp")]
    public class IJPController : ApiController
    {
        [Route("UserLogin")]
        [HttpPost]
        public UserDetails UserLogin(UserCredential credential)
        {
            try
            {
              var IPAddress =   HttpContext.Current.Request.UserHostAddress.ToString();
                DBLib objDBLiv = new DBLib();
                //return objDBLiv.ValidateUser("sukhmanis", "Pyramid#@1");
                return objDBLiv.ValidateUser(credential.Username, credential.Password);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "UserLogin", ex.StackTrace);
                throw;
            }
        }

        [Route("getConinfo/{UserId}/{SiteName}")]
        [HttpGet]
        public List<PortalAccount> GetConinfo(string UserId, string SiteName )
        {
            try
            {
                DBLib objDBLiv = new DBLib();
                return objDBLiv.GetConinfo(Convert.ToInt32(EncrDecr.Decrypt(UserId)), SiteName);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "GetConinfo", ex.StackTrace);
                throw;
            }
        }
        [Route("getActiveConnectionsForUser/{UserId}")]
        [HttpGet]
        public List<PortalAccount> GetActiveConnectionsForUser(string UserId)
        {
            try
            {
                DBLib objDBLiv = new DBLib();
                return objDBLiv.GetActiveConnectionsForSingleUser(Convert.ToInt32(EncrDecr.Decrypt(UserId)));

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "getActiveConnectionsForUser", ex.StackTrace);
                throw;
            }
        }

        [Route("getSiteCredentials/{SiteId}")]
        [HttpGet]
        public PortalAccount GetSiteCredentials(int SiteId)
        {
            try
            {
                DBLib objDBLiv = new DBLib();
                return objDBLiv.GetUserIDandPassword(SiteId);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "GetSiteCredentials", ex.StackTrace);
                throw;
            }
        }
        [Route("getPortalCredentials")]
        [HttpPost]
        public PortalAccount GetPortalCredentials(SiteInfo Info)
        {
            try
            {
                DBLib objDBLiv = new DBLib();
                return objDBLiv.GetPortalCredentials(Convert.ToInt32(EncrDecr.Decrypt(Info.UserId)), Info.SiteId, Info.GroupId, Info.AllocatedTime, Info.IPAddress);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "getPortalCredentials", ex.StackTrace);
                throw;
            }
        }
        [Route("saveUserSiteInfo")]
        [HttpPost]
        public int SaveUserSiteInfo(SiteInfo Info)
        {
            try
            {
                var IPAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                DBLib objDBLiv = new DBLib();
                return objDBLiv.SaveUserSiteInfo(Convert.ToInt32(EncrDecr.Decrypt(Info.UserId)), Info.GroupId, Info.SiteId, Info.AllocatedTime, "");

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "SaveUserSiteInfo", ex.StackTrace);
                throw;
            }
        }

        [Route("updateUserSiteInfo")]
        [HttpPost]
        public int UpdateUserSiteInfo(SiteInfo Info)
        {
            try
            {
                DBLib objDBLiv = new DBLib();
                return objDBLiv.UpdateUserSiteInfo(Convert.ToInt32(EncrDecr.Decrypt(Info.UserId)), Info.GroupId, Info.SiteId, Info.Status);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "UpdateUserSiteInfo", ex.StackTrace);
                throw;
            }
        }

        [Route("getSiteNames/{UserId}")]
        [HttpGet]
        public List<string> GetSiteNames(string UserId)
        {
            try
            {
                DBLib objDBLiv = new DBLib();
                return objDBLiv.GetSiteNames(Convert.ToInt32(EncrDecr.Decrypt(UserId)));

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "GetSiteNames", ex.StackTrace);
                throw;
            }
        }

        [Route("freeActiveConnection/{UserId}")]
        [HttpGet]
        public int FreeActiveConnection(string UserId)
        {
            try
            {
                DBLib objDBLiv = new DBLib();
                return objDBLiv.FreeActiveConnections(Convert.ToInt32(EncrDecr.Decrypt(UserId)));

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "FreeActiveConnection", ex.StackTrace);
                throw;
            }
        }

        [Route("validateIPAddress")]
        [HttpPost]
        public IPStatus ValidateIPAddress(SiteInfo Info)
        {
            try
            {
                DBLib objDBLiv = new DBLib();
                return objDBLiv.ValidateIPAddress(Info.IPAddress);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "ValidateIPAddress", ex.StackTrace);
                throw;
            }
        }

        [Route("forgotPassword")]
        [HttpPost]
        public KeyValuePair ForgotPassword(UserInfo Info)
        {
            try
            {
                Utility objDBLiv = new Utility();
                return objDBLiv.ForgotPassword(Info.LoginId);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "forgotPassword", ex.StackTrace);
                throw;
            }
        }
        [Route("resetPassword")]
        [HttpPost]
        public KeyValuePair ResetPassword(UserInfo Info)
        {
            try
            {
                Utility objDBLiv = new Utility();
                return objDBLiv.ResetPassword(Info.LoginId, Info.SecurityCode, Info.Password);


            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "ResetPassword", ex.StackTrace);
                throw;
            }
        }

        [Route("getIJPPortalURL")]
        [HttpPost]
        public string GetIJPPortalURL(SiteInfo Info)
        {
            try
            {
                DBLib objDBLiv = new DBLib();
                return objDBLiv.GetIJPPortalURL(Info.IPAddress);

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "GetIJPPortalURL", ex.StackTrace);
                throw;
            }
        }
        [Route("Track/{UserId}/{groupId}/{siteId}")]
        [HttpGet]
        public string Track(string UserId, int groupId, int siteId)
        {
            //Logger.Log.Info("Tracing", string.Format("Track({0}, {1}, {2})", userId, groupId, siteId));
            string message = "Succcess";
           
            try
            {
                bool flag = true;
                int userId = Convert.ToInt32(EncrDecr.Decrypt(UserId));
                DBLib objDBLiv = new DBLib();
                if (userId <= 0)
                {
                    message = "INVALID USER ID RECIEVED";
                    flag = false;
                    ErrorLog.WriteErrorLog(message, string.Format("Track({0}, {1}, {2})", userId, groupId, siteId));
                }

                if (groupId <= 0)
                {
                    message = "INVALID GROUP ID RECIEVED";
                    flag = false;

                    ErrorLog.WriteErrorLog(message, string.Format("Track({0}, {1}, {2})", userId, groupId, siteId));
                }

                if (siteId <= 0)
                {
                    message = "INVALID SITE ID RECIEVED";
                    flag = false;

                    ErrorLog.WriteErrorLog(message, string.Format("Track({0}, {1}, {2})", userId, groupId, siteId));
                }

                if (flag)
                    objDBLiv.Deduct(userId, groupId, siteId);
            }
            catch (Exception ex)
            {
                message = "AN ERROR OCCURRED";
                ErrorLog.WriteErrorLog(ex.Message, "Track", stackTrace: ex.StackTrace);
            }

            return message;
        }

        [Route("isUserActive/{UserId}")]
        [HttpGet]
        public int IsUserActive(string UserId)
        {
            int IsActive = 0;

            try
            {
                int userId = Convert.ToInt32(EncrDecr.Decrypt(UserId));
                DBLib objDBLiv = new DBLib();

                IsActive= objDBLiv.IsUserActive(userId);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "IsUserActive", ex.StackTrace);
            }

            return IsActive;
        }

        [Route("sendexceptionalert")]
        [HttpPost]
        public int SendExceptionAlert(ExtensionExceptionDetails Info)
        {
            try
            {
                string Exception;
                byte[] data = System.Convert.FromBase64String(Info.Exception);
                Exception = System.Text.ASCIIEncoding.ASCII.GetString(data);

                ErrorLog.WriteExtensionErrorLog(EncrDecr.Decrypt(Info.UserId), Info.UserName, Exception, Info.Source, Info.Version, Info.PageURL);
                //return 1;
            }
            catch (Exception ex)
            {
                //ErrorLog.WriteErrorLog(ex.Message, "IJPController", "SendExceptionAlert", ex.StackTrace);
               // throw;
            }
            return 1;
        }
        [Route("isLinkVerified/{LoginId}")]
        [HttpGet]
        public int IsLinkVerified(string LoginId)
        {
            int IsVerified = 0;

            try
            {
                DBLib objDBLiv = new DBLib();
                string emailId = LoginId.Replace('_', '.');
                IsVerified = objDBLiv.IsLinkVerified(emailId);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "IsLinkVerified", ex.StackTrace);
            }

            return IsVerified;
        }


        [Route("getportal")]
        [HttpGet]
        public async Task<ApiResponse> GetPortal(Int32 PortalId)
        {
            try
            {
                DBLib db = new DBLib();
                Portal portal = await db.GetPortal(PortalId);
                
                return (new ApiResponse() { StatusCode = CustomStatusCode.Success, Message = "Portal Fetched.", Data = portal });
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "GetPortal", ex.StackTrace);
                return (new ApiResponse() { StatusCode = CustomStatusCode.Error, Message = "Some Internal Error Occur.", Data = { } });
            }
        }

        [Route("getportals")]
        [HttpGet]
        public async Task<ApiResponse> GetPortals()
        {
            try
            {
                DBLib db = new DBLib();
                List<Portal> portals = await db.GetPortals();

                return (new ApiResponse() { StatusCode = CustomStatusCode.Success, Message = "Portal Fetched.", Data = portals });
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "GetPortals", ex.StackTrace);
                return (new ApiResponse() { StatusCode = CustomStatusCode.Error, Message = "Some Internal Error Occur.", Data = { } });
            }
        }

        [Route("addportal")]
        [HttpPost]
        public async Task<ApiResponse> AddPortal(PortalEntity portal)
        {
            try
            {
                DBLib db = new DBLib();

                Boolean result = await db.AddOrUpdatePortal(portal);


                if (result)
                    return (new ApiResponse() { StatusCode = CustomStatusCode.Success, Message = "Portal Added", Data = { } });
                else
                    return (new ApiResponse() { StatusCode = CustomStatusCode.Error, Message = "Portal Not Added", Data = { } });
                
                
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "AddPortal", ex.StackTrace);
                return (new ApiResponse() { StatusCode = CustomStatusCode.Error, Message = "Some Internal Error Occur.", Data = { } });
            }
        }

        [Route("updateportal")]
        [HttpPost]
        public async Task<ApiResponse> UpdatePortal(PortalEntity portal)
        {
            try
            {
                DBLib db = new DBLib();

                Boolean result = await db.AddOrUpdatePortal(portal);
                
                if (result)
                    return (new ApiResponse() { StatusCode = CustomStatusCode.Success, Message = "Portal Updated", Data = { } });
                else
                    return (new ApiResponse() { StatusCode = CustomStatusCode.Error, Message = "Portal Not Updated", Data = { } });

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "UpdatePortal", ex.StackTrace);
                return (new ApiResponse() { StatusCode = CustomStatusCode.Error, Message = "Some Internal Error Occur.", Data = { } });
            }
        }

        [Route("getjobportal")]
        [HttpGet]
        public async Task<ApiResponse> GetJobPortal(Int32 PortalId)
        {
            try
            {
                DBLib db = new DBLib();
                JobPortal portal = await db.GetJobPortal(PortalId);

                return (new ApiResponse() { StatusCode = CustomStatusCode.Success, Message = "Job Portal Fetched.", Data = portal });
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "GetJobPortal", ex.StackTrace);
                return (new ApiResponse() { StatusCode = CustomStatusCode.Error, Message = "Some Internal Error Occur.", Data = { } });
            }
        }

        [Route("getjobportals")]
        [HttpGet]
        public async Task<ApiResponse> GetJobPortals()
        {
            try
            {
                DBLib db = new DBLib();
                List<JobPortal> portals = await db.GetJobPortals();

                return (new ApiResponse() { StatusCode = CustomStatusCode.Success, Message = "Job Portal Fetched.", Data = portals });
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "GetJobPortals", ex.StackTrace);
                return (new ApiResponse() { StatusCode = CustomStatusCode.Error, Message = "Some Internal Error Occur.", Data = { } });
            }
        }

        [Route("addjobportal")]
        [HttpPost]
        public async Task<ApiResponse> AddJobPortal(JobPortalEntity portal)
        {
            try
            {
                DBLib db = new DBLib();

                Boolean result = await db.AddOrUpdateJobPortal(portal);


                if (result)
                    return (new ApiResponse() { StatusCode = CustomStatusCode.Success, Message = "Job Portal Added", Data = { } });
                else
                    return (new ApiResponse() { StatusCode = CustomStatusCode.Error, Message = "Job Portal Not Added", Data = { } });


            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "AddJobPortal", ex.StackTrace);
                return (new ApiResponse() { StatusCode = CustomStatusCode.Error, Message = "Some Internal Error Occur.", Data = { } });
            }
        }

        [Route("updatejobportal")]
        [HttpPost]
        public async Task<ApiResponse> UpdateJobPortal(JobPortalEntity portal)
        {
            try
            {
                DBLib db = new DBLib();

                Boolean result = await db.AddOrUpdateJobPortal(portal);

                if (result)
                    return (new ApiResponse() { StatusCode = CustomStatusCode.Success, Message = "Job Portal Updated", Data = { } });
                else
                    return (new ApiResponse() { StatusCode = CustomStatusCode.Error, Message = "Job Portal Not Updated", Data = { } });

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "IJPController", "UpdateJobPortal", ex.StackTrace);
                return (new ApiResponse() { StatusCode = CustomStatusCode.Error, Message = "Some Internal Error Occur.", Data = { } });
            }
        }

    }
}
