using Hardware_ToolBox.Control_class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Hardware_ToolBox.Refresh
{
    public static class Brief
    {
        private static Basic_Information window;
        private static string[] Happening = new string[11];
        private static void Occupancy_rate(object source, System.Timers.ElapsedEventArgs e)
        {
            if (App.select_form == 0)//在界面可见时
            {
                Happening = Got_the_news.Brief_ini();

                for (int i = 0; i < Happening.Length; i++)
                    if (Happening[i] == "" || Happening[i] == null)
                        Happening[i] = "0";

                new Thread(() =>//异步调用
                {
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action(() =>
                        {
                            //写入占用率
                            window.处理器占用.Content = "CPU利用率：" + double.Parse(Happening[0]).ToString("F0") + " %";
                            Operation.Other.Animation(double.Parse(Happening[0]), window.处理器占用条);

                            window.显卡占用.Content = "GPU利用率：" + double.Parse(Happening[1]).ToString("F0") + " %";
                            Operation.Other.Animation(double.Parse(Happening[1]), window.显卡占用条);

                            window.内存占用.Content = "内存利用率：" + double.Parse(Happening[2]).ToString("F0") + " %";
                            Operation.Other.Animation(double.Parse(Happening[2]), window.内存占用条);

                            //写入信息
                            window.CPU信息.Content = double.Parse(Happening[3]).ToString("F0") + " MHz" +
                                        "\n" + double.Parse(Happening[4]).ToString("F0") + " MHz" +
                                        "\n" + double.Parse(Happening[5]).ToString("F0") + " °C" +
                                        "\n" + Happening[6];

                            window.GPU信息.Content = double.Parse(Happening[7]).ToString("F0") + " MHz" +
                                        "\n" + double.Parse(Happening[8]).ToString("F0") + " MHz" +
                                        "\n" + double.Parse(Happening[9]).ToString("F0") + " MHz" +
                                        "\n" + double.Parse(Happening[10]).ToString("F0") + " °C";
                        }));
                }).Start();
            }
        }

        public static void Brief_inf(this Basic_Information main)
        {
            System.Timers.Timer Access_to_information = new System.Timers.Timer(Properties.Settings.Default.刷新时间);
            Access_to_information.Elapsed += new System.Timers.ElapsedEventHandler(Occupancy_rate);
            Access_to_information.AutoReset = true;
            Access_to_information.Enabled = true;
            window = main;
        }
    }
}
