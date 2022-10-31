using IntegratedJobPortal.Entity;
using IntegratedJobPortal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Dapper;

namespace IntegratedJobPortal.Common
{
    public sealed class DBLib
    {
        public int GetPersonalInfoByEmail(string Email)
        {
            int userId = 9999;
            try
            {
                PcoreUserDetails oUser = new PcoreUserDetails();
                string ApiRoot = ConfigurationManager.AppSettings["PCoreApi"].ToString();
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(ApiRoot);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = client.GetAsync(ApiRoot + "GetPCoreUserByEmail/param?email=" + Email).Result;

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        oUser = JsonConvert.DeserializeObject<PcoreUserDetails>(EmpResponse);

                        if (oUser != null && oUser.empstatus == 1)
                        {
                            userId = oUser.empid;
                        }


                    }
                    //returning the employee list to view  
                    // return View(model);
                }


            }
            catch (Exception ex)
            {

            }
            return userId;
        }
        public UserDetails ValidateUser(string UserName, string Password)
        {
            UserDetails objUserDetails = new Models.UserDetails();

            SqlDataReader rdr = null;
            try
            {


                string constr = SRTLib.GetDBString();
                string USR_Pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(Password.ToString().Trim(), "SHA1");
                //SqlDataAdapter adapter;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("_procIJP_FetchUsers_LoginDetail"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@USR_LoginId", UserName);
                        cmd.Parameters.AddWithValue("@USR_Pwd", USR_Pwd);
                        cmd.Connection = con;
                        con.Open();
                        //adapter = new SqlDataAdapter(cmd);
                        rdr = cmd.ExecuteReader();
                        //userId = Convert.ToInt32(cmd.ExecuteScalar());

                        //adapter.Fill(ds);
                        while (rdr.Read())
                        {

                            objUserDetails.UserName = rdr["UserFullName"].ToString();
                            objUserDetails.UserId = EncrDecr.Encrypt(rdr["UserID"].ToString());
                            objUserDetails.IsSuperUser = Convert.ToInt32(rdr["SuperUser"]);
                            objUserDetails.Email = rdr["Email"].ToString();
                            objUserDetails.CrintellPassword = rdr["CrintellPassword"].ToString();
                            objUserDetails.AuthToken = EncrDecr.ApiTokenKey;
                            objUserDetails.PCoreUserId = GetPersonalInfoByEmail(rdr["Email"].ToString());
                        }
                        con.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "ValidateUser", ex.StackTrace);
            }
            finally
            {
                //if (conn != null)
                //{
                //    conn.Close();
                //}
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
            return objUserDetails;
        }

        public List<PortalAccount> GetConinfo(int UserId, string SiteName = "All")
        {
            UserDetails objUserDetails = new Models.UserDetails();
            List<PortalAccount> PortalAccountList = new List<Models.PortalAccount>();
            try
            {


                string constr = SRTLib.GetDBString();
                SqlDataAdapter adapter;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("_procIJP_GetConnectionList"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userId", UserId);
                        cmd.Parameters.AddWithValue("@siteName", SiteName);
                        cmd.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(ds);
                        con.Close();
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                PortalAccount objPortalAccount = new Models.PortalAccount();
                                objPortalAccount.SiteId = Convert.ToInt32(row["SiteId"]);
                                objPortalAccount.SiteName = row["SiteName"].ToString();
                                objPortalAccount.SiteaccStatus = row["SiteaccStatus"].ToString();
                                objPortalAccount.AccountId = row["AccountId"].ToString();
                                objPortalAccount.UserName = row["UserName"].ToString();
                                objPortalAccount.GroupName = row["GroupName"].ToString();
                                objPortalAccount.IPAddress = row["IPAddress"].ToString();
                                objPortalAccount.TotalHours = row["TotalHours"].ToString();
                                objPortalAccount.MaxUser = row["MaxUser"].ToString();
                                objPortalAccount.Portalusesterms = row["Portalusesterms"].ToString();
                                objPortalAccount.LoginTime = row["LoginTime"].ToString();
                                objPortalAccount.TimeFormat = row["TimeFormat"].ToString();
                                objPortalAccount.TimeLeft = Convert.ToInt32(row["TimeLeft"]);
                                objPortalAccount.Status = row["Status"].ToString();
                                objPortalAccount.IsValid = Convert.ToInt32(row["IsValid"]);
                                if (row["ActivateTime"].ToString().Trim().Length > 0)
                                    objPortalAccount.ActivateTime = Convert.ToDateTime(row["ActivateTime"]);
                                objPortalAccount.ResumeViewsLimit = row["ResumeViewsLimit"].ToString();
                                objPortalAccount.DefaultTime = Convert.ToInt32(row["DefaultTime"]);
                                objPortalAccount.GroupId = Convert.ToInt32(row["GroupId"]);

                                PortalAccountList.Add(objPortalAccount);
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "GetConinfo", ex.StackTrace);
            }
            return PortalAccountList;
        }

        public List<PortalAccount> GetActiveConnectionsForSingleUser(int UserId)
        {
            UserDetails objUserDetails = new Models.UserDetails();
            List<PortalAccount> PortalAccountList = new List<Models.PortalAccount>();
            try
            {


                string constr = SRTLib.GetDBString();
                SqlDataAdapter adapter;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("_procIJP_ActiveConnectionsForSingleUser"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userId", UserId);
                        cmd.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(ds);
                        con.Close();
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                PortalAccount objPortalAccount = new Models.PortalAccount();
                                objPortalAccount.SiteId = Convert.ToInt32(row["SiteId"]);
                                objPortalAccount.GroupId = Convert.ToInt32(row["GroupId"]);
                                //objPortalAccount.UserId = Convert.ToInt32(row["UserId"]);
                                objPortalAccount.SiteName = row["SiteName"].ToString();
                                objPortalAccount.AccountId = row["AccountId"].ToString();
                                objPortalAccount.UserName = row["UserName"].ToString();
                                objPortalAccount.GroupName = row["GroupName"].ToString();
                                objPortalAccount.LoginTime = row["LoginTime"].ToString();
                                objPortalAccount.IPAddress = row["IPAddress"].ToString();
                                objPortalAccount.TimeLeft = Convert.ToInt32(row["TimeLeft"]);
                                objPortalAccount.TimeAllotted = Convert.ToInt32(row["TimeAllotted"]);
                                objPortalAccount.DefaultTime = Convert.ToInt32(row["DefaultTime"]);
                                objPortalAccount.LogOutURL = row["LogOutURL"].ToString();
                                PortalAccountList.Add(objPortalAccount);
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "GetActiveConnectionsForSingleUser", ex.StackTrace);
            }
            return PortalAccountList;
        }
        public PortalAccount GetUserIDandPassword(int SiteId)
        {
            PortalAccount objPortalAccount = new Models.PortalAccount();
            try
            {


                string constr = SRTLib.GetDBString();
                SqlDataAdapter adapter;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("ProcIJP_GetSiteUserIdAndPassowrd"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SiteID", SiteId);
                        cmd.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(ds);
                        con.Close();
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {

                                objPortalAccount.SiteName = row["SiteName"].ToString();
                                objPortalAccount.LoginId = row["LoginId"].ToString();
                                objPortalAccount.Password = row["Password"].ToString();
                                objPortalAccount.LoginURL = row["LoginURL"].ToString();
                                objPortalAccount.LogOutURL = row["LogOutURL"].ToString();
                                objPortalAccount.RedirectURL = row["RedirectURL"].ToString();
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "GetUserIDandPassword", ex.StackTrace);
            }
            return objPortalAccount;
        }

        public PortalAccount GetPortalCredentials(int UserId, int SiteId, int GroupId, int AllocatedTime, string IPAddress)
        {
            PortalAccount objPortalAccount = new Models.PortalAccount();
            try
            {


                string constr = SRTLib.GetDBString();
                SqlDataAdapter adapter;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("ProcIJP_GetSiteCredentials"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@SiteID", SiteId);
                        cmd.Parameters.AddWithValue("@GroupId", GroupId);
                        cmd.Parameters.AddWithValue("@AllocatedTime", AllocatedTime);
                        cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                        cmd.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(ds);
                        con.Close();
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {

                                objPortalAccount.SiteName = row["SiteName"].ToString();
                                objPortalAccount.LoginId = row["LoginId"].ToString();
                                objPortalAccount.Password = row["Password"].ToString();
                                objPortalAccount.LoginURL = row["LoginURL"].ToString();
                                objPortalAccount.LogOutURL = row["LogOutURL"].ToString();
                                objPortalAccount.RedirectURL = row["RedirectURL"].ToString();
                                objPortalAccount.IsValidIP = Convert.ToInt32(row["IsValidIP"].ToString());
                                objPortalAccount.IPMessage = row["IPMessage"].ToString();
                                objPortalAccount.IsAvailable = Convert.ToInt32(row["IsAvailable"].ToString());
                                objPortalAccount.OccupiedMessage = row["OccupiedMessage"].ToString();
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "GetUserIDandPassword", ex.StackTrace);
            }
            return objPortalAccount;
        }
        public int SaveUserSiteInfo(int UserId, int GroupId, int SiteId, int AllocatedTime, string IPAddress)
        {
            UserDetails objUserDetails = new Models.UserDetails();
            int status = 0;
            try
            {


                string constr = SRTLib.GetDBString();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("_ProcISS_UserSiteInfoSave"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", UserId);
                        cmd.Parameters.AddWithValue("@GroupID", GroupId);
                        cmd.Parameters.AddWithValue("@SiteID", SiteId);
                        cmd.Parameters.AddWithValue("@AllocatedTime", AllocatedTime);
                        cmd.Parameters.AddWithValue("@UsrStatus", 1);
                        cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                        cmd.Connection = con;
                        con.Open();
                        status = Convert.ToInt32(cmd.ExecuteNonQuery());
                        con.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "SaveUserSiteInfo", ex.StackTrace);
            }
            return status;
        }

        public int UpdateUserSiteInfo(int UserId, int GroupId, int SiteId, int Status)
        {
            UserDetails objUserDetails = new Models.UserDetails();
            int status = 1;
            try
            {


                string constr = SRTLib.GetDBString();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("_ProcISS_UserSiteInfoUpdateByAdmin"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", UserId);
                        cmd.Parameters.AddWithValue("@GroupID", GroupId);
                        cmd.Parameters.AddWithValue("@SiteID", SiteId);
                        cmd.Parameters.AddWithValue("@UsrStatus", 0);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "UpdateUserSiteInfo", ex.StackTrace);
            }
            return status;
        }

        public List<string> GetSiteNames(int UserId)
        {
            List<string> SiteNameList = new List<string>();
            try
            {
                string constr = SRTLib.GetDBString();
                SqlDataAdapter adapter;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("_proc_ISSSiteInfoGroupWise"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PGroupid", UserId);
                        cmd.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(ds);
                        con.Close();
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                SiteNameList.Add(row["SiteName"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "GetSiteNames", ex.StackTrace);
            }
            return SiteNameList;
        }

        public int FreeActiveConnections(int UserId)
        {

            List<PortalAccount> ConList = GetActiveConnectionsForSingleUser(UserId);
            foreach (var account in ConList)
            {
                UpdateUserSiteInfo(UserId, account.GroupId, account.SiteId, 0);
            }

            return 1;
        }

        public IPStatus ValidateIPAddress(string IPAddress)
        {
            IPStatus oIPStatus = new IPStatus();
            try
            {
                string constr = SRTLib.GetDBString();
                SqlDataAdapter adapter;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("_procIJP_ValidateIPAddress"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                        cmd.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(ds);
                        con.Close();
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            oIPStatus.IsValid = Convert.ToInt32(ds.Tables[0].Rows[0]["IsValid"].ToString());
                            oIPStatus.IPMessage = ds.Tables[0].Rows[0]["IPMessage"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "ValidateIPAddress", ex.StackTrace);
            }
            return oIPStatus;
        }
        /// <summary>
        /// Update Consultant Details Get password date and check for the existence of user on the basis of userid
        /// </summary>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        public UserDetails GetUserEmails(string LoginId)
        {
            UserDetails objUserDetails = new Models.UserDetails();
            try
            {
                string constr = SRTLib.GetDBString();
                SqlDataAdapter adapter;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("_proc_Security_ForgetPassword"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LoginId", LoginId);
                        cmd.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(ds);
                        con.Close();
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            objUserDetails.UserName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                            if (string.IsNullOrEmpty(objUserDetails.Email))
                                objUserDetails.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                            else
                                objUserDetails.Email = "," + ds.Tables[0].Rows[0]["Email"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "GetUserEmails", ex.StackTrace);
            }
            return objUserDetails;
        }

        public int InsertAutoGenerateData(string SecurityCode, string Loginid, string ConEmailId)
        {
            int result = 0;
            try
            {
                string constr = SRTLib.GetDBString();
                SqlDataAdapter adapter;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("_procSRT_InsertAutoDetails"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SecurityCode", SecurityCode);
                        cmd.Parameters.AddWithValue("@Loginid", Loginid);
                        cmd.Parameters.AddWithValue("@EmailId", ConEmailId);
                        cmd.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(ds);
                        con.Close();
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            result = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "ValidateIPAddress", ex.StackTrace);
            }
            return result;
        }

        public UserInfo GetUserId(string LoginId)
        {
            UserInfo user = new Models.UserInfo();
            try
            {
                string constr = SRTLib.GetDBString();
                SqlDataAdapter adapter;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("_procSRT_GetUserDetails"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LoginId", LoginId);
                        cmd.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(ds);
                        con.Close();
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            user.UserId = Convert.ToInt32(ds.Tables[0].Rows[0]["USR_UserID"].ToString());
                            user.UserNameId = Convert.ToInt32(ds.Tables[0].Rows[0]["USR_UserNameId"].ToString());

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "GetUserId", ex.StackTrace);
            }
            return user;
        }

        public bool ValidateResetUser(string LoginId, string SecurityCode)
        {
            bool IsValid = false;
            try
            {
                string constr = SRTLib.GetDBString();
                SqlDataAdapter adapter;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("_procSRT_ValidateResetUser"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@USR_LoginId", LoginId);
                        cmd.Parameters.AddWithValue("@USR_SecurityCode", SecurityCode);
                        cmd.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(ds);
                        con.Close();
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            IsValid = Convert.ToBoolean(ds.Tables[0].Rows[0]["Result"].ToString());

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "ValidateResetUser", ex.StackTrace);
            }
            return IsValid;
        }
        public bool ResetUserPassword(int UserId, string NewPassword)
        {
            bool IsValid = false;
            try
            {
                string constr = SRTLib.GetDBString();
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("_procSRT_ResetPwd"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@USR_UserId", UserId);
                        cmd.Parameters.AddWithValue("@USR_Pwd", NewPassword);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "ResetUserPassword", ex.StackTrace);
            }
            return IsValid;
        }
        public bool UpdateOMSPassword(int UserNameId, string LoginId, string NewPassword)
        {
            bool IsValid = false;
            try
            {
                string constr = SRTLib.GetDBString();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("procSRT_HOEmployeeCredentials_U"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@USR_UserNameId", UserNameId);
                        cmd.Parameters.AddWithValue("@USR_LoginID", LoginId);
                        cmd.Parameters.AddWithValue("@USR_DecryptPassword", NewPassword);
                        cmd.Parameters.AddWithValue("@USR_PasswordChange", true);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "UpdateOMSPassword", ex.StackTrace);
            }
            return IsValid;
        }

        public string GetIJPPortalURL(string IPAddress)
        {
            string PortalURL = "";
            try
            {
                string constr = SRTLib.GetDBString();
                SqlDataAdapter adapter;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("_procIJP_GetIJPPortalURL"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                        cmd.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(ds);
                        con.Close();
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            PortalURL = ds.Tables[0].Rows[0]["IJPPortalURL"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "GetIJPPortalURL", ex.StackTrace);
            }
            return PortalURL;
        }
        public void Deduct(int UserId, int GroupId, int SiteId)
        {
            try
            {
                string constr = SRTLib.GetDBString();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("_procIJP_DeductCreditsOnResumeView"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userId", UserId);
                        cmd.Parameters.AddWithValue("@groupId", GroupId);
                        cmd.Parameters.AddWithValue("@siteId", SiteId);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "Deduct", ex.StackTrace);
            }
        }

        public int IsUserActive(int UserId)
        {
            int IsActive = 0;
            try
            {
                string constr = SRTLib.GetDBString();
                SqlDataAdapter adapter;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("_procIJP_IsUserActive"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(ds);
                        con.Close();
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            IsActive = Convert.ToInt32(ds.Tables[0].Rows[0]["IsActive"].ToString());

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "IsUserActive", ex.StackTrace);
            }
            return IsActive;
        }

        public int IsLinkVerified(string LoginId)
        {
            int IsVerified = 0;
            try
            {
                string constr = SRTLib.GetDBString();
                SqlDataAdapter adapter;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("ijp_ziprecruiter_is_link_verified"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@login_id", LoginId);
                        cmd.Connection = con;
                        con.Open();
                        adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(ds);
                        con.Close();
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            IsVerified = Convert.ToInt32(ds.Tables[0].Rows[0]["IsVerified"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.Message, "DBLib", "IsLinkVerified", ex.StackTrace);
            }

            return IsVerified;
        }



        #region Portal

        // Getting Portal By Id
        public async Task<Portal> GetPortal(Int32 PortalId)
        {
            try
            {
                String ConnectionString = SRTLib.GetDBString();
                SqlConnection db = new SqlConnection(ConnectionString);
                var parms = new
                {
                    PortalId = PortalId
                };

                Portal portal = (await db.QueryFirstOrDefaultAsync<Portal>("GetPortals", param: parms, commandType: CommandType.StoredProcedure));
                return portal;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Getting Portals List
        public async Task<List<Portal>> GetPortals()
        {
            try
            {
                String ConnectionString = SRTLib.GetDBString();
                SqlConnection db = new SqlConnection(ConnectionString);


                List<Portal> portals = (await db.QueryAsync<Portal>("GetPortals", commandType: CommandType.StoredProcedure)).ToList();
                return portals;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Add/Update Portal
        public async Task<bool> AddOrUpdatePortal(PortalEntity portal)
        {
            try
            {
                String ConnectionString = SRTLib.GetDBString();
                SqlConnection db = new SqlConnection(ConnectionString);
                var parms = new
                {
                    PortalId = portal.PortalId,
                    PortalName = portal.PortalName,
                    CreateBy = portal.CreateBy
                };


                var affectedRows = await db.ExecuteAsync("AddOrUpdatePortal", param: parms, commandType: CommandType.StoredProcedure);

                if (affectedRows != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        // Getting Job Portal By Id
        public async Task<JobPortal> GetJobPortal(Int32 PortalId)
        {
            try
            {
                String ConnectionString = SRTLib.GetDBString();
                SqlConnection db = new SqlConnection(ConnectionString);
                var parms = new
                {
                    PortalId = PortalId
                };

                JobPortal portal = (await db.QueryFirstOrDefaultAsync<JobPortal>("GetJobPortals", param: parms, commandType: CommandType.StoredProcedure));
                return portal;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Getting Job Portals List 
        public async Task<List<JobPortal>> GetJobPortals()
        {
            try
            {
                String ConnectionString = SRTLib.GetDBString();
                SqlConnection db = new SqlConnection(ConnectionString);


                List<JobPortal> portals = (await db.QueryAsync<JobPortal>("GetJobPortals", commandType: CommandType.StoredProcedure)).ToList();
                return portals;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Add/Update Portal
        public async Task<bool> AddOrUpdateJobPortal(JobPortalEntity portal)
        {
            try
            {
                String ConnectionString = SRTLib.GetDBString();
                SqlConnection db = new SqlConnection(ConnectionString);
                var parms = new
                {
                    PortalId = portal.PortalId,
                    PortalAccountId = portal.PortalAccountId,
                    PortalLoginId = portal.PortalLoginId,
                    PortalPassword = portal.PortalPassword,
                    IsFreeze = portal.IsFreeze,
                    CreatedBy = portal.CreatedBy,
                    ModifiedBy = portal.ModifiedBy,
                    AccountStartDate = portal.AccountStartDate,
                    AccountExpiryDate = portal.AccountExpiryDate,
                    ContactPerson = portal.ContactPerson,
                    Remarks = portal.Remarks,
                    PortalUsesTerms = portal.PortalUsesTerms,
                    DefaultSiteTime = portal.DefaultSiteTime,
                    IsInActiveForADay = portal.IsInActiveForADay,
                    PortalInactiveTillDate = portal.PortalInactiveTillDate,
                    ResumeViewsLimit = portal.ResumeViewsLimit,

                };


                var affectedRows = await db.ExecuteAsync("AddOrUpdateJobPortalDetail", param: parms, commandType: CommandType.StoredProcedure);

                if (affectedRows != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}