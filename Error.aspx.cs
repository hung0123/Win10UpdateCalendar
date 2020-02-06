using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web1
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbUserID.Text = Session["Accudata.Base.BasePage." + Session.SessionID + ".UserID"].ToString();
            lbErrTime.Text = Session["Accudata.Base.BasePage." + Session.SessionID + ".ErrorTime"].ToString();
        }
    }
}
