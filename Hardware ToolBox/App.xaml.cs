using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OpenHardwareMonitor.Hardware;
namespace Hardware_ToolBox
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //用于获取基本配置信息
        [DllImport("Hardware detection.dll")]
        public static extern string Hwinfo(string Configuration_file_path, string Run_directory);
        //读ini文件
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);
        //写ini文件
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        //读取内存信息写入到给定的ini文件中
        [DllImport("System memory information.dll")]
        public static extern void Memory_information();
        //得到cpu使用率
        [DllImport("Processor occupied.dll")]
        public static extern int Processor();
        //得到内存使用率
        [DllImport("Processor occupied.dll")]
        public static extern int ram();

        //不知道为什么要这样写，但写上可用运行是真的
        public class UpdateVisitor : IVisitor
        {
            public void VisitComputer(IComputer computer)
            {
                computer.Traverse(this);
            }
            public void VisitHardware(IHardware hardware)
            {
                hardware.Update();
                foreach (IHardware subHardware in hardware.SubHardware)
                    subHardware.Accept(this);
            }
            public void VisitSensor(ISensor sensor) { }
            public void VisitParameter(IParameter parameter) { }
        }
        //public App()
        //{

        //    /**
        //     * 当前用户是管理员的时候，直接启动应用程序
        //     * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
        //     */
        //    //获得当前登录的Windows用户标示
        //    WindowsIdentity identity = WindowsIdentity.GetCurrent();
        //    WindowsPrincipal principal = new WindowsPrincipal(identity);
        //    //判断当前登录用户是否为管理员
        //    if (principal.IsInRole(WindowsBuiltInRole.Administrator))
        //    {
        //        //如果是管理员，则直接运行
        //        Run(new MainWindow());
        //    }
        //    else
        //    {
        //        //创建启动对象
        //        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        //        startInfo.UseShellExecute = true;
        //        startInfo.WorkingDirectory = Environment.CurrentDirectory;
        //        startInfo.FileName = Assembly.GetExecutingAssembly().Location;
        //        //设置启动动作,确保以管理员身份运行
        //        startInfo.Verb = "runas";
        //        try
        //        {
        //            System.Diagnostics.Process.Start(startInfo);
        //        }
        //        catch
        //        {
        //            return;
        //        }
        //        //退出
        //        //Application.Current.Shutdown();

        //    }
        //}


    }
}
//string[] str = new string[2];
//str[0] = "0";
//str[1] = "1";
//str[2] = "2";


//string[] str = new string[2];
//str[0] = ((string[])e.Argument)[0];
//str[1] = ((string[])e.Argument)[1];