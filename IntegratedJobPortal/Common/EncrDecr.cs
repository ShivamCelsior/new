using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace IntegratedJobPortal.Common
{
    public class EncrDecr
    {
        /// <summary>
        /// Decode string
        /// </summary>
        /// <param name="encodedText">Encoded string</param>
        /// <returns>Plain text</returns>
        //public static string DecodeString(string encodedText)
        //{
        //    byte[] stringBytes = Convert.FromBase64String(encodedText);
        //    return Encoding.Unicode.GetString(stringBytes);
        //}

        /// <summary>
        /// Encode string 
        /// </summary>
        /// <param name="origText">Plain Text</param>
        /// <returns>Encoded string without special characters</returns>
        //public static string EncodeString(string origText)
        //{
        //    byte[] stringBytes = Encoding.Unicode.GetBytes(origText);
        //    return Convert.ToBase64String(stringBytes, 0, stringBytes.Length);
        //}
        /// <summary>
		/// function to encrypt string
		/// </summary>
		/// <param name="Value"></param>
		/// <returns></returns>
		public static string Encrypt(string Value)
        {
            string ReturnString = string.Empty;
            if (Value != "" && Value != string.Empty)
            {
                System.Text.StringBuilder EncryptString = new System.Text.StringBuilder();
                char[] CharArray = Value.ToCharArray();
                try
                {
                    foreach (char ch in CharArray)
                    {
                        int val = System.Convert.ToInt32(ch);
                        val = val + 500;
                        EncryptString.Append(val.ToString());
                    }
                }
                catch
                {
                }
                finally
                {
                    ReturnString = EncryptString.ToString();
                    EncryptString = null;
                }
            }
            return ReturnString;
        }

        /// <summary>
        /// function to decrypt string
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string Decrypt(string Value)
        {
            string ReturnString = string.Empty;
            if (Value != "" && Value != string.Empty)
            {
                System.Text.StringBuilder DecryptString = new System.Text.StringBuilder();
                try
                {
                    for (int i = 0; i < Value.Length; i += 3)
                    {
                        string str = Value.Substring(i, 3);
                        int val = System.Convert.ToInt32(str, 10);
                        val = val - 500;
                        char ch = System.Convert.ToChar(val);
                        DecryptString.Append(ch);
                    }
                }
                catch
                {
                }
                finally
                {
                    ReturnString = DecryptString.ToString();
                    DecryptString = null;
                }
            }
            return ReturnString;
        }
        public static string ApiTokenKey
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings["apitokenkey"];
            }
        }

        public static bool IsTokenValid(string token)
        {
            return string.Compare(token, ApiTokenKey, true) == 0;
        }
    }
}