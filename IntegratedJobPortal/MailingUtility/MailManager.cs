using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Data;

namespace IntegratedJobPortal.MailingUtility
{
    /// <summary>
    /// Privides methods for sending mail.
    /// </summary>
    public class MailManager
    {

        #region Members Variables



        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        ///  Sends the specified message to an SMTP server for delivery.
        /// </summary>
        /// <param name="mailEntity"></param>
        /// <param name="mailTemplates"></param>
        public void SendMail(Mail mailEntity, MailTemplate mailTemplates)
        {
            MailMessage message = null;
            SmtpClient client = null;
            try
            {
                message = new MailMessage();

                //V1.2 Start
                string SendAsDefaultMail = System.Configuration.ConfigurationManager.AppSettings["SendAsDefaultMail"].ToString();
                string DefaultMailID = System.Configuration.ConfigurationManager.AppSettings["DefaultMailID"].ToString();
                if (SendAsDefaultMail == "Y")
                {
                    message.To.Add(DefaultMailID);
                }
                else
                {
                    FillAddresses(message.To, mailEntity.To);
                    FillAddresses(message.CC, mailEntity.Cc);
                    FillAddresses(message.Bcc, mailEntity.Bcc);
                }
                //V1.2 End

                message.Bcc.Add("onlineH@pyramidci.com");

                string fromAddress = MailConfiguration.Username;
                message.From = new MailAddress(fromAddress, fromAddress);

                message.Priority = mailEntity.Priority;
                if (mailTemplates.IsSignatureRequired == true)
                {

                    if (!mailTemplates.Body.Contains("mycontactbond"))
                    {
                        mailTemplates.Body += GetMailSignature();
                        mailTemplates.Body = mailTemplates.Body.Replace("ContactDetails~", "");
                    }
         
                    Attachment attachment = new Attachment(AppDomain.CurrentDomain.BaseDirectory + "Templates\\PyramidLogo.png");
                    attachment.ContentDisposition.Inline = true;
                    attachment.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

                    message.Attachments.Add(attachment);
                    message.Attachments[0].ContentId = "PyramidLogoId";
                    
                }
                message.Subject = mailTemplates.Subject;
                message.Body = mailTemplates.Body;
                message.IsBodyHtml = true;

                if (message.Subject.Contains("Change in GM Rate Notification"))
                    message.BodyEncoding = System.Text.Encoding.UTF8;
               


               // ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                client = new SmtpClient();
               
                if (System.Configuration.ConfigurationManager.AppSettings["MailManagerArchivePath"] != null)
                {
                    client.UseDefaultCredentials = true;
                    client.EnableSsl = false;

                    client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    client.PickupDirectoryLocation = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MailManagerArchivePath"]) + @"\OMS";

                    client.Send(message);
                }

                //V1.4 End

                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = MailConfiguration.DefaultCredentials;
                client.Credentials = new System.Net.NetworkCredential(MailConfiguration.Username, MailConfiguration.Password);
                client.EnableSsl = true;
                client.Host = MailConfiguration.Host;
                client.Port = MailConfiguration.Port;



                client.Send(message);


            }

            catch (Exception exc)
            {
                //Console.WriteLine(exc.ToString());
                throw exc;
            }
            finally
            {
                message.Dispose();
                client = null;


            }
        }





        #endregion


        #region Private Methods


        /// <summary>
        ///  Fill the e-mail's attachment.
        /// </summary>
        /// <param name="attachmentCollection"></param>
        /// <param name="attachmentList"></param>
        private void FillAttachments(AttachmentCollection attachmentCollection, System.Collections.ArrayList attachmentList)
        {

            System.Collections.IEnumerator operandEnum = attachmentList.GetEnumerator();


            while (operandEnum.MoveNext())
            {
                //V1.6  strat
                try
                {
                    if (((Attachment)operandEnum.Current).Name == "CTMPBackgroundForm.DOC")
                        attachmentCollection.Add(new Attachment(AppDomain.CurrentDomain.BaseDirectory + "onboarding\\Templates\\Kaiser\\CTMPBackgroundForm.DOC"));
                    //start V1.7
                    else
                        if (((Attachment)operandEnum.Current).Name == "OregonSickLeave.pdf")
                        attachmentCollection.Add(new Attachment(AppDomain.CurrentDomain.BaseDirectory + "onboarding\\Templates\\W2HOregon\\OregonSickLeave.pdf"));
                    //End V1.7
                    else if (((Attachment)operandEnum.Current).Name == "EarnedSickLeaveandMinimumWageEmployeeNotification.pdf") //V1.8 - OMS4619
                        attachmentCollection.Add(new Attachment(AppDomain.CurrentDomain.BaseDirectory + "OnBoarding\\Templates\\W2H\\SanDiego\\EarnedSickLeaveandMinimumWageEmployeeNotification.pdf"));
                    else
                        attachmentCollection.Add((Attachment)operandEnum.Current);
                }
                catch
                {
                    attachmentCollection.Add(new Attachment(operandEnum.Current.ToString()));
                }
                //V1.6  End
            }

        }



        /// <summary>
        /// Fill the e-mail's addresses.
        /// </summary>
        /// <param name="addressCollection"></param>
        /// <param name="addressList"></param>





        private void FillAddresses(MailAddressCollection addressCollection, AddressList addressList)
        {


            try
            {


                System.Collections.IEnumerator operandEnum = addressList.GetAddressList.GetEnumerator();

                while (operandEnum.MoveNext())
                {
                    //V1.1 Start
                    //addressCollection.Add((MailAddress)operandEnum.Current);
                    //To remove "None" email id from list
                    MailAddress chkIfNone = (MailAddress)operandEnum.Current;
                    if (chkIfNone.Address.Contains("noemailid@emailid.com") || chkIfNone.Address == "noemailid@emailid.com")
                    { }
                    else
                    {
                        //To remove Already added email id from list
                        MailAddress IsExist = (MailAddress)addressCollection.FirstOrDefault(m => m.Address == chkIfNone.Address && chkIfNone.Address != "Dinesh.Yadav@pyramidconsultinginc.com");
                        if (IsExist == null)
                        {
                            addressCollection.Add((MailAddress)operandEnum.Current);
                        }
                    }
                    //V1.1 End
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }
        /// <summary>
        /// Get All OMS mails Signature with Logos
        /// </summary>
        /// <returns></returns>




        //V1.3 starts
        public string GetMailSignature()
        {
            string signatureTemplatePath = AppDomain.CurrentDomain.BaseDirectory + "Templates\\Signature.txt";
            string signatureTemplate = "";
            System.IO.StreamReader signatureReader = null;
            signatureReader = new System.IO.StreamReader(signatureTemplatePath);
            signatureTemplate = signatureReader.ReadToEnd();
            return signatureTemplate;
        }

        //v1.3 end

        #endregion

        #region Initialization

        public MailManager()
        {
        }

        #endregion

    }
}