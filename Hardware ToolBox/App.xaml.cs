using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace Hardware_ToolBox
{
    public static class hint
    {
        public static MainWindow window;
        public static void Hint(MainWindow main)
        {
            window = main;
        }
    }
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static double Transition;            //动画过渡时间
        public static double Intervals;             //检测信息间隔时间
        public static bool Con_inf = false;         //决定配置信息监测是否完成
        public static bool Check_Normal = false;    //决定校验程序是否正常
        public static int select_form = 0;          //指定起始页和当前所在页面
    }
}

//string[] str = new string[2];
//str[0] = "0";
//str[1] = "1";
//str[2] = "2";


//string[] str = new string[2];
//str[0] = ((string[])e.Argument)[0];
//str[1] = ((string[])e.Argument)[1];