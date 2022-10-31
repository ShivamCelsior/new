using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace IntegratedJobPortal.Common
{
    public sealed class SRTLib
    {
        private SRTLib() { }

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

        /// <summary>
        /// Function to get connection string from file
        /// </summary>
        /// <param name="sKeyFile"></param>
        /// <returns></returns>
        public static string GetDBString()
        {

            string sKeyFile = AppDomain.CurrentDomain.BaseDirectory;
            sKeyFile += @"Config/DBKey.dat";
            string ConnectString = string.Empty;
            if (sKeyFile != "" && sKeyFile != string.Empty)
            {
                if (File.Exists(sKeyFile))
                {
                    StreamReader KFile;
                    string BufferString = string.Empty;
                    try
                    {
                        KFile = File.OpenText(sKeyFile);
                        BufferString = KFile.ReadLine();
                        KFile.Close();
                        ConnectString = Decrypt(BufferString);
                    }
                    catch
                    {
                    }
                }
            }
            return ConnectString;
        }

        /// <summary>
        /// function to wirte connection string in file
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="FileName"></param>
        /// <param name="Server"></param>
        /// <param name="Database"></param>
        /// <param name="UserId"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        public static bool SetDBString(string Server, string Database, string UserId, string PWD)
        {

            string FilePath = AppDomain.CurrentDomain.BaseDirectory;
            FilePath += "Config";
            //sKeyFile += @"Security\SRTKey.dat";
            string FileName = "DBKey.dat";
            //string FilePath = "E:\\PIS\\PIS1.4\\Security";
            bool IsSave = false;
            string ConString = string.Empty;
            string EncryptString = string.Empty;
            SqlConnection SqlCon;
            TextWriter KFile;

            if (Server != "" && Server != string.Empty && Database != "" && Database != string.Empty && UserId != "" && UserId != string.Empty && PWD != "" && PWD != string.Empty)
            {
                ConString = "Server=" + Server + ";";
                ConString += "database=" + Database + ";";
                ConString += "uid=" + UserId + ";";
                ConString += "pwd=" + PWD + ";";
                ConString += "max pool size=1000;";
                ConString += "min pool size=50;"; //V1.1
            }

            if (ConString != "" && ConString != string.Empty)
            {
                SqlCon = new SqlConnection(ConString);
                try
                {
                    SqlCon.Open();
                    if (SqlCon.State == System.Data.ConnectionState.Open)
                    {
                        if (!(System.IO.Directory.Exists(FilePath)))
                            System.IO.Directory.Exists(FilePath);

                        if ((System.IO.Directory.Exists(FilePath)))
                        {
                            string AbsFileName;
                            AbsFileName = FilePath + "\\" + FileName;
                            KFile = File.CreateText(AbsFileName);
                            EncryptString = Encrypt(ConString);
                            KFile.WriteLine(EncryptString);
                            KFile.Close();
                            IsSave = true;
                        }
                    }
                    SqlCon.Close();
                }
                catch
                {
                    IsSave = false;
                }
                finally
                {
                    SqlCon.Dispose();
                }
            }
            return IsSave;
        }
    }
}