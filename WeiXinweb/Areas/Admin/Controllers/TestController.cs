using Service.Common;
using System;
using System.Collections.Generic;
using System.IO;using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using WeiXinCommon;
using WeiXinModel;

namespace WeiXinweb.Areas.Area.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Area/Test/
        const string Token = "tokenadeeeegg33455";//你的token  
      public static LogHelper loghelper = new LogHelper();
     public static  string logFile = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["logpath"] + DateTime.Now.ToString("/yyyyMM") + "/" + "Access_Token" + System.DateTime.Now.ToString("yyyyMM") + ".txt");
      
        //var logFile = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["logpath"] + DateTime.Now.ToString("/yyyyMM") + "/" + "Access_Token" + System.DateTime.Now.ToString("yyyyMM") + ".txt");
        public ActionResult Index()
        {
           //// string echoStr = Request.QueryString["echoStr"].ToString();
           // string signature = Request.QueryString["signature"].ToString();
           // string timestamp = Request.QueryString["timestamp"].ToString();
           // string nonce = Request.QueryString["nonce"].ToString();
           // string postStr = "";
           // if (Request.HttpMethod.ToLower() == "post")
           // {
           //     System.IO.Stream s = System.Web.HttpContext.Current.Request.InputStream;
           //     byte[] b = new byte[s.Length];
           //     s.Read(b, 0, (int)s.Length);
           //     postStr = System.Text.Encoding.UTF8.GetString(b);
           //     if (!string.IsNullOrEmpty(postStr))
           //     {
           //         //ResponseMsg(postStr);  
           //       //  Response.Write(ResponseMsg(postStr));
           //         Response.End();
           //     }
           //   //  WriteLog("postStr:" + postStr);  
           // }
           // else
           // {
           //     TokenValidate tv = new TokenValidate();
           //    string ret= tv.Valid(echoStr, signature, timestamp, nonce);
           //     if(ret!="")
           //     {
           //         //日志文件
           //         var logFile = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["logpath"] + DateTime.Now.ToString("/yyyyMM") + "/" + System.DateTime.Now.ToString("yyyyMM") + ".txt");
           //         LogHelper logHelper = new LogHelper();
           //         logHelper.WriteLine(logFile, "echoStr:" + echoStr + ",signature" + signature + ",timestamp" + timestamp + ",nonce" + nonce);
           //         Response.Write(ret);
           //         Response.End();
           //     }
           // }
         //   var logFile = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["logpath"] + DateTime.Now.ToString("/yyyyMM") + "/" + "Access_Token" + System.DateTime.Now.ToString("yyyyMM") + ".txt");
            loghelper.WriteLine(logFile, "开始调用IsExistAccess_Token 方法"); 
            //Access_Token是否存在
            string Access_token=  IsExistAccess_Token();
          
       
          loghelper.WriteLine(logFile, "Access_Token:" + Access_token);
            return View();
        }
     
     
      

    
        ////返回字符串调用方法: 
        //public static HttpResponseMessage ToHttpMsgForWeChat(string strMsg) { HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(strMsg, Encoding.GetEncoding("UTF-8"), "application/x-www-form-urlencoded") }; return result; }


        public static Access_token GetAccess_token()
        {
            Access_token mode = new Access_token();
            string appid = mode.appid;
            string secret = mode.app_secret;
            loghelper.WriteLine(logFile,"appid:" + appid + "secret:" + secret);
            string strUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret;
            

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            loghelper.WriteLine(logFile, "请求URL：" + strUrl);
            req.Method = "GET";
            loghelper.WriteLine(logFile, "请求方式为：" + req.Method);
            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();
                loghelper.WriteLine(logFile, "myResponse");
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            
                string content = reader.ReadToEnd();
                loghelper.WriteLine(logFile, "content=" + content);
                //Response.Write(content);  
                //在这里对Access_token 赋值  
                Access_token token = new Access_token();
                token = JsonHelper.ParseFromJson<Access_token>(content);
                mode.access_token = token.access_token;
                mode.expires_in = token.expires_in;
                loghelper.WriteLine(logFile, "返回值为 access_token=" + mode.access_token + "expires_in=" + mode.expires_in);
            }
            return mode;
        }
        /// 根据当前日期 判断Access_Token 是否超期  如果超期返回新的Access_Token   否则返回之前的Access_Token  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static string IsExistAccess_Token()
        {

            string Token = string.Empty;
            DateTime YouXRQ;
           
            // 读取XML文件中的数据，并显示出来 ，注意文件路径  
            //string filepath = System.Web.HttpContext.Current.Server.MapPath("Access_Token.xml");
           string filepath= Path.Combine(HttpRuntime.AppDomainAppPath, "Access_Token.xml");
        //   var logFile = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["logpath"] + DateTime.Now.ToString("/yyyyMM") + "/" + "Access_Token" + System.DateTime.Now.ToString("yyyyMM") + ".txt");
           loghelper.WriteLine(logFile, "开始获取Access_Token.xml 路径:" + filepath);
            StreamReader str = new StreamReader(filepath, System.Text.Encoding.UTF8);
            loghelper.WriteLine(logFile, "Access_Token.xml 路径转UTF8:"+str);
            XmlDocument xml = new XmlDocument();
            xml.Load(str);
            str.Close();
            str.Dispose();
            Token = xml.SelectSingleNode("xml").SelectSingleNode("Access_Token").InnerText;
            loghelper.WriteLine(logFile, "token值是" + Token);
            YouXRQ = Convert.ToDateTime(xml.SelectSingleNode("xml").SelectSingleNode("Access_YouXRQ").InnerText);
            loghelper.WriteLine(logFile, "获取Access_YouXRQ值 " + YouXRQ);
            if (DateTime.Now > YouXRQ)
            {
                DateTime _youxrq = DateTime.Now;
                loghelper.WriteLine(logFile, "GetAccess_token 方法");
                Access_token mode = GetAccess_token();
                loghelper.WriteLine(logFile, "GetAccess_token 结束");

                xml.SelectSingleNode("xml").SelectSingleNode("Access_Token").InnerText = mode.access_token;
                loghelper.WriteLine(logFile, "Access_Token 值改变为" + mode.access_token);

                _youxrq = _youxrq.AddSeconds(int.Parse(mode.expires_in));
                xml.SelectSingleNode("xml").SelectSingleNode("Access_YouXRQ").InnerText = _youxrq.ToString();
                loghelper.WriteLine(logFile, "Tokend保存路径" + filepath);
                xml.Save(filepath);
                Token = mode.access_token;
            }
         
            return Token;
        }  
    }
}