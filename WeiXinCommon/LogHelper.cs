using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinCommon
{
   public class LogHelper
    {
       private bool IsOpenLog = true;
        /// <summary>
        /// 追加一行信息
        /// </summary>
        /// <param name="logFile"></param>
        /// <param name="text"></param>
        public void WriteLine(string logFile, string text)
        {
            try
            {
                if (!IsOpenLog) return;
                text += "\r\n";
                string logpath = logFile.Substring(0, logFile.LastIndexOf('\\'));
                if (!Directory.Exists(logpath))
                    Directory.CreateDirectory(logpath);

                if (!File.Exists(logFile))
                    File.Create(logFile).Dispose();

                using (StreamWriter sw = new StreamWriter(logFile, true, Encoding.UTF8))
                {
                    sw.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + text);
                }
            }
            catch (Exception ex)
            {
                SysCommon.WriteDBLog(ex.ToString(), "WriteLine");
            }
        }
    }
}
