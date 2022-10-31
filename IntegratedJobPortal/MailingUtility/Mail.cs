using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace IntegratedJobPortal.MailingUtility
{

    public class Mail
    {

        #region Members Variables

        private AddressList _To;
        private AddressList _Cc;
        private AddressList _Bcc;
        private Address _From;
        private MailPriority _Priority;
        private System.Collections.ArrayList _Attachments;

        #endregion

        #region Initialization

        /// <summary>
        /// Empty default constructor.
        /// </summary>
        public Mail()
        {
            _To = new AddressList();
            _Cc = new AddressList();
            _Bcc = new AddressList();
            _From = new Address();
            _Attachments = new System.Collections.ArrayList();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the addresses of the recipients of the e-mail
        /// </summary>
        public AddressList To
        {
            get { return _To; }
            set { _To = value; }
        }

        /// <summary>
        // Gets or sets the address collection that contains the carbon copy (CC) recipients
        //for this e-mail message.
        /// </summary>
        public AddressList Cc
        {
            get { return _Cc; }
            set { _Cc = value; }
        }

        /// <summary>
        // Gets or sets the address collection that contains the blind carbon copy (BCC) recipients
        //for this e-mail message.
        /// </summary>
        public AddressList Bcc
        {
            get { return _Bcc; }
            set { _Bcc = value; }
        }

        /// <summary>
        /// Gets or sets the from address for this e-mail message.
        /// </summary>
        public Address From
        {
            get { return _From; }
            set { _From = value; }
        }

        public MailPriority Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }

        /// <summary>
        /// Gets or sets the Attachments for this e-mail message.
        /// </summary>
        public System.Collections.ArrayList Attachments
        {
            get { return _Attachments; }
            set { _Attachments.Add(value); }
        }
        #endregion





    }

}