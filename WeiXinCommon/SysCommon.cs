using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinCommon
{
   public class SysCommon
    {
       #region 写数据库日志
       public static void WriteDBLog(string content, string pgurl, string ip = "127.0.0.1")
       {
           content = content.Replace("'", "''");
           pgurl = pgurl.Replace("'", "''");
           ip = ip.Replace("'", "''");
           string sql = string.Format("Insert into S_Log(PageUrl,Content,IP,RegTime) Values('{0}','{1}','{2}',getdate())", pgurl, content, ip);
           //dbContext.pager.Execute(sql);
       }

       #endregion
    }
}
