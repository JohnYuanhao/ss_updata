using System;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Windows服务程序测试01
{
    class FileOpetation
    {
        public static void SaveRecord(string content)
        {
            if (string.IsNullOrEmpty(content)) { return; }
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            { //ApplicationBace在bin文件夹下Debug文件夹，或者Realease文件夹下 
                string path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, string.Format("0:yyyyMMdd", DateTime.Now));
                using (fileStream = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    using (streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.Write(content);
                        if (streamWriter != null)
                        {
                            streamWriter.Close();
                        }
                    }
                }
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            } catch (Exception)
            {
                throw;
            }
        }
    }
} 
            

namespace Windows服务程序测试01
{
    public partial class Service1 : ServiceBase
    {
        System.Threading.Timer recordTimer;
        public Service1()
        {
            //InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            IntialSaveRecord();
        }
        protected override void OnStop()
        {
        }
        private void IntialSaveRecord()
        {
            TimerCallback timerCallback = new TimerCallback(CallbackTask);
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            recordTimer = new System.Threading.Timer(timerCallback, autoEvent, 10000, 60000 * 10);
        }
        private void CallbackTask(object stateInfo)
        {
            FileOpetation.SaveRecord(string.Format(@"当前时间：{0},状况：程序正在运行", DateTime.Now));
        }
    }
} 