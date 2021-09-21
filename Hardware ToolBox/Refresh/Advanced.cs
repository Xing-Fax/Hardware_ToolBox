using Hardware_ToolBox.Control_class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Hardware_ToolBox.Refresh
{
    public static class Advanced
    {

        #region //设定最大最小值
        static float max3 = 0;
        static float min3 = 99999;

        static float max5 = 0;
        static float min5 = 99999;

        static float max12 = 0;
        static float min12 = 99999;

        static float max_fan1 = 0;
        static float min_fan1 = 99999;

        static float max_fan2 = 0;
        static float min_fan2 = 99999;

        static float max_fan3 = 0;
        static float min_fan3 = 99999;
        #endregion
        private static System_monitoring window;
        private static string[] Happening = new string[33];
        private static void Occupancy_rate(object source, System.Timers.ElapsedEventArgs e)
        {
            if (App.select_form == 3)//在界面可见时
            {
                Happening = Got_the_news.Advanced_ini();//获取数据

                for(int i =0;i < Happening .Length;i ++)
                    if (Happening[i] == "" || Happening[i] == null )
                        Happening[i] = "0";

                new Thread(() =>//异步调用
                {
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action(() =>
                        {
                            //写入占用率

                            window.处理器占用2.Content = "CPU利用率：      " + double.Parse(Happening[0]).ToString("F1") + " %";
                            Operation.Other.Animation(double.Parse(Happening[0]), window.处理器占用条2);

                            window.处理器温度2.Content = "CPU温度：          " + double.Parse(Happening[1]).ToString("F1") + " °";
                            Operation.Other.Animation(double.Parse(Happening[1]), window.CPU温度2);

                            window.RAM利用率2.Content = "RAM利用率：     " + double.Parse(Happening[2]).ToString("F1") + " %";
                            Operation.Other.Animation(double.Parse(Happening[2]), window.RAM利用率条2);

                            window.显卡占用2.Content = "渲染引擎：          " + double.Parse(Happening[3]).ToString("F1") + " %";
                            Operation.Other.Animation(double.Parse(Happening[3]), window.显卡利用率条);

                            window.视频引擎.Content = "视频引擎：          " + double.Parse(Happening[4]).ToString("F1") + " %";
                            Operation.Other.Animation(double.Parse(Happening[4]), window.视频引擎条);

                            window.显卡温度.Content = "核心温度：          " + double.Parse(Happening[5]).ToString("F1") + " °";
                            Operation.Other.Animation(double.Parse(Happening[5]), window.显卡温度条);

                            window.显卡显存利用率.Content = "显存占用：          " + double.Parse(Happening[6]).ToString("F1") + " %";
                            Operation.Other.Animation(double.Parse(Happening[6]), window.显卡显存条);

                            window.帧缓存器利用率.Content = "显存控制器：      " + double.Parse(Happening[7]).ToString("F1") + " %";
                            Operation.Other.Animation(double.Parse(Happening[7]), window.帧缓存器条);

                            window.CPU电压.Content = "CPU电压：          " + double.Parse(Happening[8]).ToString("F3") + " V";
                            Operation.Other.Animation(double.Parse(Happening[8]) * 10, window.CPU电压条);

                            window.内存电压.Content = "内存电压：         " + double.Parse(Happening[9]).ToString("F3") + " V";
                            Operation.Other.Animation(double.Parse(Happening[9]) * 10, window.内存电压条);

                            window.电压3.Content = "+ 3.3 V电压：      " + double.Parse(Happening[10]).ToString("F3") + " V";
                            Operation.Other.Animation(double.Parse(Happening[10]) * 10, window.电压3条);

                            if (float.Parse(Happening[10]) > max3)
                                max3 = float.Parse(Happening[10]);
                            if (float.Parse(Happening[10]) < min3)
                                min3 = float.Parse(Happening[10]);

                            window.电压5.Content = "+ 5 V电压：         " + double.Parse(Happening[11]).ToString("F3") + " V";
                            Operation.Other.Animation(double.Parse(Happening[11]) * 10, window.电压5条);
                            if (float.Parse(Happening[11]) > max5)
                                max5 = float.Parse(Happening[11]);
                            if (float.Parse(Happening[11]) < min5)
                                min5 = float.Parse(Happening[11]);

                            window.电压12.Content = "+ 12 V电压：       " + double.Parse(Happening[12]).ToString("F3") + " V";
                            Operation.Other.Animation(double.Parse(Happening[12]) * 10, window.电压12条);
                            if (float.Parse(Happening[12]) > max12)
                                max12 = float.Parse(Happening[12]);
                            if (float.Parse(Happening[12]) < min12)
                                min12 = float.Parse(Happening[12]);

                            window.风扇1.Content = "风扇一：      " + double.Parse(Happening[13]).ToString("F1") + " RPM";
                            Operation.Other.Animation(double.Parse(Happening[13]) / 10, window.风扇1条);
                            if (float.Parse(Happening[13]) > max_fan1)
                                max_fan1 = float.Parse(Happening[13]);
                            if (float.Parse(Happening[13]) < min_fan1)
                                min_fan1 = float.Parse(Happening[13]);

                            window.风扇2.Content = "风扇二：      " + double.Parse(Happening[14]).ToString("F1") + " RPM";
                            Operation.Other.Animation(double.Parse(Happening[14]) / 10, window.风扇2条);
                            if (float.Parse(Happening[14]) > max_fan2)
                                max_fan2 = float.Parse(Happening[14]);
                            if (float.Parse(Happening[14]) < min_fan2)
                                min_fan2 = float.Parse(Happening[14]);

                            window.风扇3.Content = "风扇三：      " + double.Parse(Happening[15]).ToString("F1") + " RPM";
                            Operation.Other.Animation(double.Parse(Happening[15]) / 10, window.风扇3条);
                            if (float.Parse(Happening[15]) > max_fan3)
                                max_fan3 = float.Parse(Happening[15]);
                            if (float.Parse(Happening[15]) < min_fan3)
                                min_fan3 = float.Parse(Happening[15]);

                            //写入信息
                            window.CPU参数2.Content = double.Parse(Happening[16]).ToString("F2") + " MHz" +
                                     "\n" + double.Parse(Happening[17]).ToString("F2") + " MHz";
                            window.CPU参数3.Content = Happening[18] + " 个" +
                                    "\n" + Happening[19] + " 个";
                            window.CPU参数4.Content = Happening[20] + " 个" +
                                    "\n" + Happening[21].Replace(".", ":");
                            window.CPU参数5.Content = Happening[22] + " MB" +
                                    "\n" + Happening[23] + " MB";
                            window.GPU参数2.Content = double.Parse(Happening[24]).ToString("F2") + " MHz" +
                                    "\n" + double.Parse(Happening[25]).ToString("F2") + " MHz";
                            window.GPU参数3.Content = double.Parse(Happening[26]).ToString("F2") + " MB" +
                                    "\n" + double.Parse(Happening[27]).ToString("F2") + " MB";
                            window.GPU参数4.Content = double.Parse(Happening[28]).ToString("F2").Replace("-1.00", "-1") + " RPM" +
                                    "\n" + double.Parse(Happening[29]).ToString("F2") + " %";

                            window.风扇参数1.Content = Happening[32] + " 个";

                            window.风扇参数2.Content = max_fan1.ToString("F1") + " RPM" + "\n" + min_fan1.ToString("F1") + " RPM";
                            window.风扇参数3.Content = max_fan2.ToString("F1") + " RPM" + "\n" + min_fan2.ToString("F1") + " RPM";
                            window.风扇参数4.Content = max_fan3.ToString("F1") + " RPM" + "\n" + min_fan3.ToString("F1") + " RPM";

                            window.电压信息1.Content = max12.ToString("F3") + " V\n" + min12.ToString("F3") + " V";
                            window.电压信息2.Content = max5.ToString("F3") + " V\n" + min5.ToString("F3") + " V";
                            window.电压信息3.Content = max3.ToString("F3") + " V\n" + min3.ToString("F3") + " V";
                            window.电压信息4.Content = Happening[30] + " V\n" + Happening[31] + " V";
                        }));
                }).Start();
            }
        }

        public static void Advanced_inf(this System_monitoring main)
        {
            System.Timers.Timer Access_to_information = new System.Timers.Timer(Properties.Settings.Default.刷新时间);
            Access_to_information.Elapsed += new System.Timers.ElapsedEventHandler(Occupancy_rate);
            Access_to_information.AutoReset = true;
            Access_to_information.Enabled = true;
            window = main;
        }
    }
}
