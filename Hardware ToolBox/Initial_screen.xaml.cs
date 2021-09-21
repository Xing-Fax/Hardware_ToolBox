using Hardware_ToolBox.Control_class;
using System;
using System.Windows;
using System.Windows.Data;

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
            加载.Text += "校验主程序完整性...\n";
            Operation.Check.procedure();                            //校验程序，防止修改
        }
    }
}
