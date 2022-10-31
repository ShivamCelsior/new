using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegratedJobPortal.MailingUtility
{

    /// <summary>
    /// Store e-mail address.
    /// </summary>
    public class Address
    {

        #region Members Variables

        private string _MailAddress;
        private string _DisplayName;

        #endregion

        #region Public Properties

        /// <summary>
        ///Gets or sets the e-mail address.
        /// </summary>
        public String MailAddress
        {
            get { return _MailAddress; }
            set
            {
                System.Net.Mail.MailAddress testEmail = new System.Net.Mail.MailAddress(value);
                _MailAddress = value;
            }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public String DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; }
        }


        #endregion

        #region Initialization

        /// <summary>
        /// Empty default constructor.
        /// </summary>
        public Address()
        {

        }

        /// <summary>
        /// Initializes a new instance of class using the specified address.
        /// </summary>
        /// <param name="mailAddress">it contains an e-mail address.</param>
        public Address(string mailAddress)
        {
            this._MailAddress = mailAddress;

        }

        /// <summary>
        /// Initializes a new instance with mail address and dispaly name.
        /// </summary>
        /// <param name="mailAddress">it contains an e-mail address.</param>
        /// <param name="displayName">it contains the display name associated with address.</param>
        public Address(string mailAddress, string displayName)
            : this(mailAddress)
        {
            this._DisplayName = displayName;

        }


        #endregion




    }
}