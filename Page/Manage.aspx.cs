using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Data;
using Web1.Common.Excel;
using Syncfusion.XlsIO;

namespace Web1.Page
{
    public partial class Manage : Common.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterPostBackControl(btnExport);
        }
        protected void btnSaveShow_Click(object sender, EventArgs e)
        {
            ShowQuestion("確認後將清除目前資料",string.Format("$('#{0}').click()",btnSave.ClientID),"");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            


            XmlDocument doc = new XmlDocument();

            doc.Load(Server.MapPath("~/XML/Manage.xml"));

            XmlNodeList settingNode = doc.SelectNodes("/Root/Setting");
            XmlNodeList detailNode = doc.SelectNodes("/Root/Detail");


            string timeS = txtSTIME.Text;
            string timeE = txtETIME.Text;
            string limit = txtLimit.Text == "" ? "0" : txtLimit.Text;

            if (timeE == "" || timeS == "")
            {
                System.Threading.Thread.Sleep(500);
                ShowMessage("請輸入時間");
                return;
            }
            if (limit == "0")
            {
                System.Threading.Thread.Sleep(500);
                ShowMessage("請輸入限制人數");
                return;
            }



            settingNode[0].ChildNodes[0].InnerText = timeS;//起始時間
            settingNode[0].ChildNodes[1].InnerText = timeE;//結束時間
            // loop through all AID nodes

            DateTime sT = DateTime.Parse(timeS);
            DateTime eT = DateTime.Parse(timeE);

            if (detailNode[0].ChildNodes.Count != 0)
            {
                //備份

                doc.Save(Server.MapPath(string.Format("~/XML/Backup/Manage{0}.xml",System.DateTime.Now.ToString("yyyyMMddHHmmss"))));
                detailNode[0].RemoveAll();
            }


            TimeSpan ts = eT - sT ;

            for(int i=0;i<=ts.Days;i++)
            {
                DateTime res = sT.AddDays(i);

                XmlElement eleChild1 = doc.CreateElement("D"+res.ToString("yyyyMMdd"));

                


                XmlElement eleFirst = doc.CreateElement("First");
                if (rbN.Checked && (res.DayOfWeek == DayOfWeek.Saturday || res.DayOfWeek == DayOfWeek.Sunday))
                {
                    eleFirst.SetAttribute("People", "0");
                    eleFirst.SetAttribute("Total", "-1");
                }
                else
                {
                    eleFirst.SetAttribute("People", "0");
                    eleFirst.SetAttribute("Total", limit);
                }


                XmlElement eleSecond = doc.CreateElement("Second");
                if (rbN.Checked && (res.DayOfWeek == DayOfWeek.Saturday || res.DayOfWeek == DayOfWeek.Sunday))
                {

                    eleSecond.SetAttribute("People", "0");
                    eleSecond.SetAttribute("Total", "-1");
                }
                else
                {
                    eleSecond.SetAttribute("People", "0");
                    eleSecond.SetAttribute("Total", limit);
                }

                eleChild1.AppendChild(eleFirst);
                eleChild1.AppendChild(eleSecond);

                detailNode[0].AppendChild(eleChild1);

            }
            // save the XmlDocument back to disk
            doc.Save(Server.MapPath("~/XML/Manage.xml"));

            doc = null;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(Server.MapPath("~/XML/Manage.xml"));


            DataTable dt = new DataTable();
            dt.Columns.Add("Day");
            dt.Columns.Add("Order");//早上場或下午場
            dt.Columns.Add("People");//場次報名人數
            dt.Columns.Add("Total");//場次總人數
            dt.Columns.Add("PeoSEQ");//姓名
            dt.Columns.Add("Name");//姓名
            dt.Columns.Add("Phone");//分機
            dt.Columns.Add("Organ");//部門

            XmlNodeList detailNode = doc.SelectNodes("/Root/Detail");

            foreach (XmlNode items in detailNode[0].ChildNodes)
            {
                
                foreach (XmlNode item in items.ChildNodes)
                {
                    DataRow dr = dt.NewRow();
                    dr["Day"] = items.Name.Substring(1);
                    dr["Order"] = item.Name == "First"?"早上":"下午";
                    dr["People"] = item.Attributes["People"].Value;
                    dr["Total"] = item.Attributes["Total"].Value;
                    dt.Rows.Add(dr);
                    if (item.HasChildNodes)
                    {
                        int seq = 1;
                        foreach (XmlNode user in item.ChildNodes)
                        {
                            DataRow drDetail = dt.NewRow();
                            drDetail["Day"] = items.Name.Substring(1);
                            drDetail["Order"] = item.Name == "First" ? "早上" : "下午";
                            drDetail["People"] = item.Attributes["People"].Value;
                            drDetail["Total"] = item.Attributes["Total"].Value;
                            drDetail["PeoSEQ"] = seq;
                            drDetail["Name"] = user.Attributes["name"].Value;
                            drDetail["Phone"] = user.Attributes["phone"].Value;
                            drDetail["Organ"] = user.Attributes["org"].Value;
                            dt.Rows.Add(drDetail);
                            seq++;
                        }
                    }
                    
                }

            }

            DownLoadExcel(dt);
        }

        private void DownLoadExcel(DataTable dt)
        {
             
            ExcelEngine excelEngine = new ExcelEngine()
            {
                ThrowNotSavedOnDestroy = false
            };
            IApplication application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Excel2013;

            IWorkbook workbook = excelEngine.Excel.Workbooks.Open(Server.MapPath("~/Content/Template/Template.xlsx"));
            
            IWorksheet sheet = workbook.Worksheets[0];

            sheet.ImportDataTable(dt, false, 2, 1);

            sheet.Unprotect("");
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.SaveAs(ms);
                ExportFile(ms.ToArray(), "Result.xlsx");
            }
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

            res.Flush();
            res.Close();
        }

        protected override string FuncID
        {
            get { return "m"; }
        }
    }
}
