using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
   public class CommonMethods
    {
       public static string encrypt(string x)
       {
           System.Security.Cryptography.MD5CryptoServiceProvider test123 = new System.Security.Cryptography.MD5CryptoServiceProvider();
           byte[] data = System.Text.Encoding.ASCII.GetBytes(x);
           data = test123.ComputeHash(data);
           String md5Hash = System.Text.Encoding.ASCII.GetString(data);

           return md5Hash;
       }
     
       public static string CreateMD5(string input)
       {
           // Use input string to calculate MD5 hash
           MD5 md5 = System.Security.Cryptography.MD5.Create();
           byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
           byte[] hashBytes = md5.ComputeHash(inputBytes);

           // Convert the byte array to hexadecimal string
           StringBuilder sb = new StringBuilder();
           for (int i = 0; i < hashBytes.Length; i++)
           {
               sb.Append(hashBytes[i].ToString("X2"));
           }
           return sb.ToString();
       }
    }
}
