using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Hardware_ToolBox.Control_class
{
    /// <summary>
    /// About_interface.xaml 的交互逻辑
    /// </summary>
    public partial class About_interface : UserControl
    {
        public About_interface()
        {
            InitializeComponent();
        }

        private void 开源许可_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.gnu.org/licenses/old-licenses/gpl-2.0.html");
        }

        private void 许可协议_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("User License Agreement.txt");
        }

        private void PackIcon_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/xingchuanzhen/Hardware_ToolBox");
        }

        private void 捐赠_Click(object sender, RoutedEventArgs e)
        {
            if (image.Visibility == Visibility.Visible)
            {
                名称.Text  = "捐赠";
                BeginStoryboard((Storyboard)FindResource("捐赠FOO"));
            }

            if (image.Visibility == Visibility.Collapsed)
            {
                名称.Text  = "取消";
                BeginStoryboard((Storyboard)FindResource("捐赠ON"));
            }
        }
    }
}
