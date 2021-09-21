using System;
using System.Collections.Generic;
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
    /// Little_easter_egg.xaml 的交互逻辑
    /// </summary>
    public partial class Little_easter_egg : UserControl
    {
        public Little_easter_egg()
        {
            InitializeComponent();
        }

        private void 菜单背景_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (小方块.Opacity == 0)
            {
                BeginStoryboard((Storyboard)FindResource("小人跳"));

                金币.Content = "节操 + " + (int.Parse(金币.Content.ToString().Replace("节操 + ", "").Replace("...", "")) + 1) + "...";
            }
        }
    }
}
