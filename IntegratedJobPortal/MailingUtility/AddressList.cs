using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegratedJobPortal.MailingUtility
{
    /// <summary>
    ///  Store e-mail addresses.
    /// </summary>
    public class AddressList
    {
        #region Members Variables

        private System.Net.Mail.MailAddressCollection _Addresses;

        #endregion

        #region Initialization

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AddressList()
        {
            _Addresses = new System.Net.Mail.MailAddressCollection();
        }


        #endregion

        #region Public Methods


        /// <summary>
        /// Add the email address to address list.
        /// </summary>
        /// <param name="mailAddress">it contains an e-mail address</param>
        public void AddAddress(string mailAddress)
        {
            try
            {
                _Addresses.Add(new System.Net.Mail.MailAddress(mailAddress));
            }
            catch
            {

                throw;
            }

        }

        /// <summary>
        /// Add the email address and display name to address list.
        /// </summary>
        /// <param name="mailAddress">it contains an e-mail address</param>
        /// <param name="displayName">it contains the display name associated with address</param>
        public void AddAddress(string mailAddress, string displayName)
        {
            try
            {
                _Addresses.Add(new System.Net.Mail.MailAddress(mailAddress, displayName));
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Gets the list of e-mail address.
        /// </summary>
        internal System.Net.Mail.MailAddressCollection GetAddressList
        {
            get { return _Addresses; }
        }


        #endregion


    }
}