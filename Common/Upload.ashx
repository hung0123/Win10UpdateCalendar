<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;
using System.IO;
using System.Web.SessionState;

public class Upload : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            HttpFileCollection files = context.Request.Files;
            string msg = string.Empty;
            string error = string.Empty;

            if (files.Count > 0)
            {
                string tempPath = System.Configuration.ConfigurationManager.AppSettings["UploadPath"];
                string fileName = System.IO.Path.GetFileName(files[0].FileName); //實際檔名 
                string tmpFileName = string.Format("{0}_{1}", System.Web.HttpContext.Current.Session.SessionID, fileName);   //上傳檔名

                //上傳檔案
                files[0].SaveAs(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(tempPath), tmpFileName));

                //檔案上傳失敗
                if (!System.IO.File.Exists(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(tempPath), tmpFileName)))
                {
                    msg = "檔案上傳失敗。";
                    context.Response.Write("{ error:'Error', msg:'" + msg + "'}");
                }
                else
                {
                    msg = fileName + "@@" + tmpFileName;
                    context.Response.Write("{ error:'OK', msg:'" + msg + "'}");
                }
            }
            else
            {
                context.Response.Write("{ error:'OK', msg:'" + msg + "'}");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write("{ error:'Error', msg:'" + ex.Message + "',e:'null'}");
        }
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
