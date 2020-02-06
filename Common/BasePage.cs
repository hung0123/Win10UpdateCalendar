using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Web1.Common
{
    abstract public class BasePage : Accudata.Base.BasePage
    {
        protected override bool IsAuthenticated()
        {
            //為了可以直連單一頁面先註解UserId的檢查機制
            //DataTable dt = Data.DataAccessAAS.QryUserData(UserID);
            //if (dt.Rows.Count == 0) return false;
            //EmpNo = dt.Rows[0]["emp_id"].ToString();
            //EmpName = dt.Rows[0]["USER_NAME"].ToString();
            return true;
        }

        /// <summary>
        /// 員工編號
        /// </summary>
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

        /// <summary>
        /// 員工姓名
        /// </summary>
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

        protected override void GenPermission()
        {

            for (int i = 0; i < Request.QueryString.Keys.Count; i++)
            {
                string sKey = Request.QueryString.GetKey(i);
                string[] sValue = Request.QueryString.GetValues(i);

                //add by 大雄 2013.12.04
                //如果QueryString類似這樣：http://localhost:1096/trans.aspx?Dept_ID=I130&Dept_Id2=I136&USER_ID=SMANO&FuncID=SQC.2.6&p_auth_level=1&R
                //那最後的那個R，會導致 sKey=null  sValue=R
                if (sKey == null && sValue != null)
                {
                    sKey = sValue[sValue.Length - 1];
                }

                Permission.Set(sKey, sValue[sValue.Length - 1]);
            }
        }

        /// <summary>
        /// 編碼
        /// </summary>
        /// <param name="value">編碼前字串</param>
        /// <returns>編碼後字串</returns>
        public override string EeCode(string value)
        {
            return value;
        }

        /// <summary>
        /// 解碼
        /// </summary>
        /// <param name="value">解碼前字串</param>
        /// <returns>解碼後字串</returns>
        public override string DeCode(string value)
        {
            return value;
        }
    }
}
