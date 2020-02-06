using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Syncfusion.XlsIO;
using System.IO;
using Ionic.Zip;

namespace Web1.Common.Excel
{
    public class Excel
    {
        private ExcelEngine excelEngine { get; set; }
        private IApplication application { get; set; }
        /// <summary>
        /// KEY:檔名 VALUE:資料
        /// </summary>
        public Dictionary<string, MemoryStream> ZipFileDictionary { get; set; }
        //private IWorkbook workbook { get; set; }

        public Excel()
        {
            InitExcelEngine();
            ZipFileDictionary = new Dictionary<string, MemoryStream>();
        }

        /// <summary>
        /// Open Template
        /// </summary>
        /// <param name="sourcePath">File Path</param>
        /// <returns>WorkBook</returns>
        public IWorkbook Open(string filepath)
        {
            if (excelEngine == null || application == null)
            {
                InitExcelEngine();
            }

            IWorkbook workbook = application.Workbooks.Open(filepath);
            return workbook;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Close(IWorkbook workbook)
        {
            workbook.Close();
            excelEngine.Dispose();
        }

        /// <summary>
        /// 下載Excel
        /// </summary>
        /// <param name="filename">File Name</param>
        /// <param name="workbook">把那個WorkBook傳回來就對了</param>
        public void Save(string filename, IWorkbook workbook)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.SaveAs(ms);
                ExportFile(ms.ToArray(), filename);
            }
        }

        /// <summary>
        /// 直接下載範本檔
        /// </summary>
        public void DownloadTemplate(string filepath, string filename)
        {
            IWorkbook workbook = Open(filepath);
            IWorksheet sheet = workbook.Worksheets[0];
            sheet.Unprotect(string.Empty);
            Save(filename, workbook);
            Close(workbook);
        }

        /// <summary>
        /// 下載多個Excel在一個壓縮檔內
        /// </summary>
        /// <param name="filename">壓縮檔檔名</param>
        public void ExportZipfile(string filename)
        {
            using (ZipFile zip = new ZipFile(System.Text.Encoding.Default))
            {
                foreach (var r in ZipFileDictionary)
                {
                    zip.AddEntry(r.Key, r.Value.ToArray());
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    zip.Save(ms);
                    ExportFile(ms.ToArray(), filename);
                }
            }
        }

        private void InitExcelEngine()
        {
            excelEngine = new ExcelEngine()
            {
                ThrowNotSavedOnDestroy = false
            };
            application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Excel2013;
        }

        private void ExportFile(byte[] b, string FileName)
        {
            System.Web.HttpResponse res = null;
            res = System.Web.HttpContext.Current.Response;
            res.ClearHeaders();
            res.Clear();
            res.ClearContent();
            res.AddHeader("Accept-Language", "zh-tw");
            res.ContentType = "Application/octect-stream";
            res.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(FileName));
            res.AppendHeader("Content-Length", b.Length.ToString()); //表頭加入檔案大小
            res.BinaryWrite(b);

            //jquery.fileDownload.js 會使用此cookie作為完成處理的屬性比對
            res.SetCookie(new HttpCookie("fileDownload", "true"));

            res.Flush();
            res.Close();
        }
    }
}
