using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hardware_ToolBox.Control_class
{
    /// <summary>
    /// Basic_Information.xaml 的交互逻辑
    /// </summary>
    public partial class Basic_Information : UserControl
    {
        public Basic_Information()
        {
            InitializeComponent();
            Refresh.Brief.Brief_inf(this);              //检测运行状态
            Operation.Hardware_INF.Traverse_inf(this);  //检测配置信息
        }

        private void 关闭1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(电脑基本信息.Text);
            hint.window.Pop_ups("复制成功ˋ( °▽、°)");
        }

        private void 关闭2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(Operation.String.Combine_strings(操作系统信息名称.Text.Split('：'), 操作系统信息.Text.Split('\n')));
            hint.window.Pop_ups("复制成功ˋ( °▽、°)~");
        }

        private void 关闭4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(Operation.String.Combine_strings(中央处理器信息名称.Text.Split('：'), 中央处理器信息.Text.Split('\n')));
            hint.window.Pop_ups("复制成功ˋ( °▽、°)~");
        }

        private void 关闭3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(Operation.String.Combine_strings(显卡显示器信息名称.Text.Split('：'), 显卡显示器信息.Text.Split('\n')));
            hint.window.Pop_ups("复制成功ˋ( °▽、°)~");
        }

        private void 关闭5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(Operation.String.Combine_strings(bios信息名称.Text.Split('：'), bios信息.Text.Split('\n')));
            hint.window.Pop_ups("复制成功ˋ( °▽、°)~");
        }
    }
}
