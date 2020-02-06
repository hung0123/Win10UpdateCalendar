using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web1
{
    public partial class Home : Common.BasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected override string FuncID
        {
            get { return "m"; }
        }
    }
}
