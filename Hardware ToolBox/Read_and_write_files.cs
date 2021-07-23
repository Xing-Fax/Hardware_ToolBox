using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using OpenHardwareMonitor.Hardware;
using System.Security.Cryptography.X509Certificates;
using static Hardware_ToolBox.App;
using System.Windows;

namespace Hardware_ToolBox.Detection_engine
{
    class Read_and_write_files
    {
        //写入ini文件↓
        public static  void IniWrite(string section, string key, string value, string path)
        {
            App.WritePrivateProfileString(section, key, value, path);
        }
        //读取ini文件↓
        public static  string IniReadValue(string section, string skey, string path)
        {
            StringBuilder temp = new StringBuilder(500);
            App.GetPrivateProfileString(section, skey, "", temp, 500, path);
            return temp.ToString();
        }

        static  UpdateVisitor updateVisitor = new UpdateVisitor();
        static  Computer myComputer = new Computer();

        public static void initialization()
        {
            myComputer.Open();
            //启动主板监测
            myComputer.MainboardEnabled = false;
            //启动CPU监测
            myComputer.CPUEnabled = true;
            //启动内存监测
            myComputer.RAMEnabled = false;
            //启动GPU监测
            myComputer.GPUEnabled = true;
            //启动风扇监测
            myComputer.FanControllerEnabled = false;
            //启动硬盘监测
            myComputer.HDDEnabled = false;
        }
        //读取电脑各项参数
        public static string[] device_status()
        {
            myComputer.Accept(updateVisitor);

            string[] parameter = new string[12];

            parameter[0] = App.Processor().ToString ();//得到cpu占用
            parameter[1] = Parameter_interception("GPU Core", "Load");//得到gpu占用
            parameter[2] = App.ram().ToString();//得到内存占用

            //parameter[3] = Parameter_interception("CPU", "000");
            parameter[4] = Parameter_interception("CPU Core #1", "Clock");
            parameter[5] = Parameter_interception("Bus Speed", "Clock");
            parameter[6] = Parameter_interception("温度", "000");

            //parameter[7] = Parameter_interception("GPU型号", "000");
            parameter[8] = Parameter_interception("GPU Core", "Clock");
            parameter[9] = Parameter_interception("GPU Memory", "Clock");
            parameter[10] = Parameter_interception("GPU Shader", "Clock");
            parameter[11] = Parameter_interception("GPU Core", "Temperature");

            return parameter;
        }

        private static string  Parameter_interception(string interception,string of)
        {
            foreach (var hardwareItem in myComputer.Hardware)
            {
                if (interception == hardwareItem.HardwareType.ToString())
                {
                    return hardwareItem.Name;
                }
                //if (interception == "GPU型号" && hardwareItem.HardwareType.ToString().StartsWith ("Gpu"))
                //{
                //    return hardwareItem.Name;
                //}
                foreach (var sensor in hardwareItem.Sensors)
                {
                    if (interception == sensor.Name && of == sensor.SensorType.ToString())
                    {
                        return sensor.Value.ToString();
                    }

                    if (interception == "温度" && sensor .Name .StartsWith("Core #1"))
                    {
                        return sensor.Value.ToString();
                    }
                }
            }
            return "暂无信息";
        }

        //执行cmd命令，并返回执行结果
        public static string RunCmd(string command)
        {
            //例Process
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";         //确定程序名
            p.StartInfo.Arguments = "/c " + command;   //确定程式命令行
            p.StartInfo.UseShellExecute = false;      //Shell的使用
            p.StartInfo.RedirectStandardInput = true;  //重定向输入
            p.StartInfo.RedirectStandardOutput = true; //重定向输出
            p.StartInfo.RedirectStandardError = true;  //重定向输出错误
            p.StartInfo.CreateNoWindow = true;        //设置置不显示示窗口
            p.Start();
            return p.StandardOutput.ReadToEnd();      //输出出流取得命令行结果果
        }


        public static bool Document_verification(string file)
        {
            try
            {
                X509Certificate cert = X509Certificate.CreateFromSignedFile(file);
                string f = cert.GetCertHashString();
                //MessageBox.Show(cert.GetCertHashString());
                if (f == "36A888B9F2A505BF92AC6B2796C2188E639AB1D1")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }

        //private static string model(string name)
        //{
        //    foreach (var hardwareItem in myComputer.Hardware)
        //    {
        //        if (hardwareItem.HardwareType.ToString () == name)
        //        {
        //            return hardwareItem.Name;
        //        }
        //        //str += hardwareItem.HardwareType + "型号是：" + hardwareItem.Name + "\n";
        //    }
        //    return "暂无信息";
        //}
        //public static float GetCpuUsage()
        //{
        //    var cpuCounter = new PerformanceCounter
        //    {
        //        CategoryName = "Processor",
        //        CounterName = "% Processor Time",
        //        InstanceName = "_Total"
        //    };
        //    return cpuCounter.NextValue();
        //}

        //public static int Disk_time()
        //{
        //    //PerformanceCounter diskRt = new PerformanceCounter("PhysicalDisk", "% Disk Write Time", "0 C:");
        //    //PerformanceCounter diskWt = new PerformanceCounter("PhysicalDisk", "% Disk Read Time", "0 C:");
        //    PerformanceCounter diskTt = new PerformanceCounter("PhysicalDisk", "% Disk Time", "0 C:");

        //    //while (true)
        //    //{
        //        //Thread.Sleep(1000); // wait for 1 second 
        //        //Console.WriteLine("% Disk Read Time =" + diskRt.NextValue() + " %.");
        //        //Console.WriteLine("% Disk Write Time = " + diskWt.NextValue() + " %.");
        //        //Console.WriteLine("% Disk Time = " + diskTt.NextValue() + " %.");
        //    //}
        //    return (int)diskTt.NextValue();
        //}
    }
}
