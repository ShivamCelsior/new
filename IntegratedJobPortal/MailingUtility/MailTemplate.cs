using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegratedJobPortal.MailingUtility
{
    /// <summary>
    /// Represents a specific template.
    /// </summary>
    public class MailTemplate
    {

        #region Members Variables


        /// <summary>
        /// The subject of the message.
        /// </summary>
        private string _Subject;

        /// <summary>
        /// The body of the message.
        /// </summary>
        private string _Body;

        /// <summary>
        /// Is Company Signature part required.
        /// </summary>
        private bool _IsSignatureRequired = true;

        #endregion

        #region Properties


        /// <summary>
        /// The subject of the message.
        /// </summary>
        public string Subject
        {
            get { return this._Subject; }
            set { this._Subject = value; }
        }


        /// <summary>
        /// The body of the message.
        /// </summary>
        public string Body
        {
            get { return this._Body; }
            set { this._Body = value; }
        }

        /// <summary>
        /// Is Company Signature part required.
        /// </summary>
        public bool IsSignatureRequired
        {
            get { return this._IsSignatureRequired; }
            set { this._IsSignatureRequired = value; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Empty default constructor.
        /// </summary>
        public MailTemplate()
        {
        }

        #endregion


    }
}