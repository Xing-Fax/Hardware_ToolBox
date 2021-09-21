using Hardware_ToolBox.Control_class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Hardware_ToolBox.Operation
{
    /// <summary>
    /// 初始化类用于修改程序保存设置
    /// </summary>
    public static class Initialization
    {
        /// <summary>
        /// 初始化主题颜色
        /// </summary>
        /// <param name="main"></param>
        public static void Theme(this Personalise main)
        {
            switch (Properties.Settings.Default.主题颜色)
            {
                case 1:
                    main.蓝.IsChecked = true;
                    break;
                case 2:
                    main.红.IsChecked = true;
                    break;
                case 3:
                    main.绿.IsChecked = true;
                    break;
                case 4:
                    main.粉.IsChecked = true;
                    break;
                case 5:
                    main.灰.IsChecked = true;
                    break;
                case 6:
                    main.橙.IsChecked = true;
                    break;
                case 7:
                    main.紫.IsChecked = true;
                    break;
                case 8:
                    main.黑.IsChecked = true;
                    break;
                case 9:
                    main.黄.IsChecked = true;
                    break;
                case 10:
                    main.青.IsChecked = true;
                    break;
            }
        }
        /// <summary>
        /// 初始化背景图片及其填充方式
        /// </summary>
        /// <param name="main"></param>
        public static void Background(this MainWindow main)
        {
            if (Properties.Settings.Default.默认背景图片 != "无")
            {
                if (Properties.Settings.Default.视频还是图片)
                {
                    if (File.Exists(Properties.Settings.Default.默认背景图片))
                    {
                        main.背景图片.Source = new BitmapImage(new Uri(Properties.Settings.Default.默认背景图片));
                    }
                }
                else
                {
                    main.视频背景.Source = new Uri(Properties.Settings.Default.默认背景图片, UriKind.Relative);
                    main.视频背景.Play();
                }
            }

            switch (Properties.Settings.Default.填充方式)
            {
                case 1:
                    main.程序设置.原始.IsChecked = true;
                    break;
                case 2:
                    main.程序设置.填充.IsChecked = true;
                    break;
                case 3:
                    main.程序设置.缩放.IsChecked = true;
                    break;
                case 4:
                    main.程序设置.适应.IsChecked = true;
                    break;
            }
        }

        /// <summary>
        /// 初始化其他设置
        /// </summary>
        /// <param name="main"></param>
        public static void Other_settings(this MainWindow main)
        {
            main.程序设置.背景模糊.Value = Properties.Settings.Default.背景模糊;
            main.程序设置.透明.Value = Properties.Settings.Default.背景不透明;
            main.程序设置.卡片透明.Value = Properties.Settings.Default.卡片不透明;
            App.Transition = Properties.Settings.Default.过渡动画;
            App.Intervals = Properties.Settings.Default.刷新时间;
            main.程序设置.过渡时间条.Value = Properties.Settings.Default.过渡动画 / 10;
            main.程序设置.刷新间隔条.Value = Properties.Settings.Default.刷新时间 / 10;

            main.程序设置.重启.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 将部分设置还原到默认值
        /// </summary>
        public static void Default(this MainWindow main)
        {
            main.程序设置.刷新间隔条.Value = 100;
            main.程序设置.过渡时间条.Value = 30;
            main.程序设置.卡片透明.Value = 70;
            main.程序设置.透明.Value = 0;
            main.程序设置.背景模糊.Value = 0;
            Properties.Settings.Default.Save();
            main.小提示信息.Content = "还原完毕！";
        }

        /// <summary>
        /// 将全部设置还原到默认值
        /// </summary>
        public static void All_Default(this MainWindow main)
        {
            main.程序设置.刷新间隔条.Value = 100;
            main.程序设置.过渡时间条.Value = 30;
            main.程序设置.卡片透明.Value = 70;
            main.程序设置.透明.Value = 0;
            main.程序设置.背景模糊.Value = 0;
            main.程序设置.蓝.IsChecked = true;
            main.程序设置.适应.IsChecked = true;
            if (Properties.Settings.Default.视频还是图片 == false)
                main.视频背景.Source = null;
            main.背景图片.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/背景.jpg"));
            Properties.Settings.Default.默认背景图片 = "无";
            Properties.Settings.Default.视频还是图片 = true;
            Properties.Settings.Default.默认背景图片 = "无";
            Properties.Settings.Default.Save();
            main.小提示信息.Content = "还原完毕！";
        }

    }
}
