using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hardware_ToolBox.Operation
{
    class Check
    {
        private static void Verification_procedure(object sender, DoWorkEventArgs e)
        {
            //校验文件签名

            string[] file_list = new string[7];

            file_list[0] = Environment.CurrentDirectory + "/Security certificate installation/Certificate installation.exe";
            file_list[1] = Process.GetCurrentProcess().MainModule.FileName;
            file_list[2] = Environment.CurrentDirectory + "/System memory information.dll";
            file_list[3] = Environment.CurrentDirectory + "/NvAPIWrapper.dll";
            file_list[4] = Environment.CurrentDirectory + "/LibreHardwareMonitorLib.dll";
            file_list[5] = Environment.CurrentDirectory + "/Configuration information.exe";
            file_list[6] = Environment.CurrentDirectory + "/unlocker.exe";

            for (int i = 0; i < file_list.Length; i++)
                if (!Other.Document_verification(file_list[i]))
                    Environment.Exit(0);

            //安全证书检测程序
            X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
            store.Open(OpenFlags.MaxAllowed);
            X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindByThumbprint, "36a888b9f2a505bf92ac6b2796c2188e639ab1d1", false);
            if (certs.Count == 0 || certs[0].NotAfter < DateTime.Now)
            {   //如果不存在自动启动证书安装程序
                Process.Start(Environment.CurrentDirectory + "/Security certificate installation/Certificate installation.exe");
            }
            App.Check_Normal = true;
        }

        /// <summary>
        /// 程序初始化校验防止被修改
        /// </summary>
        public static void procedure()
        {
            //初始化程序
            using (BackgroundWorker bw = new BackgroundWorker())
            {
                bw.DoWork += new DoWorkEventHandler(Verification_procedure);
                bw.RunWorkerAsync();
            }
        }
    }
}
