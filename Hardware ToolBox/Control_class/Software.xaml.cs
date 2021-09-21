using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

namespace Hardware_ToolBox.Control_class
{
    /// <summary>
    /// Software.xaml 的交互逻辑
    /// </summary>
    public partial class Software : UserControl
    {
        public Software()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 打开指定exe可执行文件
        /// 并判断文件是否存在
        /// 存在打开程序
        /// 不存在提示错误
        /// </summary>
        /// <param name="file">文件路径</param>
        private void Open_exe(string file)
        {
            try
            {
                if (File.Exists(file))
                {
                    Process.Start(file);
                }
                else
                {
                    hint.window.Pop_ups("错误代码：-1");
                }
            }
            catch
            {
                hint.window.Pop_ups("错误代码：-2");
            }
        }

        private void image_Copy_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\CPU-Z\CUP-Z.exe");
        }

        private void image_Copy1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\GPU-Z\GPU-Z.exe");
        }

        private void image_Copy2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\SSD-Z\SSD-Z.exe");
        }

        private void image_Copy3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\HDTune\HDTune.exe");
        }

        private void image_Copy4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\CrystalDiskInfo\DiskInfo64.exe");
        }

        private void image_Copy5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\AS SDD\ASSSD.exe");
        }

        private void image_Copy6_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\DisplayX\DisplayX.exe");
        }

        private void image_Copy7_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\monitorinfo\monitorinfo.exe");
        }

        private void image_Copy8_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\ChipGenius\ChipGenius_v4_19_0319.exe");
        }

        private void image_Copy9_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\MyDiskTest\MyDiskTest_v298.exe");
        }

        private void image_Copy10_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\HWMonitor\HWMonitor_x64.exe");
        }

        private void image_Copy11_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\LinX\LinX.exe");
        }

        private void image_Copy12_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\SmartRAM\SmartRAM_3.0.exe");
        }

        private void image_Copy13_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\BatteryInfoView\BatteryInfoView.exe");
        }

        private void image_Copy14_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\memtest64\MemTest64.exe");
        }

        private void image_Copy15_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\FurMark\FurMark.exe");
        }

        private void image_Copy16_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\CoreTemp\Core Temp.exe");
        }

        private void image_Copy17_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\mouse Test Utility\鼠标单击变双击测试器V2.0.exe");
        }

        private void image_Copy18_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\Keyboard Test Utility\Keyboard Test Utility.exe");
        }

        private void image_Copy19_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\AIDA64\aida64.exe");
        }

        private void image_Copy20_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\DiskGeniusPro\DiskGeniusPro.exe");
        }

        private void image_Copy21_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\memtestpro\memtestpro.exe");
        }

        private void image_Copy22_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\Prime95\prime95x64.exe");
        }

        private void image_Copy23_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\superpi\Superpi.exe");
        }

        private void image_Copy24_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\ULTRAISO\ULTRAISO.EXE");
        }

        private void image_Copy25_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\微星小飞机\MSIAfterburnerSetup462.exe");
        }

        private void image_Copy26_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\AMDGPUClockTool\AMDGPUClockTool.exe");
        }

        private void image_Copy27_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\gpuinfo\Gpuinfo.exe");
        }

        private void image_Copy28_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\speccy\Speccy64.exe");
        }

        private void image_Copy29_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\hwinfo\HWiNFO64.exe");
        }

        private void image_Copy30_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\魔方数据恢复\魔方数据恢复.exe");
        }

        private void image_Copy31_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\LLFTOOL\LLFTOOL.exe");
        }

        private void image_Copy32_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\内存整理\Sunlight内存整理_原始.exe");
        }

        private void image_Copy33_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\DDU\Display Driver Uninstaller.exe");
        }

        private void image_Copy34_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\魔方内存盘\ramdisk.exe");
        }

        private void image_Copy35_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\Uninstall Tool\Uninstall Tool.exe");
        }

        private void image_Copy36_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\接码神器\接码神器.exe");
        }

        private void image_Copy37_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\云盘搜搜\云盘搜索助手.exe");
        }

        private void image_Copy38_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\图标替换\U盘图标替换程序.exe");
        }

        private void image_Copy39_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\图拉丁硬件检测\图拉丁硬件检测.exe");
        }

        private void image_Copy40_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\finaldata\FINALDATA.exe");
        }

        private void image_Copy41_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\Defraggler\Defraggler.exe");
        }

        private void image_Copy42_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\ATTODISKBENCHMARK\ATTO 磁盘基准测试.exe");
        }

        private void image_Copy43_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\nvidiaInspector\nvidiaInspector.exe");
        }

        private void image_Copy44_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\gpuinfo\Gpuinfo.exe");
        }

        private void image_Copy45_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\Thaiphoon\Thaiphoon.exe");
        }

        private void image_Copy46_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\线程炸弹\线程炸弹.exe");
        }

        private void image_Copy47_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\XIANGQI\xiangqi.exe");
        }

        private void image_Copy48_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\音箱煲箱\音箱煲机软件.exe");
        }

        private void image_Copy49_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\AG视频解析\AG视频解析v4.2.exe");
        }

        private void image_Copy50_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\B站视频下载\B站视频下载工具 3.5.exe");
        }

        private void image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\CPU-Z\CUP-Z.exe");
        }

        private void image1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\GPU-Z\GPU-Z.exe");
        }

        private void image2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\SSD-Z\SSD-Z.exe");
        }

        private void image3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\HDTune\HDTune.exe");
        }

        private void image4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\DisplayX\DisplayX.exe");
        }

        private void image5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\BatteryInfoView\BatteryInfoView.exe");
        }

        private void image6_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\CrystalDiskInfo\DiskInfo64.exe");
        }

        private void image7_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\AS SDD\ASSSD.exe");
        }

        private void image8_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\mouse Test Utility\鼠标单击变双击测试器V2.0.exe");
        }

        private void image9_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\monitorinfo\monitorinfo.exe");
        }

        private void image10_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\Uninstall Tool\Uninstall Tool.exe");
        }

        private void image11_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\AIDA64\aida64.exe");
        }

        private void image12_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\天若OCR开源版V5.0.0\天若OCR文字识别.exe");
        }

        private void image_Copy51_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\天若OCR开源版V5.0.0\天若OCR文字识别.exe");
        }

        private void image_Copy52_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\抓包工具\Fiddler.exe");
        }

        private void image_Copy53_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\SAI\sai.exe");
        }

        private void image_Copy54_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\GIF录制\GifCam.exe");
        }

        private void image_Copy55_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\\PKG解包\WpfApp22.exe");
        }

        private void image_Copy56_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\ASCII码速查\ASCII码速查.exe");
        }

        private void image_Copy57_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\x96dbg\release\x96dbg.exe");
        }

        private void image_Copy58_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\.NET Reactor 5.9.8.0\dotNET_Reactor.exe");
        }

        private void image_Copy59_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\M3U8下载\M3U8 Downloader.exe");
        }

        private void image_Copy60_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\exe打包工具\exe打包工具.exe");
        }

        private void image_Copy61_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\ICO图标转换\ConvertToIco.exe");
        }

        private void image_Copy62_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\MD5校验工具\MD5效验工具.exe");
        }

        private void image_Copy63_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\wPrime\wPrime.exe");
        }

        private void image_Copy64_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\ollyDBG脱壳脚本编辑器\ollyDBG脱壳脚本编辑器.exe");
        }

        private void image_Copy65_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\Procmon-监视进程行为\Procmon-监视进程行为.exe");
        }

        private void image_Copy66_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\RAR密码破解器\AccentRPR_x64.exe");
        }

        private void image_Copy67_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\磁盘空间分析\SpaceSniffer v1.3.0.2_CHS.exe");
        }

        private void image_Copy68_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\防卸载隐藏工具\HideUL.exe");
        }

        private void image_Copy69_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\封包助手\PackAssist.exe");
        }

        private void image_Copy70_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\灰色按钮解锁\灰色按钮解锁.exe");
        }

        private void image_Copy71_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\文件操作监视\文件操作监控.exe");
        }

        private void image_Copy72_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\文件占用解锁\文件解锁器.exe");
        }

        private void image_Copy73_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\吾爱破解\吾爱破解[LCG].exe");
        }

        private void image_Copy74_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\系统字体修改\noMeiryoUI.exe");
        }

        private void image_Copy75_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\图标提取\提取高清图标.exe");
        }

        private void image_Copy76_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\进制转换\进制转换完整版.exe");
        }

        private void image_Copy77_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\h2testw_1.4\h2testw_1.4.exe");
        }

        private void image_Copy78_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\ICON图标转换\ICO图标转换(64位).exe");
        }

        private void image_Copy79_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\文件信息摘要生成\文件信息摘要生成(64位).exe");
        }

        private void image12_Copy_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\ICON图标转换\ICO图标转换(64位).exe");
        }

        private void image12_Copy1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\文件信息摘要生成\文件信息摘要生成(64位).exe");
        }

        private void image12_Copy2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\DiskGeniusPro\DiskGeniusPro.exe");
        }

        private void image12_Copy4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\ICO图标转换\ConvertToIco.exe");
        }

        private void image12_Copy5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Open_exe(Environment.CurrentDirectory + @"\Software catalog\FurMark\FurMark.exe");
        }
    }
}
