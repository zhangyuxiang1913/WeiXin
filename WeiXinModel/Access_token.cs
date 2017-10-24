using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinModel
{
    public class Access_token
    {
        public Access_token()
        {
            this.appid = "wxfaddf4773d85437a";
            this.app_secret = "1487456d2780d0d6f3d2b1131482fcd9";

            //  
            //TODO: 在此处添加构造函数逻辑  
            //  
        }
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 凭证有效时间，单位：秒  
        /// </summary>
        public string expires_in { get; set; }
        /// <summary>
        /// 开发者密码
        /// </summary>
        public string app_secret { get; set; }
        /// <summary>
        /// 开发者ID
        /// </summary>
        public string appid { get; set; }
        // string _access_token;
        // string _expires_in;
        // string _appid;
        // string _app_secret;


        // /// <summary>  
        // /// 获取到的凭证   
        // /// </summary>  
        // public string access_token
        // {
        //     get { return _access_token; }
        //     set { _access_token = value; }
        // }

        // /// <summary>  
        // /// 凭证有效时间，单位：秒  
        // /// </summary>  
        // public string expires_in
        // {
        //     get { return _expires_in; }
        //     set { _expires_in = value; }
        // }  
        //public string appid
        // {
        //     get { return _appid; }
        //     set { _appid = value; }
        // }
        //public string app_secret
        //{
        //    get { return _app_secret; }
        //    set { _app_secret = value; }
        //}
    }
}
