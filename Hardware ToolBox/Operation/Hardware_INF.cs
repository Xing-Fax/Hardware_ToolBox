using Hardware_ToolBox.Control_class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardware_ToolBox.Operation
{
    //   ┏┓　    ┏┓
    // ┏┛┻━━━┛┻┓
    // ┃　　　　　　　┃ 　
    // ┃　　　━　　　┃
    // ┃　┳┛　┗┳　┃
    // ┃　　　　　　　┃
    // ┃　　　┻　　　┃
    // ┃　　　　　　　┃
    // ┗━┓　　　┏━┛
    //     ┃　　　┃ 神兽保佑
    //     ┃　　　┃ 代码无BUG！
    //     ┃　　　┗━━━┓
    //     ┃　　　　　　　┣┓
    //     ┃　　　　　　　┏┛
    //     ┗┓┓┏━┳┓┏┛
    //       ┃┫┫　┃┫┫
    //       ┗┻┛　┗┻┛

    /// <summary>
    /// 得到配置基本信息
    /// </summary>
    public static class Hardware_INF
    {
        //保存格式化后的数据
        static string 系统inf = null, 显卡inf = null, 核心inf = null, BIOSinf = null, Brief = null;
        private static void Read_information(object sender, DoWorkEventArgs e)
        {
            string Absolute_path = AppDomain.CurrentDomain.BaseDirectory;                               //获得自己的绝对目录路径

            Brief = General.Console(Absolute_path + "Configuration information.exe", "");               //获得信息摘要

            string ini_file = AppDomain.CurrentDomain.BaseDirectory + @"Comprehensive information.ini"; //设置ini文件目录位置

            string[] data = new string[46];                                                             //建立数值用于存储信息

            #region//开始读取信息
            //系统
            data[0] = General.Read_ini("系统", "版本", ini_file);
            data[1] = General.Read_ini("系统", "Build", ini_file);
            data[2] = General.Read_ini("系统", "系统位宽", ini_file);
            //内存
            string temp = General.Console(Absolute_path + @"Security certificate installation\Configuration detection.exe", " Detect -m");
            data[3] = String.Substring(temp, "注册用户：", "<-");
            data[4] = General.Read_ini("内存信息", "物理内存总数", ini_file) + " MB";
            data[5] = General.Read_ini("内存信息", "分页文件总数", ini_file) + " MB";
            data[6] = General.Read_ini("内存信息", "虚拟内存总数", ini_file) + " MB";
            data[7] = String.Substring(temp, "计算机名称：", "<-");
            data[8] = String.Substring(temp, "序列号：", "<-");
            //显卡
            data[9] = General.Read_ini("显卡", "显卡1型号", ini_file);
            data[10] = General.Read_ini("显卡", "显卡1厂商", ini_file);
            data[11] = General.Read_ini("显卡", "显卡1驱动版本", ini_file);
            data[12] = General.Read_ini("显卡", "显卡1ID", ini_file);
            data[13] = General.Read_ini("显卡", "显卡1显存", ini_file);
            data[14] = General.Read_ini("显卡", "显卡1步进", ini_file);
            //显示器
            temp = General.Console(Absolute_path + @"Security certificate installation\Configuration detection.exe", " Detect -x");
            data[15] = General.Read_ini("显示器", "显示器1型号_测试版", ini_file);
            data[16] = General.Read_ini("显示器", "显示器1ID", ini_file);
            data[17] = General.Read_ini("显示器", "显示器1尺寸", ini_file);
            data[18] = General.Read_ini("显示器", "显示器1宽度", ini_file) + " * " + General.Read_ini("显示器", "显示器1高度", ini_file); ;
            data[19] = General.Read_ini("显示器", "显示器1生产日期", ini_file);
            data[20] = General.Read_ini("显示器", "显示器1edid版本", ini_file);
            data[21] = String.Substring(temp, "当前显示模式：", "<-") + "\n" + String.Substring(temp, "DAC类型：", "<-");
            //处理器
            temp = General.Console(Absolute_path + @"Security certificate installation\Configuration detection.exe", " Detect -p");
            data[22] = General.Read_ini("处理器", "处理器型号", ini_file);
            data[23] = General.Read_ini("处理器", "处理器架构", ini_file);
            data[24] = General.Read_ini("处理器", "处理器厂商", ini_file);
            data[25] = General.Read_ini("处理器", "处理器接口", ini_file);
            data[26] = General.Read_ini("处理器", "处理器核心数", ini_file) + " 个";
            data[27] = General.Read_ini("处理器", "处理器线程数", ini_file) + " 个";
            data[28] = General.Read_ini("处理器", "处理器位宽", ini_file);
            data[29] = General.Read_ini("处理器", "处理器主频", ini_file);
            data[30] = General.Read_ini("处理器", "处理器外频", ini_file);
            data[31] = String.Substring(temp, "虚拟化支持：", "<-").Replace("True", "已启用").Replace("False", "已禁用");
            data[32] = General.Read_ini("处理器", "处理器数量", ini_file) + " 颗处理器";
            data[33] = General.Read_ini("缓存", "缓存1容量", ini_file) + " KB";
            data[34] = General.Read_ini("缓存", "缓存2容量", ini_file) + " KB";
            data[35] = General.Read_ini("缓存", "缓存3容量", ini_file) + " KB";
            try { data[36] = (float.Parse(String.Substring(temp, "CPU电压：", "<-")) / 10).ToString() + " V"; } catch { }
            try { data[37] = (int.Parse(General.Read_ini("内存信息", "物理内存总数", ini_file)) / 1000).ToString() + " GB"; } catch { }
            data[38] = General.Read_ini("内存", "数量", ini_file) + " 个";
            data[39] = String.Substring(temp, "Hyper-V支持：", "<-").Replace("True", "已启用").Replace("False", "已禁用");
            data[40] = String.Substring(temp, "CPU编号：", "<-").Trim();
            //bios芯片
            data[41] = General.Read_ini("BIOS", "厂商", ini_file);
            data[42] = General.Read_ini("BIOS", "说明", ini_file);
            data[43] = General.Read_ini("BIOS", "版本", ini_file);
            data[44] = General.Read_ini("BIOS", "OEM版本", ini_file);
            #endregion

            //删除检测临时文件
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Comprehensive information.ini");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\PnPDevice.ini");

            //遍历信息，将空的信息替换为“暂无信息”

            //遍历系统信息
            for (int i = 0; i <= 8; i++)   { if (data[i] != "") { 系统inf += data[i] + "\n"; } else { 系统inf += "暂无信息" + "\n"; }; }
            //遍历监视器和显卡信息
            for (int i = 9; i <= 21; i++)  { if (data[i] != "") { 显卡inf += data[i] + "\n"; } else { 显卡inf += "暂无信息" + "\n"; }; }
            //遍历处理器信息
            for (int i = 22; i <= 40; i++) { if (data[i] != "") { 核心inf += data[i] + "\n"; } else { 核心inf += "暂无信息" + "\n"; }; }
            //遍历BIOS信息
            for (int i = 41; i <= 44; i++) { if (data[i] != "") { BIOSinf += data[i] + "\n"; } else { BIOSinf += "暂无信息" + "\n"; }; }
        }

        private static Basic_Information window;
        private static void Update_control(object sender, RunWorkerCompletedEventArgs e)
        {
            //更新信息
            window.电脑基本信息.Text = Brief;
            window.操作系统信息.Text = 系统inf;
            window.显卡显示器信息.Text = 显卡inf;
            window.中央处理器信息.Text = 核心inf;
            window.bios信息.Text = BIOSinf;
            App.Con_inf = true;//完成任务
        }

        /// <summary>
        /// 遍历电脑基本配置信息
        /// </summary>
        /// <param name="main"></param>
        public static void Traverse_inf(this Basic_Information main)
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
