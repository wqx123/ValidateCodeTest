using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// ValidateRegister 的摘要说明
    /// </summary>
    public class ValidateRegister : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string action = context.Request["action"];

            if (action == "CheckValidate")
            {
                string txtValidate = context.Request["txtValidate"];

                if (txtValidate == (context.Session["code"]).ToString())
                {
                    context.Response.Write("yes");
                }
                else
                {
                    context.Response.Write("no");
                }
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
}