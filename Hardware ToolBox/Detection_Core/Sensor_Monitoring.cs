using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardware_ToolBox.Detection_Core
{
    /// <summary>
    /// LibreHardwareMonitor检测核心
    /// </summary>
    class Sensor_Monitoring
    {
        private class UpdateVisitor : IVisitor
        {
            public void VisitComputer(IComputer computer)
            {
                computer.Traverse(this);
            }
            public void VisitHardware(IHardware hardware)
            {
                hardware.Update();
                foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
            }
            public void VisitSensor(ISensor sensor) { }
            public void VisitParameter(IParameter parameter) { }
        }
        /// <summary>
        /// 检测引擎
        /// </summary>
        private static Computer computer = new Computer();
        /// <summary>
        /// 初始化监测引擎
        /// </summary>
        /// <param name="Processor">是否启用CPU</param>
        /// <param name="Graphics">是否启用GPU</param>
        /// <param name="RAM">是否启用RAM</param>
        /// <param name="Master">是否启用系统母板</param>
        /// <param name="Controller">是否启用控制器</param>
        /// <param name="Network">是否启用网卡</param>
        /// <param name="Storage">是否启用ROM</param>
        /// <param name="UPS">是否启用电源</param>
        public static void Initial(bool Processor,bool Graphics,bool RAM,bool Master,bool Controller,bool Network,bool Storage,bool UPS)
        {
            computer.IsCpuEnabled = Processor;          //处理器
            computer.IsGpuEnabled = Graphics;           //显卡
            computer.IsMemoryEnabled = RAM;             //内存
            computer.IsMotherboardEnabled = Master;     //主板
            computer.IsControllerEnabled = Controller;  //控制器
            computer.IsNetworkEnabled = Network;        //网络
            computer.IsStorageEnabled = Storage;        //存储
            computer.IsPsuEnabled = UPS;                //电源
            computer.Open();                            //启动
        }
        /// <summary>
        /// 刷新并返回数据
        /// </summary>
        /// <returns>返回字符串格式数据</returns>
        public static string Core()
        {
            computer.Accept(new UpdateVisitor());                                           //刷新数据
            string Information = null;
            foreach (IHardware hardware in computer.Hardware)                               //遍历传感器信息
            {
                //Information += "Name->" + hardware.Name + "<-\n";
                foreach (IHardware subhardware in hardware.SubHardware)
                    //Information += "Child->" + subhardware.Name + "<-\n";
                    foreach (ISensor sensor in subhardware.Sensors)
                        Information += sensor.Name + " " + sensor.SensorType + " ->" + sensor.Value + "<-\n";
                foreach (ISensor sensor in hardware.Sensors)
                    Information += sensor.Name + " " + sensor.SensorType + " ->" + sensor.Value + "<-\n";
            }
            return Information;                                                             //返回数据
        }

        /// <summary>
        /// 获得配置报告文件
        /// 获取前请先初始化要获取的硬件类型
        /// </summary>
        /// <returns></returns>
        public static string Report()
        {
            return computer.GetReport();            //得到报告文件
        }

        /// <summary>
        /// 关闭程序后自动释放资源
        /// </summary>
        public static void Release_resources()
        {
            computer.Close();                       //释放资源
        }
    }
}
