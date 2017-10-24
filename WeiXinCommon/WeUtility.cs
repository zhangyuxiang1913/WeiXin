using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinCommon
{
   public class WeUtility
    {
       public static readonly string Token = "tokenadeeeegg33455";
       public static bool CheckSignature(string signature,string timestamp,string nonce)
       {
           if(string.IsNullOrWhiteSpace(signature)|| string.IsNullOrWhiteSpace(timestamp)|| string.IsNullOrWhiteSpace(nonce))
           {
               return false;
           }
            
          // string Token = WeConst.Token;
        //   string Token = "";
           string[] ArrTmp = { Token, timestamp, nonce };
           Array.Sort(ArrTmp);
           string tmpStr = string.Join("", ArrTmp);
           tmpStr = UtilityFunc.SHA1_Hash(tmpStr);//对该字符串进行sha1加密
           tmpStr = tmpStr.ToLower();
           //获得加密后的字符串可与signature对比
           //通过检验signature对请求进行校验，若正确，则原样返回echostr参数内容，接入生效，否则接入失败
           if(tmpStr==signature)
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
