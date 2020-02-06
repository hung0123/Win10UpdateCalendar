using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Ionic.Zip;

namespace Web1.Common
{
    public class Utility
    {
        public static string CheckSqlInjection(string sql)
        {
            if (sql == null)
            {
                return null;
            }
            return sql.Replace("'", "''");
        }

        /// <summary>
        /// 匯出(ZIP檔)
        /// </summary>
        public static byte[] ExportZip(string tempfile)
        {
            string tempZipfile = "";
            try
            {
                tempZipfile = tempfile;
                if (tempfile.LastIndexOf(".") != -1)
                {
                    tempZipfile = tempfile.Substring(0, tempfile.LastIndexOf(".")) + ".zip";
                }

                //檔案壓縮
                if (File.Exists(tempZipfile)) File.Delete(tempZipfile);
                using (ZipFile zip = new ZipFile(tempZipfile, System.Text.Encoding.GetEncoding(950)))
                {
                    zip.AddFile(tempfile, "");
                    zip.Save();
                }
                return getBytes(tempZipfile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 匯出(ZIP檔)-多個xls檔
        /// </summary>
        /// <param name="tempFolder">資料夾路徑</param>
        /// <param name="tempZipFile">壓縮名</param>
        public static byte[] ExportZip_Multiple(string tempFolder, string tempZipFile)
        {
            try
            {
                //檔案壓縮
                if (File.Exists(tempZipFile)) File.Delete(tempZipFile);
                using (ZipFile zip = new ZipFile(tempZipFile, System.Text.Encoding.GetEncoding(950)))
                {
                    foreach (string tempfile in Directory.GetFileSystemEntries(tempFolder))
                    {
                        zip.AddFile(tempfile, "");
                    }

                    zip.Save();
                }
                return getBytes(tempZipFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 將檔案轉成2進位元
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private static byte[] getBytes(string FilePath)
        {
            byte[] buffer = null;

            if (!System.IO.File.Exists(FilePath))
            {
                throw new Exception("File Not Exist :" + FilePath);
            }
            System.IO.Stream s = null;
            try
            {
                s = System.IO.File.OpenRead(FilePath);//For Read Only
                buffer = new byte[s.Length];
                s.Read(buffer, 0, buffer.Length);
                s.Close();
                return buffer;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (s != null)
                    s.Close();
            }
        }
    }
}
