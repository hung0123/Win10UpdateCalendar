using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web1
{
    public partial class Site : System.Web.UI.MasterPage
    {
        /// <summary>
        /// 取得UID
        /// </summary>
        /// <returns></returns>
        public string getUID
        {
            get
            {
                return "Accudata.Base.BasePage." + Session.SessionID;
            }
        }
        /// <summary>
        /// UserID
        /// </summary>
        public string UserID
        {
            get
            {
                if (Session[getUID + ".UserID"] == null)
                {
                    Session[getUID + ".UserID"] = "";
                }
                return (string)Session[getUID + ".UserID"];
            }
            set
            {
                Session[getUID + ".UserID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
    }
}
