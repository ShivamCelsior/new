using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.Configuration;

namespace IntegratedJobPortal.MailingUtility
{
    /// <summary>
    /// Provides access to mail configuration as they define in settings.
    /// </summary>
    internal static class MailConfiguration
    {
        #region Members Variables

        private static readonly string _Host;
        private static readonly Int32 _Port;
        private static readonly string _Username;
        private static readonly string _Password;
        private static readonly string _From;
        private static readonly bool _DefaultCredentials;

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the MailConfiguration class. It calls only once.
        /// </summary>
        static MailConfiguration()
        {
            try
            {
                Configuration config;
                if (HttpContext.Current != null)
                    config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                else
                    config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


                MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

                if (settings.Smtp.Network.Host == null)
                {
                    throw new ArgumentNullException("host");
                }
                if (settings.Smtp.Network.Host == string.Empty)
                {
                    throw new Exception("Host name cannot be empty");
                }

                _Host = settings.Smtp.Network.Host;
                _Port = settings.Smtp.Network.Port;
                _Username = settings.Smtp.Network.UserName;
                _Password = settings.Smtp.Network.Password;
                _From = settings.Smtp.From;
                _DefaultCredentials = settings.Smtp.Network.DefaultCredentials;
            }
            catch
            {

                throw;
            }
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets  the name of the SMTP server
        /// </summary>
        public static string Host
        {
            get { return _Host; }
        }

        /// <summary>
        /// Gets the port that SMTP clients use to connect to an SMTP mail server.
        /// The default value is 25.
        /// </summary>
        public static Int32 Port
        {
            get { return _Port; }

        }

        /// <summary>
        /// Gets the user name to connect to an SMTP mail server.
        /// </summary>
        public static string Username
        {
            get { return _Username; }

        }

        /// <summary>
        /// Gets the user password to use to connect to an SMTP mail server.
        /// </summary>
        public static string Password
        {
            get { return _Password; }

        }

        /// <summary>
        /// Gets the default value that indicates who the email message is from
        /// </summary>
        public static string From
        {
            get { return _From; }

        }

        /// <summary>
        /// Determines whether or not default user credentials are used to access an
        /// SMTP server. The default value is false.
        /// </summary>
        public static bool DefaultCredentials
        {
            get { return _DefaultCredentials; }

        }


        #endregion

    }
}