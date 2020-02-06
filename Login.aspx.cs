using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web1
{
    public partial class Login : System.Web.UI.Page
    {
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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtAcc.Text == "admin" && txtPsw.Text == "20200109")
            {
                UserID = "123";
                Session["isLogin"] = "Y";
                Response.Redirect(@"Page\Manage.aspx");
            }
            else
            {
                Response.Redirect(@"Page\Order.aspx");
            }
        }
    }
}