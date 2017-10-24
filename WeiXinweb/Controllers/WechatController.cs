using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiXinCommon;

namespace WeiXinweb.Controllers
{
    public class WechatController : Controller
    {

        //
        // GET: /Wechat/
        public ActionResult Index()
        {
             Service();
            return View();
        }
        public void Action()
        {

        }
        public void Service()
        {
            Vaild();
        }

        public void Vaild()
        {
            string echoStr = Request.QueryString["echoStr"].Replace("',", "''");
            if (string.IsNullOrWhiteSpace(echoStr))
            {
                return;
            }
            string signature = Request.QueryString["signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            if(WeUtility.CheckSignature(signature,timestamp,nonce))
            {
                Response.Write(echoStr);
                Response.End();
            }
        }

    }
}