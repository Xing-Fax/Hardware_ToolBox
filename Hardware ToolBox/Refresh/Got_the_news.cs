using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardware_ToolBox.Refresh
{
    class Got_the_news
    {
        public static string[] Advanced_ini()
        {
            //初始化检测引擎
            Detection_Core.Sensor_Monitoring.Initial(true, true, true, true, false, false, false, true);
            //获取数据
            string Inf = Detection_Core.Sensor_Monitoring.Core();
            string[] Get_ini = new string[33];
            Get_ini[0] = Operation.String.Substring(Inf, "CPU Total Load ->", "<-");                //得到CPU利用率
            Get_ini[1] = Operation.String.Substring(Inf, "CPU Cores Temperature ->", "<-");         //达到CPU温度
            Get_ini[2] = Operation.String.Substring(Inf, "Memory Load ->", "<-");                   //得到内存利用率
            Get_ini[3] = Operation.String.Substring(Inf, "GPU Core Load ->", "<-");                 //得GPU利用率
            Get_ini[4] = Operation.String.Substring(Inf, "GPU Video Engine Load ->", "<-");         //得到视频引擎利用率
            Get_ini[5] = Operation.String.Substring(Inf, "GPU Core Temperature ->", "<-");          //得到GPU温度
            Get_ini[6] = Operation.String.Substring(Inf, "GPU Memory Load ->", "<-");               //得到显存占用
            Get_ini[7] = Operation.String.Substring(Inf, "GPU Memory Controller Load ->", "<-");    //得到内存控制器利用率

            Get_ini[8] = Operation.String.Substring(Inf, "Vcore Voltage ->", "<-");                 //得到CPU电压
            Get_ini[9] = Operation.String.Substring(Inf, "DIMM Voltage ->", "<-");                  //得到内存电压
            Get_ini[10] = Operation.String.Substring(Inf, "+3.3V Voltage ->", "<-");                //3.3v电压
            Get_ini[11] = Operation.String.Substring(Inf, "+5V Voltage ->", "<-");                  //5v电压
            Get_ini[12] = Operation.String.Substring(Inf, "+12V Voltage ->", "<-");                 //12v电压
            Get_ini[13] = Operation.String.Substring(Inf, "Fan #1 Fan ->", "<-");                   //风扇1
            Get_ini[14] = Operation.String.Substring(Inf, "Fan #2 Fan ->", "<-");                   //风扇2
            Get_ini[15] = Operation.String.Substring(Inf, "Fan #3 Fan ->", "<-");                   //风扇3

            Get_ini[16] = Operation.String.Substring(Inf, "CPU Core #1 Clock ->", "<-");            //CPU主频
            Get_ini[17] = Operation.String.Substring(Inf, "Bus Speed Clock ->", "<-");              //CPU外频
            string Temp = Detection_Core.Other_information.GetProcessCount();                              //获取数据
            Get_ini[18] = Operation.String.Substring(Temp, "process->", "<-");                             //进程数
            Get_ini[19] = Operation.String.Substring(Temp, "Thread->", "<-");                              //线程数
            Get_ini[20] = Operation.String.Substring(Temp, "Handle->", "<-");                              //句柄数
            Get_ini[21] = Detection_Core.Other_information.Run_time();                                     //运行时间
            Get_ini[22] = (Detection_Core.Other_information.Memory_size() / 1048576).ToString();           //内存总量
            Get_ini[23] = (Detection_Core.Other_information.Memory_available() / 1048576).ToString();      //内存可用

            Get_ini[24] = Operation.String.Substring(Inf, "GPU Core Clock ->", "<-");               //GPU主频
            Get_ini[25] = Operation.String.Substring(Inf, "GPU Shader Clock ->", "<-");             //着色器
            Get_ini[26] = Operation.String.Substring(Inf, "GPU Memory Total SmallData ->", "<-");   //显存总量
            Get_ini[27] = Operation.String.Substring(Inf, "GPU Memory Free SmallData ->", "<-");    //显存可用
            Get_ini[28] = Operation.String.Substring(Inf, "GPU Fan Fan ->", "<-");                  //风扇转速
            Get_ini[29] = Operation.String.Substring(Inf, "GPU Fan Control ->", "<-");              //风扇控制比

            Get_ini[30] = Operation.String.Substring(Inf, "VBat Voltage ->", "<-");                 //VBAT电池电压
            Get_ini[31] = Operation.String.Substring(Inf, "Northbridge Voltage ->", "<-");          //北桥电压

            Get_ini[32] = "0";
            if (Get_ini[13] != "0") { Get_ini[32] = "1"; }                                                 //得到风扇数量
            if (Get_ini[14] != "0") { Get_ini[32] = "2"; }
            if (Get_ini[15] != "0") { Get_ini[32] = "3"; }

            return Get_ini;
        }
        /// <summary>
        /// 格式化简要数据
        /// </summary>
        /// <returns>返回数值格式数据</returns>
        public static string[] Brief_ini()
        {
            //初始化检测引擎
            Detection_Core.Sensor_Monitoring.Initial(true, true, true, false, false, false, false, false);
            //获取数据
            string Inf = Detection_Core.Sensor_Monitoring.Core();
            string[] Get_ini = new string[11];
            Get_ini[0] = Operation.String.Substring(Inf, "CPU Total Load ->", "<-");        //得到CPU利用率
            Get_ini[1] = Operation.String.Substring(Inf, "GPU Core Load ->", "<-");         //达到GPU利用率
            Get_ini[2] = Operation.String.Substring(Inf, "Memory Load ->", "<-");           //得到内存利用率
            Get_ini[3] = Operation.String.Substring(Inf, "CPU Core #1 Clock ->", "<-");     //得到CPU主频
            Get_ini[4] = Operation.String.Substring(Inf, "Bus Speed Clock ->", "<-");       //得到CPU倍频
            Get_ini[5] = Operation.String.Substring(Inf, "CPU Cores Temperature ->", "<-"); //得到CPU温度
            Get_ini[6] = Detection_Core.Other_information.Run_time();                       //得到开机时间
            Get_ini[7] = Operation.String.Substring(Inf, "GPU Core Clock ->", "<-");        //得到GPU主频
            Get_ini[8] = Operation.String.Substring(Inf, "GPU Memory Clock ->", "<-");      //得到显存频率
            Get_ini[9] = Operation.String.Substring(Inf, "GPU Shader Clock ->", "<-");      //得到着色器频率
            Get_ini[10] = Operation.String.Substring(Inf, "GPU Core Temperature ->", "<-"); //得到GPU温度

            return Get_ini;                                                                 //返回数据
        }
    }
}
