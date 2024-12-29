using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace VeriketProjesi.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            logYaz();

            timer.Elapsed += (sender, e) => logYaz();
            timer.Interval = 60000; // 60 seconds
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
        }

        public void logYaz()
        {
            string dosyaYolu = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Windows)) + "ProgramData\\VeriketApp";
            //string dosyaYolu = AppDomain.CurrentDomain.BaseDirectory + "logs";
            if (!Directory.Exists(dosyaYolu))
            {
                Directory.CreateDirectory(dosyaYolu);
            }
            //System.IO.StreamWriter dosya = new System.IO.StreamWriter(dosyaYolu, true);
            //string textYolu = AppDomain.CurrentDomain.BaseDirectory + "logs" + "/VeriketApp.txt";
            //string textYolu = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.Windows)).Replace("\\", "\\\\") + "\\ProgramData\\VeriketApp\\VeriketApp.txt";
            string textYolu = dosyaYolu + "\\VeriketApp.txt";


            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            ManagementObjectCollection collection = searcher.Get();
            string username = (string)collection.Cast<ManagementBaseObject>().First()["UserName"];


            if (!File.Exists(textYolu))
            {
                using (StreamWriter dosya = File.CreateText(textYolu))
                {
                    dosya.WriteLine(DateTime.Now + "," + username.Replace("\\", ","));
                }
                //string dosyaYolu = (Environment.GetFolderPath(Environment.SpecialFolder.Windows)).Replace("\\", "\\\\") + "\\ProgramData\\VeriketAppDeneme\\VeriketApp.txt";
            }
            else
            {
                using (StreamWriter dosya = File.AppendText(textYolu))
                {
                    dosya.WriteLine(DateTime.Now + "," + username.Replace("\\", ","));
                }
            }

            //System.Security.Principal.WindowsIdentity.GetCurrent().Name
        }
    }
}
