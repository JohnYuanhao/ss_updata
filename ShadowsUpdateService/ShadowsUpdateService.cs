using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace ShadowsUpdateService
{
    public partial class ShadowsUpdateService : ServiceBase
    {
      

        /// <summary>
        /// 启动所有dll资源文件
        /// </summary>
        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");
            dllName = dllName.Replace(".", "_");
            if (dllName.EndsWith("_resources")) return null;
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("ShadowsUpdateService.Resources", System.Reflection.Assembly.GetExecutingAssembly());
            byte[] bytes = (byte[])rm.GetObject(dllName);
            return System.Reflection.Assembly.Load(bytes);
        }

        private Timer time;
        public ShadowsUpdateService()
        {
            InitializeComponent();
            time = new Timer();
            //a = new Ini(@"C:\Users\John\Desktop\1\setting.ini");
            time.AutoReset = true;
            time.Enabled = true;
            time.Interval = 43200000;
            time.Elapsed += Time_Elapsed;
           
        }

        private void Time_Elapsed(object sender, ElapsedEventArgs e)
        {

            //a.Writue("Message" + i, "Say", i++.ToString());

        }

        protected override void OnStart(string[] args)
        {
            time.Start();
            
        }

        protected override void OnStop()
        {
            time.Enabled = false;
            time.Stop();
        }

        private void start_update() {
            active ac = new active();
            try
            {
                shadowsocks8_data sh8 = new shadowsocks8_data();
                for (int i = 0; i < sh8.Shadowsocks8_ssr.Length; i++)
                {
                    if (active.write(sh8.Shadowsocks8_ssr[i][0], sh8.Shadowsocks8_ssr[i][1]) == 0)
                        break;
                    else
                    {
                        //Console.WriteLine("Shadowsocks8:已添加" + (i + 1) + "条节点");//节点名称
                        //System.Threading.Thread.Sleep(500);
                    }
                }
            }
            catch { }

        }
    }
}
