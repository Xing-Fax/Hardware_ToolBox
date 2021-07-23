using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hardware_ToolBox
{
    /// <summary>
    /// Initial_screen.xaml 的交互逻辑
    /// </summary>
    public partial class Initial_screen : Window
    {
        public Initial_screen()
        {
            InitializeComponent();
            ///int sum = 1000;
            //Thread.Sleep(sum);
            加载.Text += "\n校验安全证书...";
            //Thread.Sleep(sum);
            加载.Text += "\n校验程序是否被篡改...";
            //Thread.Sleep(sum);
            加载.Text += "\n启动硬件检测引擎...";
            //Thread.Sleep(sum);
            加载.Text += "\n后台检测程序启动...";
            //Thread.Sleep(sum);
            加载.Text += "\n检测计算机基本配置信息...";
            //Thread.Sleep(sum);
            加载.Text += "\n检测系统运行状态...";
            //Thread.Sleep(sum);
            加载.Text += "\n开始写入硬件配置...";
            //Thread.Sleep(sum);
            加载.Text += "\n删除临时文件...";
            //Thread.Sleep(sum);
            加载.Text += "\n启动用户图形化界面...";
        }

        private void Window_Activated(object sender, EventArgs e)
        {


        }
    }
}
