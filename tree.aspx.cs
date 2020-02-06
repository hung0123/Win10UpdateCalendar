using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Web1
{
    public partial class tree : System.Web.UI.Page
    {
        List<string> Scripts = new List<string>();

        #region Accudata.Base.BasePage
        public string EmpNo
        {
            get
            {
                if (Session["BasePage_EmpNo"] == null)
                {
                    Session["BasePage_EmpNo"] = "";
                }
                return (string)Session["BasePage_EmpNo"];
            }
            set
            {
                Session["BasePage_EmpNo"] = value;
            }
        }
        public string EmpName
        {
            get
            {
                if (Session["BasePage_EmpName"] == null)
                {
                    Session["BasePage_EmpName"] = "";
                }
                return (string)Session["BasePage_EmpName"];
            }
            set
            {
                Session["BasePage_EmpName"] = value;
            }
        }

        /// <summary>
        /// Accudata.Base.BasePage Method
        /// </summary>
        protected string getUID
        {
            get
            {
                return "Accudata.Base.BasePage." + Session.SessionID;
            }
        }
        /// <summary>
        /// Accudata.Base.BasePage UserID
        /// </summary>
        protected string UserID
        {
            set
            {
                Session[getUID + ".UserID"] = value;
            }
        }
        /// <summary>
        /// Accudata.Base.BasePage FuncID
        /// </summary>
        private string FuncID
        {
            set
            {
                Session[getUID + ".FuncID"] = value;
            }
        }
        /// <summary>
        /// Accudata.Base.BasePage Permission
        /// </summary>
        public Accudata.Utility.Permission Permission
        {
            get
            {
                if (Session[getUID + ".Permission"] == null)
                {
                    Session[getUID + ".Permission"] = new Accudata.Utility.Permission();
                }
                return (Accudata.Utility.Permission)Session[getUID + ".Permission"];
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFunc();
                BindUser();
                BindParam();
            }
        }
        protected override void Render(HtmlTextWriter writer)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Scripts", string.Join(";", Scripts.ToArray()), true);
            base.Render(writer);
        }
        protected void drpFunc_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindParam();
        }
        protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindParam();
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            DataSet ds;
            DataRow[] drs;

            //登入角色
            UserID = drpUser.SelectedValue;
            FuncID = drpFunc.SelectedValue;

            //DataTable dt = WebSST.Common.Data.DataAccessAAS.QryUserData(drpUser.SelectedValue);
            //if (dt.Rows.Count != 0)
            //{
            //    EmpNo = dt.Rows[0]["emp_id"].ToString();
            //    EmpName = dt.Rows[0]["USER_NAME"].ToString();
            //}
            //EmpNo = "123";
            //EmpName = "test";
            //設定權限

            List<string> li = new List<string>();

            li.Add("User_ID=" + drpUser.SelectedValue);
            li.Add("FuncID=" + drpFunc.SelectedValue);

            foreach (GridViewRow gvR in gvParam.Rows)
            {
                Label lb = (Label)gvR.Cells[0].FindControl("lbParamID");
                TextBox tx = (TextBox)gvR.Cells[0].FindControl("txtParamValue");

                if (lb != null && tx != null && lb.Text != "")
                {
                    string Name = lb.Text;
                    string Value = tx.Text;
                    li.Add(string.Format("{0}={1}", Name, Value));

                    string CkName = drpFunc.SelectedValue + "$$" + drpUser.SelectedValue + "$$" + Name;
                    Response.Cookies[CkName].Value = Value;
                    Response.Cookies[CkName].Expires = DateTime.Now.AddDays(365);
                }
            }

            Response.Cookies["FuncID"].Value = drpFunc.SelectedValue;
            Response.Cookies["FuncID"].Expires = DateTime.Now.AddDays(365);

            Response.Cookies[drpFunc.SelectedValue + "_Open"].Value = ckOpenWindow.Checked ? "T" : "F";
            Response.Cookies[drpFunc.SelectedValue + "_Open"].Expires = DateTime.Now.AddDays(365);

            //轉頁
            ds = new DataSet();
            ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\FuncList.xml");
            drs = ds.Tables["Func"].Select("FuncId='" + drpFunc.SelectedValue + "'");
            if (ckOpenWindow.Checked)
            {
                Scripts.Add("window.open('trans.aspx?" + string.Join("&", li.ToArray()) + "')");
            }
            else
            {
                Scripts.Add("window.top.frames['content'].location='trans.aspx?" + string.Join("&", li.ToArray()) + "'");
            }
        }
        private void BindFunc()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\FuncList.xml");
            DataTable dsTables = ds.Tables["Func"];
            dsTables.DefaultView.Sort = "FuncId";
            drpFunc.DataSource = dsTables.DefaultView.ToTable(); ;
            drpFunc.DataValueField = "FuncId";
            drpFunc.DataTextField = "FuncName";
            drpFunc.DataBind();

            if (Request.Cookies["FuncID"] != null)
            {
                drpFunc.SelectedValue = Request.Cookies["FuncID"].Value;
            }
        }
        private void BindUser()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\UserList.xml");

            drpUser.DataSource = ds.Tables["User"];
            drpUser.DataValueField = "UserID";
            drpUser.DataTextField = "UserDesc";
            drpUser.DataBind();
        }
        private void BindParam()
        {
            string funcId = drpFunc.SelectedValue;
            string UserID = drpUser.SelectedValue;
            DataSet ds = new DataSet();
            ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\FuncList.xml");

            DataTable dt = ds.Tables["Param"];
            dt.DefaultView.RowFilter = "FuncId='" + funcId + "'";
            DataTable dtDefaultViewToTable = dt.DefaultView.ToTable();

            //從User帶入變數值
            ds = new DataSet();
            ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\UserList.xml");
            DataRow[] drs = ds.Tables["User"].Select("UserID='" + drpUser.SelectedValue + "'");
            if (drs.Length != 0)
            {
                foreach (DataRow dr in dtDefaultViewToTable.Rows)
                {
                    if (drs[0].Table.Columns.Contains(dr["ParamID"].ToString()))
                    {
                        dr["ParamDefault"] = drs[0][dr["ParamID"].ToString()].ToString();
                    }
                }
            }


            //從Cookies帶入變數值
            foreach (DataRow dr in dtDefaultViewToTable.Rows)
            {
                string CkName = funcId + "$$" + UserID + "$$" + dr["ParamID"].ToString();
                if (Request.Cookies[CkName] != null)
                {
                    dr["ParamDefault"] = Request.Cookies[CkName].Value;
                }
            }

            gvParam.DataSource = dtDefaultViewToTable;
            gvParam.DataBind();
            upParam.Update();

            //從Cookies帶入變數值(OpenWindow)
            if (Request.Cookies[funcId + "_Open"] != null)
            {
                ckOpenWindow.Checked = Request.Cookies[funcId + "_Open"].Value.Equals("T");
            }
            else
            {
                ckOpenWindow.Checked = false;
            }

        }
    }
}
