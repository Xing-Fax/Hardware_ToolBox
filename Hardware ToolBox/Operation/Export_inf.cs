using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardware_ToolBox.Operation
{
    /// <summary>
    /// 对检测的信息进行文本导出
    /// </summary>
    public static class Export_inf
    {
        /// <summary>
        /// 对信息进行导出
        /// </summary>
        /// <param name="main">控件列表</param>
        /// <param name="file">导出文件路径</param>
        public static bool Configuration_report(this MainWindow main ,string file)
        {
            try
            {
                if (File.Exists(file))//如果出现同名文件则删除同名文件
                {
                    File.Delete(file);
                }

                string inf = "--------------------------------------配置信息摘要导出文件--------------------------------------\n\n";
                inf += "-----------软件版本：" + FileVersionInfo.GetVersionInfo(Process.GetCurrentProcess().MainModule.FileName).FileVersion + "\n\n";
                inf += "-----------导出时间：" + DateTime.Now.ToString() + "\n\n";
                inf += "---逻辑处理器-------------------------------------------------------------------------------------\n";
                inf += main.更多信息.其他信息1.Text + "\n";
                inf += "---主机板------------------------------------------------------------------------------------------\n";
                inf += main.更多信息.其他信息2.Text + "\n";
                inf += "---物理内存----------------------------------------------------------------------------------------\n";
                inf += main.更多信息.其他信息4.Text + "\n";
                inf += "---存储设备----------------------------------------------------------------------------------------\n";
                inf += main.更多信息.其他信息3.Text + "\n";
                inf += "---网络适配器--------------------------------------------------------------------------------------\n";
                inf += main.更多信息.其他信息5.Text + "\n";
                inf += "---BIOS--------------------------------------------------------------------------------------------\n";
                inf += main.更多信息.其他信息8.Text + "\n";
                inf += "---监视器-------------------------------------------------------------------------------------------\n";
                inf += main.更多信息.其他信息9.Text + "\n";
                inf += "---显示适配器--------------------------------------------------------------------------------------\n";
                inf += main.更多信息.其他信息10.Text + "\n";
                inf += "---操作系统----------------------------------------------------------------------------------------\n";
                inf += main.更多信息.其他信息9.Text + "\n";
                inf += "---逻辑分区----------------------------------------------------------------------------------------\n";
                inf += main.更多信息.其他信息6.Text + "\n";
                inf += "---音频适配器--------------------------------------------------------------------------------------\n";
                inf += main.更多信息.其他信息11.Text + "\n";
                inf += "----------------------------------------------------------------------------------------导出结束---\n";

                StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                sw.Write(inf);
                sw.Flush();
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
