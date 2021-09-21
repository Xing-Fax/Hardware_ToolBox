using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Hardware_ToolBox.Control_class
{
    /// <summary>
    /// All_information.xaml 的交互逻辑
    /// </summary>
    public partial class All_information : UserControl
    {
        public All_information()
        {
            InitializeComponent();
            Operation.All_Hardware.Traverse_inf(this);  //检测配置信息
        }

        private void 导出配置报告_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sf1 = new SaveFileDialog();
            sf1.Title = "保存导出文件";
            sf1.FileName = "设备配置信息导出.txt";
            sf1.Filter = "txt文档|*.txt";
            if (sf1.ShowDialog() == true)
            {
                if (Operation.Export_inf.Configuration_report(hint.window, sf1.FileName))
                {
                    hint.window.Pop_ups("导出完毕！");
                }
                else
                {
                    hint.window.Pop_ups("错误代码 -5");
                }
            }
        }

        private void 处理器复制_MouseUp(object sender, MouseButtonEventArgs e)
        {
            hint.window.Pop_ups("复制成功( •ω• )✧");

            Clipboard.SetText(其他信息1.Text);
        }

        private void 主机板_MouseUp(object sender, MouseButtonEventArgs e)
        {

            hint.window.Pop_ups("复制成功( •ω• )✧");
            Clipboard.SetText(其他信息2.Text);
        }

        private void 磁盘复制_MouseUp(object sender, MouseButtonEventArgs e)
        {

            hint.window.Pop_ups("复制成功( •ω• )✧");
            Clipboard.SetText(其他信息3.Text);
        }


        private void 物理内存_MouseUp(object sender, MouseButtonEventArgs e)
        {

            hint.window.Pop_ups("复制成功( •ω• )✧");
            Clipboard.SetText(其他信息4.Text);
        }


        private void 网卡_MouseUp(object sender, MouseButtonEventArgs e)
        {

            hint.window.Pop_ups("复制成功( •ω• )✧");
            Clipboard.SetText(其他信息5.Text);
        }

        private void 逻辑分区_MouseUp(object sender, MouseButtonEventArgs e)
        {

            hint.window.Pop_ups("复制成功( •ω• )✧");
            Clipboard.SetText(其他信息6.Text);
        }

        private void 操作系统复制_MouseUp(object sender, MouseButtonEventArgs e)
        {

            hint.window.Pop_ups("复制成功( •ω• )✧");
            Clipboard.SetText(其他信息7.Text);
        }

        private void 系统BIOS_MouseUp(object sender, MouseButtonEventArgs e)
        {

            hint.window.Pop_ups("复制成功( •ω• )✧");
            Clipboard.SetText(其他信息8.Text);
        }

        private void 监视器_MouseUp(object sender, MouseButtonEventArgs e)
        {

            hint.window.Pop_ups("复制成功( •ω• )✧");
            Clipboard.SetText(其他信息9.Text);
        }

        private void 显卡复制_MouseUp(object sender, MouseButtonEventArgs e)
        {

            hint.window.Pop_ups("复制成功( •ω• )✧");
            Clipboard.SetText(其他信息10.Text);
        }

        private void 声音卡_MouseUp(object sender, MouseButtonEventArgs e)
        {

            hint.window.Pop_ups("复制成功( •ω• )✧");
            Clipboard.SetText(其他信息11.Text);
        }
    }
}
