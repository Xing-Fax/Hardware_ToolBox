using Hardware_ToolBox.Control_class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Hardware_ToolBox.Operation
{

    /// <summary>
    /// 得到系统全部信息
    /// </summary>
    public static class All_Hardware
    {
        /// <summary>
        /// 定义变量存储查询结果
        /// </summary>
        static string processor = null, Motherboard = null, RAM = null, Network = null, Disk = null,
            BIOS = null, Monitor = null, Graphics = null, system = null, logic = null, sound = null;
        private static void Read_information(object sender, DoWorkEventArgs e)
        {
            try
            {
                ManagementClass a = new ManagementClass("Win32_Processor");             //处理器信息
                ManagementClass b = new ManagementClass("Win32_BaseBoard");             //主板信息
                ManagementClass c = new ManagementClass("Win32_PhysicalMemory");        //内存信息
                ManagementClass d = new ManagementClass("Win32_NetworkAdapter");        //网卡信息
                ManagementClass e1 = new ManagementClass("Win32_DiskDrive");            //磁盘信息
                ManagementClass f = new ManagementClass("Win32_BIOS");                  //BIOS信息
                ManagementClass g = new ManagementClass("Win32_DesktopMonitor");        //监视器信息
                ManagementClass h1 = new ManagementClass("Win32_DisplayConfiguration"); //显卡信息 1
                ManagementClass h2 = new ManagementClass("Win32_VideoController");      //显卡信息 2
                ManagementClass j = new ManagementClass("Win32_OperatingSystem");       //系统信息
                ManagementClass k = new ManagementClass("Win32_LogicalDisk");           //逻辑磁盘信息
                ManagementClass l = new ManagementClass("Win32_SoundDevice");           //声卡信息

                int temp = 0;
                foreach (ManagementObject o in a.GetInstances())                        //处理器
                {
                    temp++; if (temp >= 2) { processor += "\n\n"; }
                    processor += "CPU名称：\t" + o["name"] + "\n";
                    processor += "CPU编号：\t" + o["ProcessorId"] + "\n";
                    processor += "CPU版本：\t" + o["Version"] + "\n";
                    processor += "CPU位宽：\t" + o["AddressWidth"] + " Bit\n";
                    processor += "CPU主频：\t" + o["CurrentClockSpeed"] + " MHz\n";
                    processor += "CPU外频：\t" + o["ExtClock"] + " MHz\n";
                    processor += "CPU接口：\t" + o["SocketDesignation"] + "\n";
                    processor += "CPU制造商：\t" + o["Manufacturer"] + "\n";
                    processor += "CPU核心数：\t" + o["NumberOfCores"] + " 个逻辑核心\n";
                    processor += "CPU线程数：\t" + o["NumberOfLogicalProcessors"] + " 个逻辑线程\n";
                    processor += "CPU L2缓存：\t" + o["L2CacheSize"] + " KB\n";
                    processor += "CPU L3缓存：\t" + o["L3CacheSize"] + " KB\n";
                    try { processor += "CPU电压：\t" + (float.Parse(o["CurrentVoltage"].ToString()) / 10) + " V\n"; } catch { }
                    processor += "虚拟化支持：\t" + o["VirtualizationFirmwareEnabled"] + "\n";
                    processor += "Hyper-V支持：\t" + o["VMMonitorModeExtensions"];
                }

                temp = 0;
                foreach (ManagementObject o in b.GetInstances())                         //主板
                {
                    temp++; if (temp >= 2) { Motherboard += "\n\n"; }
                    Motherboard += "主板型号：\t" + o["Product"] + "\n";
                    Motherboard += "主板制造商：\t" + o["Manufacturer"] + "\n";
                    Motherboard += "主机板：\t" + o["HostingBoard"] + "\n";
                    Motherboard += "热拔插支持：\t" + o["HotSwappable"];
                }
                temp = 0;
                foreach (ManagementObject o in c.GetInstances())                         //内存
                {
                    temp++; if (temp >= 2) { RAM += "\n\n"; }
                    RAM += "内存编号：\t" + o["DeviceLocator"] + "\n";
                    RAM += "内存类型：\t" + o["Description"] + "\n";
                    RAM += "内存位宽：\t" + o["DataWidth"] + " Bit\n";
                    RAM += "内存标签：\t" + o["BankLabel"] + "\n";
                    try { RAM += "内存容量：\t" + (long.Parse(o["Capacity"].ToString()) / 1000000) + " MB\n"; } catch { }
                    RAM += "内存频率：\t" + o["Speed"] + " MHz\n";
                    RAM += "生产厂商：\t" + o["Manufacturer"];
                }
                temp = 0;
                foreach (ManagementObject o in d.GetInstances())                         //网卡
                {
                    if (!o["Manufacturer"].ToString().Contains("Microsoft"))
                    {
                        temp++; if (temp >= 2) { Network += "\n\n"; }
                        Network += "网卡名称：\t" + o["Name"] + "\n";
                        Network += "网卡类型：\t" + o["AdapterType"] + "\n";
                        Network += "网卡编号：\t" + o["DeviceID"] + "\n";
                        Network += "物理设备：\t" + o["PhysicalAdapter"] + "\n";
                        Network += "电源管理：\t" + o["PowerManagementSupported"] + "\n";
                        Network += "产品名称：\t" + o["ProductName"] + "\n";
                        try { Network += "链接速度：\t" + (long.Parse(o["Speed"].ToString()) / 1000000) + " Mbps\n"; } catch { }
                        Network += "生产厂商：\t" + o["Manufacturer"] + "\n";
                        Network += "设备描述：\t" + o["Description"] + "\n";
                        Network += "MAC地址：\t" + o["MACAddress"];
                    }
                }
                temp = 0;
                foreach (ManagementObject o in e1.GetInstances())                        //磁盘
                {
                    temp++; if (temp >= 2) { Disk += "\n\n"; }
                    Disk += "磁盘型号：\t" + o["Model"] + "\n";
                    Disk += "磁盘名称：\t" + o["Name"] + "\n";
                    Disk += "磁盘状态：\t" + o["Status"] + "\n";
                    Disk += "生产厂商：\t" + o["Manufacturer"] + "\n";
                    try { Disk += "磁盘大小：\t" + (float.Parse(o["Size"].ToString()) / 1073741824).ToString("F1") + " GB\n"; } catch { }
                    Disk += "磁盘描述：\t" + o["Description"] + "\n";
                    Disk += "柱面总数：\t" + o["TotalCylinders"] + " 个\n";
                    Disk += "磁头总数：\t" + o["TotalHeads"] + " 个\n";
                    Disk += "扇区总数：\t" + o["TotalSectors"] + " 个\n";
                    Disk += "序列号：\t" + o["SerialNumber"].ToString().Trim();
                }
                temp = 0;
                foreach (ManagementObject o in f.GetInstances())                         //BIOS
                {
                    temp++; if (temp >= 2) { BIOS += "\n\n"; }
                    BIOS += "BIOS名称：\t" + o["Name"] + "\n";
                    BIOS += "生产厂商：\t" + o["Manufacturer"] + "\n";
                    BIOS += "主要BIOS：\t" + o["PrimaryBIOS"] + "\n";
                    BIOS += "BIOS版本：\t" + o["Version"];
                }
                temp = 0;
                foreach (ManagementObject o in g.GetInstances())                        //监视器
                {
                    temp++; if (temp >= 2) { Monitor += "\n\n"; }
                    Monitor += "监视器名称：\t" + o["Name"] + "\n";
                    Monitor += "监视器类型：\t" + o["MonitorType"] + "\n";
                    Monitor += "生产厂商：\t" + o["MonitorManufacturer"] + "\n";
                    Monitor += "监视器编号：\t" + o["DeviceID"] + "\n";
                    Monitor += "监视器高度：\t" + o["ScreenHeight"] + "\n";
                    Monitor += "监视器宽度：\t" + o["ScreenWidth"] + "\n";
                    Monitor += "电源管理支持：\t" + o["PowerManagementSupported"] + "\n";
                    Monitor += "屏幕横向PPI：\t" + o["PixelsPerXLogicalInch"] + " PPI\n";
                    Monitor += "屏幕纵向PPI：\t" + o["PixelsPerYLogicalInch"] + " PPI\n";
                    foreach (ManagementObject o2 in h2.GetInstances())
                    {
                        Monitor += "水平分辨率：\t" + o2["CurrentHorizontalResolution"] + " DPI\n";
                        Monitor += "垂直分辨率：\t" + o2["CurrentVerticalResolution"] + " DPI\n";
                        Monitor += "每秒刷新率：\t" + o2["CurrentRefreshRate"] + " Hz";
                    }
                }
                temp = 0;
                foreach (ManagementObject o in h1.GetInstances())                       //显卡
                {
                    temp++; if (temp >= 2) { Graphics += "\n\n"; }
                    Graphics += "设备名称：\t" + o["DeviceName"] + "\n";
                    foreach (ManagementObject o2 in h2.GetInstances())
                    {
                        Graphics += "设备编号：\t" + o2["DeviceID"] + "\n";
                        Graphics += "芯片厂商：\t" + o2["AdapterCompatibility"] + "\n";
                        Graphics += "出厂日期：\t" + o2["DriverDate"] + "\n";
                        try { Graphics += "显存大小：\t" + (long.Parse(o2["AdapterRAM"].ToString()) / 1048576) + " MB\n"; } catch { }
                        Graphics += "驱动版本：\t" + o2["DriverVersion"] + "\n";
                        Graphics += "DAC类型：\t" + o2["AdapterDACType"] + "\n";
                        Graphics += "最大刷新率：\t" + o2["MaxRefreshRate"] + " MHz\n";
                        Graphics += "最小刷新率：\t" + o2["MinRefreshRate"] + " MHz\n";
                        Graphics += "显示模式：\t" + o2["VideoModeDescription"];
                    }
                }
                temp = 0;
                foreach (ManagementObject o in j.GetInstances())                        //操作系统
                {
                    temp++; if (temp >= 2) { system += "\n\n"; }
                    system += "系统名称：\t" + o["Caption"] + "\n";
                    foreach (ManagementObject o2 in j.GetInstances())
                    {
                        system += "计算机名称：\t" + o2["CSName"] + "\n";
                    }
                    system += "系统版本：\t" + o["Version"] + "\n";
                    system += "系统位宽：\t" + o["OSArchitecture"] + "\n";
                    system += "注册用户：\t" + o["RegisteredUser"] + "\n";
                    system += "安装日期：\t" + o["InstallDate"] + "\n";
                    system += "系统序列号：\t" + o["SerialNumber"];
                }
                temp = 0;
                foreach (ManagementObject o in k.GetInstances())                        //逻辑磁盘信息
                {
                    temp++; if (temp >= 2) { logic += "\n\n"; }
                    logic += "盘符名称：\t" + o["DeviceID"] + "\n";
                    logic += "文件系统：\t" + o["FileSystem"] + "\n";
                    logic += "卷序名称：\t" + o["VolumeName"] + "\n";
                    logic += "卷序列号：\t" + o["VolumeSerialNumber"] + "\n";
                    try { logic += "可用空间：\t" + (float.Parse(o["FreeSpace"].ToString()) / 1073741824).ToString("F1") + " GB\n"; } catch { }
                    try { logic += "总共大小：\t" + (float.Parse(o["Size"].ToString()) / 1073741824).ToString("F1") + " GB\n"; } catch { }
                    logic += "支持文件压缩：\t" + o["SupportsFileBasedCompression"];
                }
                temp = 0;
                foreach (ManagementObject o in l.GetInstances())                        //声卡信息
                {
                    temp++; if (temp >= 2) { sound += "\n\n"; }
                    sound += "设备名称：\t" + o["Caption"] + "\n";
                    sound += "制造商：\t" + o["Manufacturer"] + "\n";
                    sound += "支持电源管理：\t" + o["PowerManagementSupported"] + "\n";
                    sound += "当前状态：\t" + o["Status"];
                }

            }
            catch { }
        }

        private static All_information window;
        private static void Update_control(object sender, RunWorkerCompletedEventArgs e)        //写入信息
        {
            if (processor != null)
                window.其他信息1.Text = processor.Replace("True", "是").Replace("False", "否");
            if (Motherboard != null)
                window.其他信息2.Text = Motherboard.Replace("True", "是").Replace("False", "否");
            if (Disk != null)
                window.其他信息3.Text = Disk.Replace("True", "是").Replace("False", "否");
            if (RAM != null)
                window.其他信息4.Text = RAM.Replace("True", "是").Replace("False", "否");
            if (Network != null)
                window.其他信息5.Text = Network.Replace("True", "是").Replace("False", "否");
            if (logic != null)
                window.其他信息6.Text = logic.Replace("True", "是").Replace("False", "否");
            if (system != null)
                window.其他信息7.Text = system.Replace("True", "是").Replace("False", "否");
            if (BIOS != null)
                window.其他信息8.Text = BIOS.Replace("True", "是").Replace("False", "否");
            if (Monitor != null)
                window.其他信息9.Text = Monitor.Replace("True", "是").Replace("False", "否");
            if (Graphics != null)
                window.其他信息10.Text = Graphics.Replace("True", "是").Replace("False", "否");
            if (sound != null)
                window.其他信息11.Text = sound.Replace("True", "是").Replace("False", "否");
        }

        /// <summary>
        /// 遍历电脑所有配置信息
        /// </summary>
        /// <param name="main"></param>
        public static void Traverse_inf(this All_information main)
        {
            using (BackgroundWorker bw = new BackgroundWorker())
            {
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Update_control);    //完成后返回
                bw.DoWork += new DoWorkEventHandler(Read_information);                          //后台
                bw.RunWorkerAsync();                                                            //启动线程
                window = main;
            }
        }
    }
}
