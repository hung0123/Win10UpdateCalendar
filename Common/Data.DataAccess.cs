using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Text;
using Accudata.Data.Agent.DataGateway;

namespace Web1.Common.Data
{
    public class DataAccess
    {

        private static Accudata.Data.Agent.DataGateway.Module m;
        private const string sSystemId = "Template"; //請填入系統名稱(例:WebAR 填AR)
        private const string sModuleId = "COMMON";

        /// <summary>
        /// LoadModule
        /// </summary>
        private static void LoadModule()
        {
            try
            {
                if (m == null)
                {
                    m = new Accudata.Data.Agent.DataGateway.Module(sSystemId, sModuleId);
                }
            }
            catch (System.Exception ex)
            {
                m = null;
                throw ex;
            }
        }


    }

    public class DataAccessAAS
    {
        private static Accudata.Data.Agent.DataGateway.Module m;
        private const string sSystemId = "Template"; //請填入系統名稱(例:WebAR 填AR)
        private const string sModuleId = "COMMON_AAS";

        /// <summary>
        /// 使用者查詢
        /// </summary>
        /// <param name="UserId">使用者帳號</param>
        /// <returns>USER_ID, EMP_ID</returns>
        public static DataTable QryUserData(string UserID)
        {
            return QI_USER_QUERY(UserID);
        }

        /// <summary>
        /// 使用者查詢
        /// </summary>
        /// <returns></returns>
        private static DataTable QI_USER_QUERY(string USER_ID)
        {

            Module m = null;
            try
            {
                m = new Module(sSystemId, sModuleId);

                string strQI_ID = "QI_USER_QUERY";
                m.DAQueryInstanceDictionary[strQI_ID].DAQueryParameterDictionary.Clear();
                m.DAQueryInstanceDictionary[strQI_ID].DAQueryParameterDictionary.Add("USER_ID", USER_ID);

                return m.LoadQueryData(strQI_ID);
            }
            catch (System.Exception ex)
            {
                m = null;
                throw ex;
            }
        }
    }
}
