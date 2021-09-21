using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Hardware_ToolBox.Detection_Core
{
    /// <summary>
    /// 得到其他系统信息
    /// </summary>
    class Other_information
    {
        /// <summary>
        /// 获得总共物理内存
        /// </summary>
        /// <returns>返回Bytes大小</returns>
        public static long Memory_size()
        {
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["TotalPhysicalMemory"] != null)
                {
                    return long.Parse(mo["TotalPhysicalMemory"].ToString());
                }
            }
            return 0;
        }

        /// <summary>
        /// 获得可用物理内存
        /// </summary>
        /// <returns>返回Bytes大小</returns>
        public static long Memory_available()
        {
            long availablebytes = 0;
            try
            {
                ManagementClass mos = new ManagementClass("Win32_OperatingSystem");
                foreach (ManagementObject mo in mos.GetInstances())
                {
                    if (mo["FreePhysicalMemory"] != null)
                    {
                        availablebytes = 1024 * long.Parse(mo["FreePhysicalMemory"].ToString());
                        break;
                    }
                }
            }
            catch { }
            return availablebytes;
        }

        /// <summary>
        /// 获得进程、线程、句柄数量
        /// </summary>
        /// <returns>返回字符串/returns>
        public static string GetProcessCount()
        {
            Process[] processes = Process.GetProcesses();
            int count = processes.Count();
            int co1 = 0, co2 = 0;
            foreach (Process pro in processes)
            {
                co1 += pro.Threads.Count;
                co2 += pro.HandleCount;
            }
            return "process->" + count.ToString() + "<-\n" + "Thread->" + co1.ToString() + "<-\n" + "Handle->" + co2.ToString() + "<-";
        }

        /// <summary>
        /// 通过Windows性能计数器获取时间
        /// </summary>
        static readonly PerformanceCounter uptime = new PerformanceCounter("System", "System Up Time");
        /// <summary>
        /// 获得计算机从启动到到现在已运行的时间
        /// </summary>
        /// <returns>返回经过dd\:hh\:mm\:ss格式化后的字符串数据</returns>
        public static string Run_time()
        {
            uptime.NextValue();
            TimeSpan ts = TimeSpan.FromSeconds(uptime.NextValue());
            return ts.ToString(@"dd\:hh\:mm\:ss"); ;
        }
    }
}
