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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hardware_ToolBox.Control_class
{
    /// <summary>
    /// System_monitoring.xaml 的交互逻辑
    /// </summary>
    public partial class System_monitoring : UserControl
    {
        public System_monitoring()
        {
            InitializeComponent();
            Refresh.Advanced.Advanced_inf(this);      //检测机器运行状态
        }
    }
}
