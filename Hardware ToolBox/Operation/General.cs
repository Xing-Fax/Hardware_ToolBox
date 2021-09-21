using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Hardware_ToolBox.Operation
{
    class General
    {
        [DllImport("kernel32")]        //读取ini操作函数
        public static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]        //写入ini操作函数
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// 写入ini文件操作
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="path">文件路径</param>
        public static void Write(string section, string key, string value, string path)
        {
            WritePrivateProfileString(section, key, value, path);
        }

        /// <summary>
        /// 读取ini文件操作
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="skey">键</param>
        /// <param name="path">ini文件路径</param>
        /// <returns>返回 节->键->值 中的值</returns>
        public static string Read_ini(string section, string skey, string path)
        {
            StringBuilder temp = new StringBuilder(500);
            GetPrivateProfileString(section, skey, "", temp, 500, path);
            return temp.ToString();
        }

        /// <summary>
        /// 执行CMD命令
        /// </summary>
        /// <param name="command">命令内容</param>
        /// <returns>返回执行后的输出结果</returns>
        public static string RunCmd(string command)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";           //确定程序名
            p.StartInfo.Arguments = "/c " + command;    //确定程式命令行
            p.StartInfo.UseShellExecute = false;        //Shell的使用
            p.StartInfo.RedirectStandardInput = true;   //重定向输入
            p.StartInfo.RedirectStandardOutput = true;  //重定向输出
            p.StartInfo.RedirectStandardError = true;   //重定向输出错误
            p.StartInfo.CreateNoWindow = true;          //设置置不显示示窗口
            p.Start();
            return p.StandardOutput.ReadToEnd();        //输出出流取得命令行结果果
        }

        /// <summary>
        /// 调用其他控制台程序
        /// </summary>
        /// <param name="file">程序路径</param>
        /// <param name="command">命令参数</param>
        /// <returns>返回执行后输出结果</returns>
        public static string Console(string file ,string command)
        {
            Process p = new Process();
            p.StartInfo.FileName = file;                //确定程序名
            p.StartInfo.Arguments = command;            //确定程式命令行
            p.StartInfo.UseShellExecute = false;        //Shell的使用
            p.StartInfo.RedirectStandardInput = true;   //重定向输入
            p.StartInfo.RedirectStandardOutput = true;  //重定向输出
            p.StartInfo.RedirectStandardError = true;   //重定向输出错误
            p.StartInfo.CreateNoWindow = true;          //设置置不显示示窗口
            p.Start();
            return p.StandardOutput.ReadToEnd();        //输出出流取得命令行结果果
        }

        /// <summary>
        /// 使用更改文件所有权的方式来删除文件
        /// 此方法想要管理员权限
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Force_delete(string path)
        {
            RunCmd("takeown /F " + "\"" + path + "\"" + " /A");                     //将文件所有者设为管理员组
            RunCmd("icacls " + "\"" + path + "\"" + " /grant Administrators:F");    //取得文件所有控制权
            RunCmd("del " + "\"" + path + "\"");                                    //删除文件
            if (!File.Exists(path))                                                 //判断是否删除成功
                return true;
            return false;
        }

        /// <summary>
        /// 大文件多次复制文件  true：复制成功   false：复制失败
        /// </summary>
        /// <param name="soucrePath">原始文件路径</param>
        /// <param name="targetPath">复制目标文件路径</param>
        /// <returns></returns>
        public static bool CopyFile(string soucrePath, string targetPath)
        {
            try
            {
                //读取复制文件流
                using (FileStream fsRead = new FileStream(soucrePath, FileMode.Open, FileAccess.Read))
                {
                    //写入文件复制流
                    using (FileStream fsWrite = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        byte[] buffer = new byte[1024 * 1024 * 2];                  //每次读取2M

                        while (true)                                                //可能文件比较大，要循环读取，每次读取2M
                        {
                            int n = fsRead.Read(buffer, 0, buffer.Count());         //每次读取的数据    n：是每次读取到的实际数据大小
                            if (n == 0)                                             //如果n=0说明读取的数据为空，已经读取到最后了，跳出循环
                                break;
                            fsWrite.Write(buffer, 0, n);                            //写入每次读取的实际数据大小
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
