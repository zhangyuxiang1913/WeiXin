using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
namespace Service.Common
{
   public class TokenValidate
    {
          const string Token = "tokenadeeeegg33455";//你的token  

          public string Valid(string echostr,string signature, string timestamp, string nonce)
          {
              bool checkVal = CheckSignature(signature, timestamp, nonce);
              if(checkVal)
              {
                  return echostr;
              }
              else
              {
                  return "";
              }
          }

        /// <summary>  
        /// 验证微信签名  
        /// </summary>  
        /// * 将token、timestamp、nonce三个参数进行字典序排序  
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密  
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。  
        /// <returns></returns>  
       private bool CheckSignature(string signature, string timestamp, string nonce)
        {
           // string signature = Request.QueryString["signature"].ToString();
           //string timestamp = Request.QueryString["timestamp"].ToString();
           // string nonce = Request.QueryString["nonce"].ToString();
            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序  
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
