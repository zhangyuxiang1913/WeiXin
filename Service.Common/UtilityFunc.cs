﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
   public class UtilityFunc
    {
       /// <summary>
       ///  字符串转换SHA1
       /// </summary>
       /// <param name="str_sha1_in"></param>
       /// <returns></returns>
       public static string SHA1_Hash(string str_sha1_in)
       {
           SHA1 sha1 = new SHA1CryptoServiceProvider();
           byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(str_sha1_in);
           byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
           string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
           str_sha1_out = str_sha1_out.Replace("-", "");
           return str_sha1_out;
       }

    }
}
