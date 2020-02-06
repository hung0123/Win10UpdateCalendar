using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Data;
using Newtonsoft.Json;

namespace Web1
{
    /// <summary>
    /// Main 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
    [System.Web.Script.Services.ScriptService]
    public class Main : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string GetData()
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(Server.MapPath("~/XML/Manage.xml"));

            XmlNodeList settingNode = doc.SelectNodes("/Root/Setting");

            string s = settingNode[0].ChildNodes[0].InnerText;//起始時間
            string e = settingNode[0].ChildNodes[1].InnerText;//結束時間

            DateTime sT = DateTime.Parse(s);
            DateTime eT = DateTime.Parse(e);
            TimeSpan ts = eT - sT;

            DataSet ds = new DataSet();
            DataTable dt = null;
            int _month = 0;

            for (int i = 0; i <= ts.Days; i++)
            {

                if (sT.AddDays(i).Month != _month || i == 0)//換月or第一個月
                {
                    dt = new DataTable();
                    dt.Columns.Add("Day");
                    dt.Columns.Add("People1");//第一個場次
                    dt.Columns.Add("Total1");//第一個場次
                    dt.Columns.Add("People2");//第二個場次
                    dt.Columns.Add("Total2");//第二個場次


                    dt.TableName = sT.AddDays(i).ToString("yyyyMM");
                    ds.Tables.Add(dt);
                }

                DataRow dr = dt.NewRow();
                string day = sT.AddDays(i).ToString("yyyyMMdd");
                dr["Day"] = sT.AddDays(i).ToString("yyyy-MM-dd");

                XmlNodeList dayNodeF = doc.SelectNodes(string.Format("/Root/Detail/D{0}/First", day));
                XmlNodeList dayNodeS = doc.SelectNodes(string.Format("/Root/Detail/D{0}/Second", day));


                dr["People1"] = dayNodeF[0].Attributes["People"].Value;
                dr["Total1"] = dayNodeF[0].Attributes["Total"].Value;

                dr["People2"] = dayNodeS[0].Attributes["People"].Value;
                dr["Total2"] = dayNodeS[0].Attributes["Total"].Value;

                dt.Rows.Add(dr);

                _month = sT.AddDays(i).Month;


            }
            return JsonConvert.SerializeObject(ds);
             

        }

        [WebMethod]
        public string SaveData(string time, string org,string name,string phone)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(Server.MapPath("~/XML/Manage.xml"));


            lock (doc) 
            {
                XmlNodeList dayNode = null;
                if (time.IndexOf("早上") > 0)
                {
                    dayNode = doc.SelectNodes(string.Format("/Root/Detail/D{0}/First", time.Substring(0, 10).Replace("-","")));
                }
                else
                {
                    dayNode = doc.SelectNodes(string.Format("/Root/Detail/D{0}/Second", time.Substring(0, 10).Replace("-", "")));
                }

                string repeatResult = CheckRepeat(org,name,phone,doc);
                if (repeatResult!= "OK")
                {
                    return "重複登記，您已登記" + repeatResult.Substring(0, 4) + "/" + repeatResult.Substring(4, 2) +"/"+repeatResult.Substring(6);
                }



                XmlElement ele = doc.CreateElement("User");
                ele.SetAttribute("org", org);
                ele.SetAttribute("name", name);
                ele.SetAttribute("phone", phone);

                dayNode[0].AppendChild(ele);

                int originPeo = int.Parse(dayNode[0].Attributes["People"].Value.ToString());
                int totalPeo = int.Parse(dayNode[0].Attributes["Total"].Value.ToString());
                dayNode[0].Attributes["People"].Value = (originPeo+1).ToString();



                if (originPeo + 1 > totalPeo)
                {
                    return "Fail";
                }
                else
                {
                    doc.Save(Server.MapPath("~/XML/Manage.xml"));
                    return "Success";
                }
            }

            
        }

        private string CheckRepeat(string org,string name,string phone,XmlDocument doc)
        {
            var user = doc.GetElementsByTagName("User");
            foreach (XmlNode item in user)
            {
                if (item.Attributes["org"].Value==org && item.Attributes["name"].Value == name && item.Attributes["phone"].Value == phone)
                {
                    string g = item.ParentNode.Name == "First" ? "早上" : "下午";
                    string d = item.ParentNode.ParentNode.Name.Substring(1);

                    return d+g;
                }
                
            }
            return "OK";  
        }
    }
}
