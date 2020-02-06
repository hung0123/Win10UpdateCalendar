using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.SessionState;

namespace Web1
{
    public partial class trans : Web1.Common.BasePage
    {
        protected override string FuncID
        {
            get { return Request["FuncID"].ToString(); }
        }
        protected override void OnInit(EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("p3p", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //轉頁
            DataSet ds = new DataSet();
            ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\FuncList.xml");
            DataRow[] drs = ds.Tables["Func"].Select("FuncId='" + FuncID + "'");
            Response.Redirect("Page\\" + drs[0]["FuncPage"].ToString());
        }
    }
}
