using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Hardware_ToolBox.Control_class;

namespace Hardware_ToolBox
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Timers.Timer Initialization = new System.Timers.Timer(1000);     //检测是否初始化完毕
        readonly Initial_screen screen = new Initial_screen();                  //起始界面
        public MainWindow()
        {
            InitializeComponent();
            screen.Show();                                                      //启动起始界面
            主窗体.Visibility = Visibility.Hidden;                              //隐藏主界面   
            hint.Hint(this);                                                    //传递对象

            for (int i = 1;i <= 10;i ++)                                        //对所有子界面进行隐藏
            {
                object o = GetType().GetField("界面" + i.ToString(), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
                ((Grid)o).Visibility = Visibility.Collapsed;
            }
            screen.加载.Text += "初始化个性设置...\n";
            screen.加载.Text += "程序初始化...\n";
            Operation.Initialization.Background(this);              //初始化背景
            screen.加载.Text += "初始化程序设置...\n";
            Operation.Initialization.Other_settings(this);          //初始化其他设置
            screen.加载.Text += "开始检测设备配置信息...\n";

            Initialization.Elapsed += new System.Timers.ElapsedEventHandler(Initialization_Process);
            Initialization.Enabled = true;

            Binding binding = new Binding();                        //创建数据绑定
            binding.Source = 起始界面.主要的;                       //源对象
            binding.Path = new PropertyPath(OpacityProperty);       //源属性
            文件解锁.背景.SetBinding(OpacityProperty, binding);     //目标对象
            打开软件.背景.SetBinding(OpacityProperty, binding);
            关于界面.背景.SetBinding(OpacityProperty, binding);
            程序设置.背景.SetBinding(OpacityProperty, binding);
            监控面板.背景.SetBinding(OpacityProperty, binding);
            更多信息.背景.SetBinding(OpacityProperty, binding);
        }

        private void Initialization_Process(object source, System.Timers.ElapsedEventArgs e)
        {
            if (App.Con_inf == true && App.Check_Normal == true)                                        //确定信息获取完毕
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    screen.Close();                                                                     //关闭起始界面
                    BeginStoryboard((Storyboard)FindResource("界面" + (App.select_form + 1) + "打开")); //显示一个指定界面
                    BeginStoryboard((Storyboard)FindResource("程序启动"));                              //显示窗体
                }));
                Initialization.Close();                                                                 //释放计时器
            }
        }

        /// <summary>
        /// 打开提示信息
        /// </summary>
        /// <param name="str">提示信息内容</param>
        public void Pop_ups(string str )                                              //打开提示信息
        {
            小提示信息.Content = str;
            BeginStoryboard((Storyboard)FindResource("提示打开"));
        }

        private void 标题栏_MouseMove(object sender, MouseEventArgs e)                //拖动窗体
        {
            if (e.LeftButton == MouseButtonState.Pressed){ DragMove();}
        }

        private void 黑幕_MouseUp(object sender, MouseButtonEventArgs e)              //通过单击黑色遮罩关闭菜单
        {
            if (黑幕.Opacity == 1) { BeginStoryboard((Storyboard)FindResource("菜单关闭")); }
        }

        private void 最小化_MouseUp(object sender, MouseButtonEventArgs e)            //最小化程序
        {
            WindowState = WindowState.Minimized;
        }

        private void 菜单打开_Click(object sender, RoutedEventArgs e)                 //打开菜单
        {
            BeginStoryboard((Storyboard)FindResource("菜单打开"));
        }

        private void 菜单关闭_Click(object sender, RoutedEventArgs e)                 //关闭菜单
        {
            BeginStoryboard((Storyboard)FindResource("菜单关闭"));
        }

        private void 视频背景_MediaEnded(object sender, RoutedEventArgs e)            //循环播放视频
        {
            视频背景.Stop();
            视频背景.Play();
        }

        private void 关闭_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Detection_Core.Sensor_Monitoring.Release_resources();   //释放检测器所使用资源
            主窗体.Visibility = Visibility.Collapsed;               //隐藏主窗体
            Properties.Settings.Default.Save();                     //保存设置
            Environment.Exit(0);                                    //关闭所有进程(向系统返回0)
        }

        private void 选项卡_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (选项卡.SelectedIndex != App.select_form && 选项卡.SelectedIndex != -1)                      //不为当前界面和空的选中
            {
                BeginStoryboard((Storyboard)FindResource("菜单关闭"));                                      //关闭菜单
                BeginStoryboard((Storyboard)FindResource("界面" + (App.select_form + 1) + "关闭"));         //关闭当前显示的界面
                BeginStoryboard((Storyboard)FindResource("界面" + (选项卡.SelectedIndex + 1) + "打开"));    //打开选中的界面   
                提示信息.Content = Operation.String.caption(选项卡.SelectedIndex);                          //设置标题文字
                App.select_form = 选项卡.SelectedIndex;                                                     //记录选择界面
            }
        }
    }
}