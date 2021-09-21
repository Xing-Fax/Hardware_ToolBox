using Microsoft.Win32;
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
    /// Personalise.xaml 的交互逻辑
    /// </summary>
    public partial class Personalise : UserControl
    {
        public Personalise()
        {
            InitializeComponent();
            Operation.Initialization.Theme(this);    //加载程序设置
        }
        private void 选择背景_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "图像文件|*.jpg;*.png;*.bmp;*.jpeg";
                if (ofd.ShowDialog() == true)
                {
                    if (Properties.Settings.Default.视频还是图片 == false)
                        hint.window.视频背景.Source = null;

                    hint.window.背景图片.Source = new BitmapImage(new Uri(ofd.FileName));
                    Properties.Settings.Default.默认背景图片 = ofd.FileName;//偷偷记下它选择的图片背景路径，方便下一次启动使用
                    Properties.Settings.Default.视频还是图片 = true;
                }
            }
            catch
            {
                hint.window.Pop_ups("错误代码-3");
            }
        }

        private void 选择视频_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "视频文件|*.mp4;*.wai;*.wmv";
                if (ofd.ShowDialog() == true)
                {
                    hint.window.视频背景.Source = new Uri(ofd.FileName, UriKind.Relative);
                    Properties.Settings.Default.默认背景图片 = ofd.FileName;//偷偷记下它选择的图片背景路径，方便下一次启动使用
                    Properties.Settings.Default.视频还是图片 = false;
                    hint.window.视频背景.Play();
                }
            }
            catch
            {
                hint.window.Pop_ups("错误代码-3");
            }
        }

        private void 背景模糊_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (背景模糊度 != null)
            {
                背景模糊度.Content = "背景模糊程度：" + (背景模糊.Value * 2).ToString("F1") + " %";
                hint.window.模糊.Radius = 背景模糊.Value * 2;
                hint.window.模糊2.Radius = 背景模糊.Value * 2;
                Properties.Settings.Default.背景模糊 = 背景模糊.Value;
            }
        }

        private void 透明_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (透明度 != null)
            {
                透明度.Content = "背景不透明度：" + 透明.Value.ToString("F1") + " %";
                hint.window.背景不透明.Fill.Opacity = 透明.Value / 100;
                Properties.Settings.Default.背景不透明 = 透明.Value;
            }
        }

        private void 卡片透明_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (卡片透明度 != null)
            {
                卡片透明度.Content = "卡片不透明度：" + 卡片透明.Value.ToString("F1") + " %";
                hint.window.起始界面.主要的.Opacity = 卡片透明.Value / 100;
                Properties.Settings.Default.卡片不透明 = 卡片透明.Value;
            }
        }

        private void 过渡时间条_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (过渡时间 != null)
            {
                过渡时间.Content = "状态条过渡动画时间：   " + (过渡时间条.Value * 10).ToString("F0") + " 毫秒";
                Properties.Settings.Default.过渡动画 = double.Parse((过渡时间条.Value * 10).ToString("F0"));
                重启.Visibility = Visibility.Visible;
            }
        }

        private void 刷新间隔条_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (刷新间隔 != null)
            {
                刷新间隔.Content = "硬件信息刷新间隔： " + (刷新间隔条.Value * 10).ToString("F0") + " 毫秒";
                Properties.Settings.Default.刷新时间 = double.Parse((刷新间隔条.Value * 10).ToString("F0"));
                重启.Visibility = Visibility.Visible;
            }
        }

        private void 原始_Checked(object sender, RoutedEventArgs e)
        {
            hint.window.背景图片.Stretch = Stretch.None;
            Properties.Settings.Default.填充方式 = 1;
        }

        private void 填充_Checked(object sender, RoutedEventArgs e)
        {
            hint.window.背景图片.Stretch = Stretch.Uniform;
            Properties.Settings.Default.填充方式 = 2;
        }

        private void 缩放_Checked(object sender, RoutedEventArgs e)
        {
            hint.window.背景图片.Stretch = Stretch.Fill;
            Properties.Settings.Default.填充方式 = 3;
        }

        private void 适应_Checked(object sender, RoutedEventArgs e)
        {
            hint.window.背景图片.Stretch = Stretch.UniformToFill;
            Properties.Settings.Default.填充方式 = 4;
        }
        #region //标准颜色值
        //绿 #FF2CCFA0
        //红 #FFED556A
        //蓝 #FF10AEC2
        //粉 #FFFF9999
        //橙 #FFFF5722
        //紫 #FF673AB7
        //黑 #FF323232
        //黄 #FFE07629
        //青 #FF009688
        #endregion
        private void 蓝_Checked(object sender, RoutedEventArgs e)
        {
            Operation.Other.Theme("#FF10AEC2");
            Properties.Settings.Default.主题颜色 = 1;
        }

        private void 红_Checked(object sender, RoutedEventArgs e)
        {
            Operation.Other.Theme("#FFED556A");
            Properties.Settings.Default.主题颜色 = 2;
        }

        private void 绿_Checked(object sender, RoutedEventArgs e)
        {
            Operation.Other.Theme("#FF2CCFA0");
            Properties.Settings.Default.主题颜色 = 3;
        }

        private void 粉_Checked(object sender, RoutedEventArgs e)
        {
            Operation.Other.Theme("#FFFF9999");
            Properties.Settings.Default.主题颜色 = 4;
        }

        private void 灰_Checked(object sender, RoutedEventArgs e)
        {
            Operation.Other.Theme("#FF9E9E9E");
            Properties.Settings.Default.主题颜色 = 5;
        }

        private void 橙_Checked(object sender, RoutedEventArgs e)
        {
            Operation.Other.Theme("#FFFF5722");
            Properties.Settings.Default.主题颜色 = 6;
        }

        private void 紫_Checked(object sender, RoutedEventArgs e)
        {
            Operation.Other.Theme("#FF673AB7");
            Properties.Settings.Default.主题颜色 = 7;
        }

        private void 黑_Checked(object sender, RoutedEventArgs e)
        {
            Operation.Other.Theme("#FF323232");
            Properties.Settings.Default.主题颜色 = 8;
        }

        private void 黄_Checked(object sender, RoutedEventArgs e)
        {
            Operation.Other.Theme("#FFE07629");
            Properties.Settings.Default.主题颜色 = 9;
        }

        private void 青_Checked(object sender, RoutedEventArgs e)
        {
            Operation.Other.Theme("#FF009688");
            Properties.Settings.Default.主题颜色 = 10;
        }

        private void 默认_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.视频还是图片 == false)
                hint.window.视频背景.Source = null;
            hint.window.背景图片.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/背景.jpg"));
            Properties.Settings.Default.默认背景图片 = "无";
        }

        private void 默认设置2_Click(object sender, RoutedEventArgs e)
        {
            Operation.Initialization.All_Default(hint.window);
            hint.window.Pop_ups("设置成功(ง •_•)ง");
        }

        private void 默认设置_Click(object sender, RoutedEventArgs e)
        {
            Operation.Initialization.Default(hint.window);
            hint.window.Pop_ups("设置成功(ง •_•)ง");
        }
    }
}
