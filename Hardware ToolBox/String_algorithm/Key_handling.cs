using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardware_ToolBox.String_algorithm
{
    class Key_handling
    {
        /// <summary>
        /// 密钥补位
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Fill_in(string key,int sum)
        {
            while (key.Length < sum)
            {
                key += "0";
            }
            return key;
        }
    }
}
