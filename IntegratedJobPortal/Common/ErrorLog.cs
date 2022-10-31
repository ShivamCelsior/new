using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace IntegratedJobPortal.Common
{
    public class ErrorLog
    {
        private static string strDocName = string.Empty;
        private static string strExtentionDocName = string.Empty;
        public static void WriteErrorLog(string ErrorMsg, string strModule, string strMethod = "", string stackTrace = "") 
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            System.Xml.XmlElement xmlErrorElemnt;
            System.Xml.XmlElement xmlErrorMsgElemnt;
            System.Xml.XmlElement xmlModuleElemnt;
            System.Xml.XmlElement xmlMethodElemnt;
            System.Xml.XmlElement xmlTimeElemnt;
            System.Xml.XmlElement stackTraceElemnt; 
            

            try
            {
                if (ErrorMsg == "Thread was being aborted."
                    || (ErrorMsg.Contains("The controller for path")
                    && ErrorMsg.Contains("was not found or does not implement IController")))
                {
                    return;
                }

                CreateLogFile();

                xmlDoc.Load(strDocName);

                xmlErrorElemnt = xmlDoc.CreateElement("error");

                xmlErrorMsgElemnt = xmlDoc.CreateElement("errorMsg");
                xmlErrorMsgElemnt.InnerText = ErrorMsg;
                xmlErrorElemnt.AppendChild(xmlErrorMsgElemnt);
                
                if (!string.IsNullOrEmpty(stackTrace))
                {
                    stackTraceElemnt = xmlDoc.CreateElement("stackTrace");
                    stackTraceElemnt.InnerText = stackTrace;
                    xmlErrorElemnt.AppendChild(stackTraceElemnt);
                }

                xmlModuleElemnt = xmlDoc.CreateElement("module");
                xmlModuleElemnt.InnerText = strModule;
                xmlErrorElemnt.AppendChild(xmlModuleElemnt);

                xmlMethodElemnt = xmlDoc.CreateElement("method");
                xmlMethodElemnt.InnerText = strMethod;
                xmlErrorElemnt.AppendChild(xmlMethodElemnt);

                xmlTimeElemnt = xmlDoc.CreateElement("dateTime");
                xmlTimeElemnt.InnerText = DateTime.Now.ToString();
                xmlErrorElemnt.AppendChild(xmlTimeElemnt);

               

                xmlDoc.DocumentElement.AppendChild(xmlErrorElemnt);
                xmlDoc.Save(strDocName);
            }
            catch
            {
                throw;
            }
        }

        public static void WriteLog(string ErrorMsg, string strModule, string forWhich = "")
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            System.Xml.XmlElement xmlErrorElemnt;
            System.Xml.XmlElement xmlErrorMsgElemnt;
            System.Xml.XmlElement xmlModuleElemnt;
            System.Xml.XmlElement xmlTimeElemnt;
            System.Xml.XmlElement forWhichElemnt;
            

            try
            {
                if (ErrorMsg == "Thread was being aborted.")
                {
                    return;
                }

                CreateLogFile();

                xmlDoc.Load(strDocName);

                xmlErrorElemnt = xmlDoc.CreateElement("error");

                xmlErrorMsgElemnt = xmlDoc.CreateElement("errorMsg");
                xmlErrorMsgElemnt.InnerText = ErrorMsg;
                xmlErrorElemnt.AppendChild(xmlErrorMsgElemnt);

                xmlModuleElemnt = xmlDoc.CreateElement("module");
                xmlModuleElemnt.InnerText = strModule;
                xmlErrorElemnt.AppendChild(xmlModuleElemnt);

                xmlTimeElemnt = xmlDoc.CreateElement("dateTime");
                xmlTimeElemnt.InnerText = DateTime.Now.ToString();
                xmlErrorElemnt.AppendChild(xmlTimeElemnt);

                if (!string.IsNullOrEmpty(forWhich))
                {
                    forWhichElemnt = xmlDoc.CreateElement("forWhich");
                    forWhichElemnt.InnerText = forWhich;
                    xmlErrorElemnt.AppendChild(forWhichElemnt);
                }

                

                xmlDoc.DocumentElement.AppendChild(xmlErrorElemnt);
                xmlDoc.Save(strDocName);
            }
            catch
            {
                throw;
            }
        }

     
        private static void CreateLogFile()
        {
            string filePath = string.Empty;

            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            string component = "<IJP></IJP>";

            try
            {
                strDocName = day + "_" + month + "_" + year + ".xml";
                filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) + "ErrorLog\\";
                strDocName = filePath + strDocName;

                if (!File.Exists(strDocName))
                {
                    XmlDocument objXmlDoc = new XmlDocument();
                    objXmlDoc.LoadXml(component);
                    objXmlDoc.Save(strDocName);
                }
            }
            catch (Exception ex)
            {           
                throw;
            }
        }

        private static void CreateExtensionLogFile()
        {
            string filePath = string.Empty;

            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            string component = "<IJP></IJP>";

            try
            {
                strExtentionDocName = day + "_" + month + "_" + year + ".xml";
                filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) + "ErrorLog\\Extension\\";
                strExtentionDocName = filePath + strExtentionDocName;

                if (!File.Exists(strExtentionDocName))
                {
                    XmlDocument objXmlDoc = new XmlDocument();
                    objXmlDoc.LoadXml(component);
                    objXmlDoc.Save(strExtentionDocName);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void WriteExtensionErrorLog(string UserId, string UserName, string ErrorMsg, string Source, string Version = "", string PageURL = "")
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            System.Xml.XmlElement xmlErrorElemnt;
            System.Xml.XmlElement xmlUserIdElemnt;
            System.Xml.XmlElement xmlUserNameElemnt;
            System.Xml.XmlElement xmlErrorMsgElemnt;           
            System.Xml.XmlElement xmlSourceElemnt;
            System.Xml.XmlElement xmlVersionElemnt;
            System.Xml.XmlElement stackPageURLElemnt;
            System.Xml.XmlElement xmlTimeElemnt;
       


            try
            {
                if (ErrorMsg == "Thread was being aborted."
                    || (ErrorMsg.Contains("The controller for path")
                    && ErrorMsg.Contains("was not found or does not implement IController")))
                {
                    return;
                }

                CreateExtensionLogFile();

                xmlDoc.Load(strExtentionDocName);

                xmlErrorElemnt = xmlDoc.CreateElement("error");

                xmlErrorMsgElemnt = xmlDoc.CreateElement("errorMsg");
                xmlErrorMsgElemnt.InnerText = ErrorMsg;
                xmlErrorElemnt.AppendChild(xmlErrorMsgElemnt);

                if (!string.IsNullOrEmpty(UserId))
                {
                    xmlUserIdElemnt = xmlDoc.CreateElement("UserId");
                    xmlUserIdElemnt.InnerText = UserId;
                    xmlErrorElemnt.AppendChild(xmlUserIdElemnt);
                }

                if (!string.IsNullOrEmpty(UserName))
                {
                    xmlUserNameElemnt = xmlDoc.CreateElement("UserName");
                    xmlUserNameElemnt.InnerText = UserName;
                    xmlErrorElemnt.AppendChild(xmlUserNameElemnt);
                }
                if (!string.IsNullOrEmpty(Source))
                {
                    xmlSourceElemnt = xmlDoc.CreateElement("Source");
                    xmlSourceElemnt.InnerText = Source;
                    xmlErrorElemnt.AppendChild(xmlSourceElemnt);
                }
                if (!string.IsNullOrEmpty(Version))
                {
                    xmlVersionElemnt = xmlDoc.CreateElement("Version");
                    xmlVersionElemnt.InnerText = Version;
                    xmlErrorElemnt.AppendChild(xmlVersionElemnt);
                }
                if (!string.IsNullOrEmpty(PageURL))
                {
                    stackPageURLElemnt = xmlDoc.CreateElement("PageURL");
                    stackPageURLElemnt.InnerText = PageURL;
                    xmlErrorElemnt.AppendChild(stackPageURLElemnt);
                }

                xmlTimeElemnt = xmlDoc.CreateElement("dateTime");
                xmlTimeElemnt.InnerText = DateTime.Now.ToString();
                xmlErrorElemnt.AppendChild(xmlTimeElemnt);



                xmlDoc.DocumentElement.AppendChild(xmlErrorElemnt);
                xmlDoc.Save(strExtentionDocName);
            }
            catch
            {
                throw;
            }
        }
    }
}