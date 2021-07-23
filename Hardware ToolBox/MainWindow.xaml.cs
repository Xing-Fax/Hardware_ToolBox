using Hardware_ToolBox.Detection_engine;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Hardware_ToolBox.App;

namespace Hardware_ToolBox
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Initial_screen initial = new Initial_screen();//启动初始页面
        static int select_form = 0;//指定起始页和当前所在页面
        public MainWindow()
        {
            initial.Show();
            InitializeComponent();
            //Me.Visibility = Visibility.Collapsed;

            主窗体.Visibility = Visibility.Hidden;

            ////校验证书安装程序
            //if (Read_and_write_files.Document_verification(Environment.CurrentDirectory + "/Security certificate installation/Certificate installation.exe") != true)
            //{
            //    Environment.Exit(0);
            //}

            //校验自身
            if (Read_and_write_files.Document_verification(Process.GetCurrentProcess().MainModule.FileName) != true)
            {
                Environment.Exit(0);
            }

            ////安全证书检测程序
            //X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
            //store.Open(OpenFlags.MaxAllowed);
            //X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindByThumbprint, "36a888b9f2a505bf92ac6b2796c2188e639ab1d1",false);
            //if (certs.Count == 0 || certs[0].NotAfter < DateTime.Now)
            //{//如果不存在自动启动证书安装程序
            //    Process.Start(Environment.CurrentDirectory + "/Security certificate installation/Certificate installation.exe");
            //}

            //启动后台检测硬件配置
            using (BackgroundWorker bw = new BackgroundWorker())
            {
                bw.DoWork += new DoWorkEventHandler(Hardware_information);
                bw.RunWorkerAsync();
            }

            Read_and_write_files.initialization();//启动检测引擎初始化
            //后台刷新硬件数据 1秒/次
            System.Timers.Timer t = new System.Timers.Timer(1000);//实例化Timer类用于更新时间
            t.Elapsed += new System.Timers.ElapsedEventHandler(Occupancy_rate);//到达时间的时候执行事件
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件

            //程序初始化
            选项卡.SelectedIndex = select_form;
            //显示隐藏界面
            #region
            界面1.Visibility = Visibility.Collapsed;
            界面2.Visibility = Visibility.Collapsed;
            界面3.Visibility = Visibility.Collapsed;
            界面4.Visibility = Visibility.Collapsed;
            界面5.Visibility = Visibility.Collapsed;
            界面6.Visibility = Visibility.Collapsed;
            界面7.Visibility = Visibility.Collapsed;
            界面8.Visibility = Visibility.Collapsed;
            界面9.Visibility = Visibility.Collapsed;
            界面10.Visibility = Visibility.Collapsed;
            #endregion
            select_on(select_form);//指定第一次启动的界面

            //隐藏界面
            #region
            注册表清理框.Visibility = Visibility.Visible;
            常规清理框.Visibility = Visibility.Collapsed;
            高级清理框.Visibility = Visibility.Collapsed;
            系统瘦身框.Visibility = Visibility.Collapsed;
            大文件扫描框.Visibility = Visibility.Collapsed;
            #endregion
            //勾选系统清理下的所有复选框
            for (int i = 1;i <= 31;i ++)
            {
                object o = this.GetType().GetField("注册表" + i.ToString(), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                ((CheckBox)o).IsChecked = true;
            }

            注册表清理.IsChecked = true;
        }
        public void Occupancy_rate(object source, System.Timers.ElapsedEventArgs e)
        {
            if (界面1 .Visibility == Visibility.Visible )//在界面可见时
            {
                string[] Happening = new string[12];
                Happening = Read_and_write_files.device_status();
                Dispatcher.Invoke(new Action(delegate
                {
                    处理器占用.Content = "CPU利用率：" + Happening[0] + " %";
                    处理器占用条.Value = double.Parse(Happening[0]);

                    显卡占用.Content = "GPU利用率：" + Happening[1] + " %";
                    显卡占用条.Value = double.Parse(Happening[1]);

                    内存占用.Content = "内存利用率：" + Happening[2] + " %";
                    内存占用条.Value = double.Parse(Happening[2]);

                    CPU信息.Content =  Happening[4] + " MHz" +
                                "\n" + Happening[5] + " MHz" +
                                "\n" + Happening[6] + " °C";

                    GPU信息.Content =  Happening[8] + " MHz" +
                                "\n" + Happening[9] + " MHz" +
                                "\n" + Happening[10] + " MHz" +
                                "\n" + Happening[11] + " °C"; 

                }));
            }
        }

        void Hardware_information(object sender, DoWorkEventArgs e)
        {
            //读取基本配置信息
            string str = App.Hwinfo(System.AppDomain.CurrentDomain.BaseDirectory + @"\Comprehensive information.ini", System.AppDomain.CurrentDomain.BaseDirectory);
            //获取当前目录下的ini文件路径
            string file = System.AppDomain.CurrentDomain.BaseDirectory + @"\Comprehensive information.ini";
            //读取内存信息
            App.Memory_information();
            //存储各项电脑参数
            string[] data = new string[45];

            //读取基本参数
            #region
            //系统
            data[0] = Read_and_write_files.IniReadValue("系统", "版本", file);
            data[1] = Read_and_write_files.IniReadValue("系统", "Build", file);
            data[2] = Read_and_write_files.IniReadValue("系统", "系统位宽", file);
            //内存
            data[3] = Read_and_write_files.IniReadValue("内存信息", "物理内存总数", file) + " MB";
            data[4] = Read_and_write_files.IniReadValue("内存信息", "物理内存可用", file) + " MB";
            data[5] = Read_and_write_files.IniReadValue("内存信息", "分页文件总数", file) + " MB";
            data[6] = Read_and_write_files.IniReadValue("内存信息", "分页文件可用", file) + " MB";
            data[7] = Read_and_write_files.IniReadValue("内存信息", "虚拟内存总数", file) + " MB";
            data[8] = Read_and_write_files.IniReadValue("内存信息", "虚拟内存可用", file) + " MB";
            string 系统inf = "";
            for (int i = 0; i <= 8; i++) { 系统inf += data[i] + "\n"; }
            //显卡
            data[9] = Read_and_write_files.IniReadValue("显卡", "显卡1型号", file);
            data[10] = Read_and_write_files.IniReadValue("显卡", "显卡1厂商", file);
            data[11] = Read_and_write_files.IniReadValue("显卡", "显卡1驱动版本", file);
            data[12] = Read_and_write_files.IniReadValue("显卡", "显卡1ID", file);
            data[13] = Read_and_write_files.IniReadValue("显卡", "显卡1显存", file);
            data[14] = Read_and_write_files.IniReadValue("显卡", "显卡1步进", file);
            //显示器
            data[15] = Read_and_write_files.IniReadValue("显示器", "显示器1型号_测试版", file);
            data[16] = Read_and_write_files.IniReadValue("显示器", "显示器1ID", file);
            data[17] = Read_and_write_files.IniReadValue("显示器", "显示器1尺寸", file);
            data[18] = Read_and_write_files.IniReadValue("显示器", "显示器1宽度", file) + " * " + Read_and_write_files.IniReadValue("显示器", "显示器1高度", file); ;
            data[19] = Read_and_write_files.IniReadValue("显示器", "显示器1生产日期", file);
            data[20] = Read_and_write_files.IniReadValue("显示器", "显示器1edid版本", file);
            data[21] = Read_and_write_files.IniReadValue("显示器", "显示器1序列号_测试版", file);
            string 显卡inf = "";
            for (int i = 9; i <= 21; i++) { 显卡inf += data[i] + "\n"; }
            //处理器
            data[22] = Read_and_write_files.IniReadValue("处理器", "处理器型号", file);
            data[23] = Read_and_write_files.IniReadValue("处理器", "处理器架构", file);
            data[24] = Read_and_write_files.IniReadValue("处理器", "处理器厂商", file);
            data[25] = Read_and_write_files.IniReadValue("处理器", "处理器接口", file);
            data[26] = Read_and_write_files.IniReadValue("处理器", "处理器核心数", file) + " 个";
            data[27] = Read_and_write_files.IniReadValue("处理器", "处理器线程数", file) + " 个";
            data[28] = Read_and_write_files.IniReadValue("处理器", "处理器位宽", file);
            data[29] = Read_and_write_files.IniReadValue("处理器", "处理器主频", file);
            data[30] = Read_and_write_files.IniReadValue("处理器", "处理器外频", file);
            data[31] = "暂时无法获取";
            data[32] = Read_and_write_files.IniReadValue("处理器", "处理器数量", file);
            data[33] = Read_and_write_files.IniReadValue("缓存", "缓存1类型", file);
            data[34] = Read_and_write_files.IniReadValue("缓存", "缓存1容量", file) + " KB";
            data[35] = Read_and_write_files.IniReadValue("缓存", "缓存2类型", file);
            data[36] = Read_and_write_files.IniReadValue("缓存", "缓存2容量", file) + " KB";
            data[37] = Read_and_write_files.IniReadValue("缓存", "缓存3类型", file);
            data[38] = Read_and_write_files.IniReadValue("缓存", "缓存3容量", file) + " KB";
            data[39] = Read_and_write_files.IniReadValue("缓存", "缓存4类型", file);
            data[40] = Read_and_write_files.IniReadValue("缓存", "缓存4容量", file) + " KB";
            string 处理器inf = "";
            for (int i = 22; i <= 40; i++) { 处理器inf += data[i] + "\n"; }
            //bios芯片
            data[41] = Read_and_write_files.IniReadValue("BIOS", "厂商", file);
            data[42] = Read_and_write_files.IniReadValue("BIOS", "说明", file);
            data[43] = Read_and_write_files.IniReadValue("BIOS", "版本", file);
            data[44] = Read_and_write_files.IniReadValue("BIOS", "OEM版本", file);
            string BIOSinf = "";
            for (int i = 41; i <= 44; i++) { BIOSinf += data[i] + "\n"; }
            #endregion

            //删除检测文件
            File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + @"\Comprehensive information.ini");
            File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + @"\PnPDevice.ini");

            Dispatcher.Invoke(new Action(delegate
            {
                电脑基本信息.Text = str;
                操作系统信息.Text = 系统inf;
                显卡显示器信息.Text = 显卡inf;
                中央处理器信息.Text = 处理器inf;
                bios信息.Text = BIOSinf;
                initial.Close();
                动画播放("程序启动");
            }));
        }

        //播放动画函数
        private void 动画播放(string str)
        {
            BeginStoryboard((Storyboard)FindResource(str));
        }
        //拖动标题移动窗体
        private void 标题栏_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        int currency = 1;
        private void 菜单背景_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (小方块.Opacity == 0)
            {
                动画播放("小人跳");
                金币.Content = "金币+" + currency.ToString() + "...";
                currency++;
            }
        }

        private void 菜单打开_Click(object sender, RoutedEventArgs e)
        {
            动画播放("菜单打开");
        }

        private void 菜单关闭_Click(object sender, RoutedEventArgs e)
        {
            动画播放("菜单关闭");
        }

        private void 黑幕_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (黑幕.Opacity == 1) { 动画播放("菜单关闭"); }
        }

        private void 常规清理_Click(object sender, RoutedEventArgs e)
        {
            hide();
            常规清理框.Visibility = Visibility.Visible;
        }

        private void 注册表清理_Click(object sender, RoutedEventArgs e)
        {
            hide();
            注册表清理框.Visibility = Visibility.Visible;
        }

        private void 高级清理_Click(object sender, RoutedEventArgs e)
        {
            hide();
            高级清理框.Visibility = Visibility.Visible;
        }

        private void 系统瘦身_Click(object sender, RoutedEventArgs e)
        {
            hide();
            系统瘦身框.Visibility = Visibility.Visible;
        }

        private void 大文件扫描_Click(object sender, RoutedEventArgs e)
        {
            hide();
            大文件扫描框.Visibility = Visibility.Visible;
        }

        private void 关闭_MouseUp(object sender, MouseButtonEventArgs e)
        {
             主窗体.Visibility = Visibility.Collapsed;
            Environment.Exit(0);
        }

        private void 最小化_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void 选项卡_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (选项卡.SelectedIndex != select_form && 选项卡.SelectedIndex != -1)
            {
                select_off(select_form);
                select_on(选项卡.SelectedIndex);
                动画播放("菜单关闭");
            }
        }

        private void 跟多_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (跟多.SelectedIndex)
            {
                case 0:
                    select_off(select_form);
                    select_on(7);
                    break;
                case 1:
                    select_off(select_form);
                    select_on(9);
                    break;
                case 2:
                    select_off(select_form);
                    select_on(8);
                    break;
            }
            选项卡.SelectedIndex = -1;
        }


        //打开/关闭 指定页面
        #region
        //指定要打开的页面
        private void select_on(int sum)
        {
            switch (sum)
            {
                case 0:
                    动画播放("界面1打开");
                    提示信息.Content = "基本信息：显示计算机当前基本配置信息和运行状态";
                    break;
                case 1:
                    动画播放("界面2打开");
                    提示信息.Content = "常用工具：提供经常使用的软件和快捷方式";
                    break;
                case 2:
                    动画播放("界面3打开");
                    提示信息.Content = "高级软件：为高级用户提供的软件和功能";
                    break;
                case 3:
                    动画播放("界面4打开");
                    提示信息.Content = "系统清理：提供计算机常用垃圾扫描和缓存清理功能";
                    break;
                case 4:
                    动画播放("界面5打开");
                    提示信息.Content = "系统优化：为计算机系统进行优化与清理";
                    break;
                case 5:
                    动画播放("界面6打开");
                    提示信息.Content = "系统监视：监视计算机系统和各硬件当前运行状态";
                    break;
                case 6:
                    动画播放("界面7打开");
                    提示信息.Content = "其他工具：提供其他不常用工具";
                    break;
                case 7:
                    动画播放("界面8打开");
                    提示信息.Content = "软件设置：修改软件设置和基本配置信息";
                    break;
                case 8:
                    动画播放("界面9打开");
                    提示信息.Content = "关于程序：显示对本程序的说明与提示";
                    break;
                case 9:
                    动画播放("界面10打开");
                    提示信息.Content = "需要帮助：为用户使用软件提供帮助说明";
                    break;
            }
            select_form = sum;
        }
        //指定要关闭的页面
        private void select_off(int sum)
        {
            switch (sum)
            {
                case 0:
                    动画播放("界面1关闭");
                    break;
                case 1:
                    动画播放("界面2关闭");
                    break;
                case 2:
                    动画播放("界面3关闭");
                    break;
                case 3:
                    动画播放("界面4关闭");
                    break;
                case 4:
                    动画播放("界面5关闭");
                    break;
                case 5:
                    动画播放("界面6关闭");
                    break;
                case 6:
                    动画播放("界面7关闭");
                    break;
                case 7:
                    动画播放("界面8关闭");
                    break;
                case 8:
                    动画播放("界面9关闭");
                    break;
                case 9:
                    动画播放("界面10关闭");
                    break;
            }
        }
        #endregion

        //隐藏其他界面
        #region
        private void hide()
        {
            注册表清理框.Visibility = Visibility.Collapsed;
            常规清理框.Visibility = Visibility.Collapsed;
            高级清理框.Visibility = Visibility.Collapsed;
            系统瘦身框.Visibility = Visibility.Collapsed;
            大文件扫描框.Visibility = Visibility.Collapsed;
        }
        #endregion

        //打开指定工具事件
        #region
        private void image_Copy_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\CPU-Z\CUP-Z.exe");
        }

        private void image_Copy1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\GPU-Z\GPU-Z.exe");
        }

        private void image_Copy2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\SSD-Z\SSD-Z.exe");
        }

        private void image_Copy3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\HDTune\HDTune.exe");
        }

        private void image_Copy4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\CrystalDiskInfo\DiskInfo64.exe");
        }

        private void image_Copy5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\AS SDD\ASSSD.exe");
        }

        private void image_Copy6_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\DisplayX\DisplayX.exe");
        }

        private void image_Copy7_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\monitorinfo\monitorinfo.exe");
        }

        private void image_Copy8_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\ChipGenius\ChipGenius_v4_19_0319.exe");
        }

        private void image_Copy9_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\MyDiskTest\MyDiskTest_v298.exe");
        }

        private void image_Copy10_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\HWMonitor\HWMonitor_x64.exe");
        }

        private void image_Copy11_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\Geek Uninstaller\Geek Uninstaller.exe");
        }

        private void image_Copy12_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\SmartRAM\SmartRAM_3.0.exe");
        }

        private void image_Copy13_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\BatteryInfoView\BatteryInfoView.exe");
        }

        private void image_Copy14_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\memtest64\MemTest64.exe");
        }

        private void image_Copy15_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\FurMark\FurMark.exe");
        }

        private void image_Copy16_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\CoreTemp\Core Temp.exe");
        }

        private void image_Copy17_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\mouse Test Utility\鼠标单击变双击测试器V2.0.exe");
        }

        private void image_Copy18_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\Keyboard Test Utility\Keyboard Test Utility.exe");
        }

        private void image_Copy19_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\AIDA64\aida64.exe");
        }

        private void image_Copy20_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\DiskGeniusPro\DiskGeniusPro.exe");
        }

        private void image_Copy21_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\memtestpro\memtestpro.exe");
        }

        private void image_Copy22_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\Prime95\prime95x64.exe");
        }

        private void image_Copy23_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\superpi\Superpi.exe");
        }

        private void image_Copy24_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\ULTRAISO\ULTRAISO.EXE");
        }

        private void image_Copy25_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\微星小飞机\MSIAfterburnerSetup462.exe");
        }

        private void image_Copy26_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\AMDGPUClockTool\AMDGPUClockTool.exe");
        }

        private void image_Copy27_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\gpuinfo\Gpuinfo.exe");
        }

        private void image_Copy28_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\speccy\Speccy64.exe");
        }

        private void image_Copy29_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\hwinfo\HWiNFO64.exe");
        }

        private void image_Copy30_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\魔方数据恢复\魔方数据恢复.exe");
        }

        private void image_Copy31_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\LLFTOOL\LLFTOOL.exe");
        }

        private void image_Copy32_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\内存整理\Sunlight内存整理_原始.exe");
        }

        private void image_Copy33_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\DDU\Display Driver Uninstaller.exe");
        }

        private void image_Copy34_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\魔方内存盘\ramdisk.exe");
        }

        private void image_Copy35_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\Uninstall Tool\Uninstall Tool.exe");
        }

        private void image_Copy36_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\接码神器\接码神器.exe");
        }

        private void image_Copy37_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\云盘搜搜\云盘搜索助手.exe");
        }

        private void image_Copy38_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\图标替换\U盘图标替换程序.exe");
        }

        private void image_Copy39_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\图拉丁硬件检测\图拉丁硬件检测.exe");
        }

        private void image_Copy40_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\finaldata\FINALDATA.exe");
        }

        private void image_Copy41_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\Defraggler\Defraggler.exe");
        }

        private void image_Copy42_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\ATTODISKBENCHMARK\ATTO 磁盘基准测试.exe");
        }

        private void image_Copy43_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\nvidiaInspector\nvidiaInspector.exe");
        }

        private void image_Copy44_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\gpuinfo\Gpuinfo.exe");
        }

        private void image_Copy45_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\Thaiphoon\Thaiphoon.exe");
        }

        private void image_Copy46_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\线程炸弹\线程炸弹.exe");
        }

        private void image_Copy47_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\XIANGQI\xiangqi.exe");
        }

        private void image_Copy48_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\音箱煲箱\音箱煲机软件.exe");
        }

        private void image_Copy49_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\AG视频解析\AG视频解析v4.2.exe");
        }

        private void image_Copy50_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\B站视频下载\B站视频下载工具 3.5.exe");
        }



        private void image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\CPU-Z\CUP-Z.exe");
        }

        private void image1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\GPU-Z\GPU-Z.exe");
        }

        private void image2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\SSD-Z\SSD-Z.exe");
        }

        private void image3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\HDTune\HDTune.exe");
        }

        private void image4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\DisplayX\DisplayX.exe");
        }

        private void image5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\BatteryInfoView\BatteryInfoView.exe");
        }

        private void image6_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\CrystalDiskInfo\DiskInfo64.exe");
        }

        private void image7_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\AS SDD\ASSSD.exe");
        }

        private void image8_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\mouse Test Utility\鼠标单击变双击测试器V2.0.exe");
        }

        private void image9_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\monitorinfo\monitorinfo.exe");
        }

        private void image10_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\Uninstall Tool\Uninstall Tool.exe");
        }

        private void image11_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\AIDA64\aida64.exe");
        }

        private void image12_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\天若OCR开源版V5.0.0\天若OCR文字识别.exe");
        }

        private void image_Copy51_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\天若OCR开源版V5.0.0\天若OCR文字识别.exe");
        }

        private void image_Copy52_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\抓包工具\Fiddler.exe");
        }

        private void image_Copy53_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\SAI\sai.exe");
        }

        private void image_Copy54_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\GIF录制\GifCam.exe");
        }

        private void image_Copy55_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\\PKG解包\WpfApp22.exe");
        }

        private void image_Copy56_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\ASCII码速查\ASCII码速查.exe");
        }

        private void image_Copy57_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\x96dbg\release\x96dbg.exe");
        }

        private void image_Copy58_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\.NET Reactor 5.9.8.0\dotNET_Reactor.exe");
        }

        private void image_Copy59_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\M3U8下载\M3U8 Downloader.exe");
        }

        private void image_Copy60_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\exe打包工具\exe打包工具.exe");
        }

        private void image_Copy61_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\ICO图标转换\ConvertToIco.exe");
        }

        private void image_Copy62_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\MD5校验工具\MD5效验工具.exe");
        }

        private void image_Copy63_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\NSIS单文件封装\NSIS单文件封装.exe");
        }

        private void image_Copy64_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\ollyDBG脱壳脚本编辑器\ollyDBG脱壳脚本编辑器.exe");
        }

        private void image_Copy65_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\Procmon-监视进程行为\Procmon-监视进程行为.exe");
        }

        private void image_Copy66_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\RAR密码破解器\AccentRPR_x64.exe");
        }

        private void image_Copy67_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\磁盘空间分析\SpaceSniffer v1.3.0.2_CHS.exe");
        }

        private void image_Copy68_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\防卸载隐藏工具\HideUL.exe");
        }

        private void image_Copy69_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\封包助手\PackAssist.exe");
        }

        private void image_Copy70_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\灰色按钮解锁\灰色按钮解锁.exe");
        }

        private void image_Copy71_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\文件操作监视\文件操作监控.exe");
        }

        private void image_Copy72_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\文件占用解锁\文件解锁器.exe");
        }

        private void image_Copy73_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\吾爱破解\吾爱破解[LCG].exe");
        }

        private void image_Copy74_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\系统字体修改\noMeiryoUI.exe");
        }

        private void image_Copy75_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\图标提取\提取高清图标.exe");
        }

        private void image_Copy76_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\进制转换\进制转换完整版.exe");
        }

        private void image_Copy77_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Software catalog\局域网查看器\局域网查看器.exe");
        }


        #endregion

        //打开指定网站
        #region
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://s.threatbook.cn/");
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://graph.baidu.com/pcpage/index?tpl_from=pc");
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://greasyfork.org/zh-CN");
        }

        private void button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://plugin.speedtest.cn/#/");
        }

        private void button_Copy3_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.yinsiduanxin.com/");
        }

        private void button_Copy4_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://msdn.itellyou.cn/");
        }

        private void button_Copy5_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.aconvert.com/cn/icon/png-to-ico/");
        }

        private void button_Copy6_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.cn-ki.net/");
        }

        private void button_Copy7_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.uupoop.com/#/old");
        }

        private void button_Copy8_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.bilibili.com/");
        }

        private void button_Copy9_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/github/");
        }

        private void button_Copy10_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.githubs.cn/");
        }

        private void button_Copy11_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.gnet.com.cn/include/voice/voice.php");
        }

        private void button_Copy12_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.toolnb.com/");
        }

        private void button_Copy13_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://uhdpixel.com/");
        }

        private void button_Copy14_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.asklib.com/");
        }
        #endregion

        //通过调用cmd打开指定系统应用
        #region
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Read_and_write_files.RunCmd(@"%windir%\system32\dfrgui.exe");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Read_and_write_files.RunCmd(@"%windir%\system32\eventvwr.msc /s");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Read_and_write_files.RunCmd(@"%windir%\regedit.exe");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Read_and_write_files.RunCmd(@"%windir%\system32\services.msc");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Read_and_write_files.RunCmd(@"start");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Read_and_write_files.RunCmd(@"%windir%\system32\cleanmgr.exe");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
           Read_and_write_files.RunCmd(@"%windir%\system32\msinfo32.exe");
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Read_and_write_files.RunCmd(@"%windir%\system32\perfmon.exe /res");
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Read_and_write_files.RunCmd(@"control");
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            Read_and_write_files.RunCmd(@"taskmgr.exe");
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            Read_and_write_files.RunCmd(@"%windir%\system32\MdSched.exe");
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            Read_and_write_files.RunCmd(@"%windir%\system32\compmgmt.msc /s");
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            Read_and_write_files.RunCmd(@"%windir%\regedit.exe");
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            Read_and_write_files.RunCmd(@"%windir%\system32\secpol.msc /s");
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            Read_and_write_files.RunCmd(@"%windir%\system32\WF.msc");
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            Read_and_write_files.RunCmd(@"%windir%\system32\taskschd.msc /s");
        }
        #endregion

        //加密解密部分
        #region
        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            try
            {
                //加密
                if (原文.Text != "")
                {
                    if (算法1.IsChecked == true)
                    {
                        密文.Text = String_algorithm.AES.AesEncrypt(原文.Text, String_algorithm.Key_handling.Fill_in(密钥1.Text, 32));
                    }
                    else if (算法2.IsChecked == true)
                    {
                        密文.Text = String_algorithm.DES.DesEncrypt(原文.Text, String_algorithm.Key_handling.Fill_in(密钥1.Text, 8));
                    }
                    else if (算法3.IsChecked == true)
                    {
                        密文.Text = String_algorithm.RC4.EncryptRC4wq(原文.Text, String_algorithm.Key_handling.Fill_in(密钥1.Text, 1));
                    }
                    else if (算法4.IsChecked == true)
                    {
                        密文.Text = String_algorithm.Base64.Base64Encrypt(原文.Text);
                    }
                    else if (算法5.IsChecked == true)
                    {
                        密文.Text = String_algorithm.SHA265.SHA256EncryptString(原文.Text);
                    }
                    else if (算法6.IsChecked == true)
                    {
                        密文.Text = String_algorithm.SHA512.sha512Encode(原文.Text);
                    }
                    else
                    {
                        MessageBox.Show("选择加密方式");
                    }
                }
            }
            catch
            {
                MessageBox.Show("密钥非法！");
            }

        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            try
            {
                //解密
                if (原文.Text != "")
                {
                    if (算法1.IsChecked == true)
                    {
                        密文.Text = String_algorithm.AES.AesDecrypt(原文.Text, String_algorithm.Key_handling.Fill_in(密钥1.Text, 32));
                    }
                    else if (算法2.IsChecked == true)
                    {
                        密文.Text = String_algorithm.DES.DeaDecrypt(原文.Text, String_algorithm.Key_handling.Fill_in(密钥1.Text, 8));
                    }
                    else if (算法3.IsChecked == true)
                    {
                        密文.Text = String_algorithm.RC4.DecryptRC4wq(原文.Text, String_algorithm.Key_handling.Fill_in(密钥1.Text, 1));
                    }
                    else if (算法4.IsChecked == true)
                    {
                        密文.Text = String_algorithm.Base64.Base64Decrypt(原文.Text);
                    }
                    else if (算法5.IsChecked == true)
                    {
                        //不可逆
                        MessageBox.Show("SHA256加密不可逆");
                    }
                    else if (算法6.IsChecked == true)
                    {
                        //不可逆
                        MessageBox.Show("SHA512加密不可逆");
                    }
                    else
                    {
                        MessageBox.Show("选择加密方式");
                    }
                }
            }
            catch
            {
                MessageBox.Show("密钥非法！");
            }
            
        }


        private void 算法1_Checked(object sender, RoutedEventArgs e)
        {
            密钥1.Text = "";
            密钥1.MaxLength = 32;
        }

        private void 算法2_Checked(object sender, RoutedEventArgs e)
        {
            密钥1.Text = "";
            密钥1.MaxLength = 8;
        }
        private void 算法3_Checked(object sender, RoutedEventArgs e)
        {
            密钥1.Text = "";
            密钥1.MaxLength = 128;
        }

        private void 算法4_Checked(object sender, RoutedEventArgs e)
        {
            密钥1.Text = "此加密无需密钥";
            密钥1.MaxLength = 0;
        }

        private void 算法5_Checked(object sender, RoutedEventArgs e)
        {
            密钥1.Text = "此加密无需密钥";
            密钥1.MaxLength = 0;
        }
        #endregion

    }
}
