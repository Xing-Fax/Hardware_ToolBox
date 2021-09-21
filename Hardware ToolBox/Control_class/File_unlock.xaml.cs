using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// File_unlock.xaml 的交互逻辑
    /// </summary>
    public partial class File_unlock : UserControl
    {
        public File_unlock()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 保存选择后的文件路径
        /// </summary>
        static string file = string.Empty;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "所有文件|*.*";
            if (ofd.ShowDialog() == true)
            {
                文件.Text = ofd.FileName;
                file = ofd.FileName;
            }
        }
        /// <summary>
        /// 保存选择后的文件夹路径
        /// </summary>
        static string path = string.Empty;
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;
                dialog.ShowDialog();
                if (dialog.FileName != "")
                {
                    path = dialog.FileName;
                    路径.Text = dialog.FileName;
                }
            }
            catch { }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (file != string.Empty && File.Exists(file))           //判断文件路径不为空且文件存在
                {
                    if (解锁.IsChecked == true)                               //判断要执行的操作
                    {
                        Operation.General.Console(AppDomain.CurrentDomain.BaseDirectory + @"\unlocker.exe", "\"" + file + "\"");
                        hint.window.Pop_ups("操作完成!");
                    }
                    else if (解锁删除.IsChecked == true)
                    {
                        Operation.General.Console(AppDomain.CurrentDomain.BaseDirectory + @"\unlocker.exe", "\"" + file + "\"");
                        Operation.General.Force_delete(file);                   //删除文件
                        hint.window.Pop_ups("操作完成!");
                    }
                    else if (解锁移动.IsChecked == true)
                    {
                        if (path != string.Empty && Directory.Exists(path))                                         //判断路径不为空且路径存在
                        {
                            Operation.General.Console(AppDomain.CurrentDomain.BaseDirectory + @"\unlocker.exe", "\"" + file + "\"");
                            if(Operation.General.CopyFile(file, path + @"\" + System.IO.Path.GetFileName(file)))    //复制文件
                            {
                                Operation.General.Force_delete(file);            //删除文件
                                hint.window.Pop_ups("操作完成!( •ω •)✧");
                            }
                            else
                            {
                                hint.window.Pop_ups("操作失败!(っ °Д °;)っ");
                            }
                        }
                        else
                        {
                            hint.window.Pop_ups("文件不存在!(っ °Д °;)っ");
                        }
                    }
                    else if (解锁复制.IsChecked == true)
                    {
                        if (path != string.Empty && Directory.Exists(path))
                        {
                            Operation.General.Console(AppDomain.CurrentDomain.BaseDirectory + @"\unlocker.exe", "\"" + file + "\"");
                            if (Operation.General.CopyFile(file, path + @"\" + System.IO.Path.GetFileName(file)))
                            {
                                hint.window.Pop_ups("操作完成!( •ω •)✧");
                            }
                            else
                            {
                                hint.window.Pop_ups("操作失败!(＃°Д°)");
                            }
                        }
                        else
                        {
                            hint.window.Pop_ups("文件不存在!o(*￣▽￣*)o");
                        }
                    }
                    else
                    {
                        hint.window.Pop_ups("请选择操作U•ェ•*U");
                    }
                }
                else
                {
                    hint.window.Pop_ups("文件不存在!o(*￣▽￣*)o");
                }
            }
            catch
            {
                 hint.window.Pop_ups("错误代码 -6");
            }
        }

        private void 解锁_Checked(object sender, RoutedEventArgs e)
        {
            if(路径 != null)
                路径.IsEnabled = false;
        }

        private void 解锁删除_Checked(object sender, RoutedEventArgs e)
        {
            if (解锁删除 != null)
                路径.IsEnabled = false;
        }

        private void 解锁移动_Checked(object sender, RoutedEventArgs e)
        {
            if (解锁移动 != null)
                路径.IsEnabled = true;
        }

        private void 解锁复制_Checked(object sender, RoutedEventArgs e)
        {
            if (解锁复制 != null)
                路径.IsEnabled = true;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                if (输入.Text != string.Empty)
                {
                    string str = 输入.Text;
                    if (编码.IsChecked == true)
                    {
                        输出.Text = Convert.ToBase64String(Encoding.Default.GetBytes(str));
                    }
                    else if (解码.IsChecked == true)
                    {
                        输出.Text = Encoding.Default.GetString(Convert.FromBase64String(str));
                    }
                }
                else
                {
                    hint.window.Pop_ups("输入不能为空!(っ °Д °;)っ");
                }
            }
            catch
            {
                hint.window.Pop_ups("转换失败!w(ﾟДﾟ)w");
            }
        }
    }
}
