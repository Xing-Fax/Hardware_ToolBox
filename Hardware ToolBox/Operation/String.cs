using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardware_ToolBox.Operation
{
    class String
    {
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="sourse">目标</param>
        /// <param name="startstr">从这里</param>
        /// <param name="endstr">到这里</param>
        /// <returns>返回不包含"从这里","到这里"的字符串</returns>
        public static string Substring(string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            int startindex, endindex;
            try
            {
                if (sourse == null)
                    return string.Empty;
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch { }
            return result;
        }

        /// <summary>
        /// 复制信息操作
        /// </summary>
        /// <param name="left">项目名称</param>
        /// <param name="right">内容</param>
        /// <returns>返回格式化后的字符串</returns>
        public static string Combine_strings(string[] left, string[] right)
        {
            string str = "";
            for (int i = 0; i < left.Length - 1; i++)
                str += left[i] + "：\t" + right[i];
            return str;
        }

        public static string caption(int sum)
        {
            switch (sum)
            {
                case 0:
                    return "基本信息：显示计算机当前基本配置信息和运行状态";
                case 1:
                    return "其他信息：提供了设备高级且全面的信息";
                case 2:
                    return "高级软件：为高级用户提供的软件和功能";
                case 3:
                    return "系统监视：监视计算机系统和各硬件当前运行状态";
                case 4:
                    return "其他工具：提供其他操作工具";
                case 5:
                    return "软件设置：修改软件设置和基本配置信息";
                case 6:
                    return "关于程序：显示对本程序的说明与提示";
                case 7:
                    return "";
                case 8:
                    return "";
                case 9:
                    return "";
            }
            return string.Empty;
        }
    }
}
