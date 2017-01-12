using Newtonsoft.Json;
using Shadowsocks.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Web;

using System.Reflection;
using ss_updata.ss_updata_src;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace ss_updata
{
    class Program
    {


        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");
            dllName = dllName.Replace(".", "_");
            if (dllName.EndsWith("_resources")) return null;
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("ss_updata.Resources", System.Reflection.Assembly.GetExecutingAssembly());
            byte[] bytes = (byte[])rm.GetObject(dllName);
            return System.Reflection.Assembly.Load(bytes);
        }

        private static string CONFIG_FILE = "gui-config.json";//配置文件

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            
            Console.Title = "SS免费节点更新中...";//设置窗口标题
            Console.WriteLine("注意：");
            Console.WriteLine("      1.将脚本跟shadowsocks放在同级目录中使用");
            Console.WriteLine("      2.如果并未成功却提示成功添加，请使用管理员权限打开");
            Console.WriteLine("更新节点均取自www.shadowsocks8.net每日免费节点,在此特别鸣谢");
            Console.WriteLine("免费节点持续添加中，静待更新");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("正在获取Shadowsocks8节点");
            shadowsocks8 sh8 = new shadowsocks8();
            //int num = dob.Doub_ssr.Count + sh8.Shadowsocks8_ssr.Length;
            for (int i=0; i < sh8.Shadowsocks8_ssr.Length; i++)
            {
                if (write(sh8.Shadowsocks8_ssr[i][0],sh8.Shadowsocks8_ssr[i][1]) == 0)
                    break;
                else
                {
                    Console.WriteLine("Shadowsocks8:已添加" + (i + 1) + "条节点");//节点名称
                    System.Threading.Thread.Sleep(500);
                }
            }
            Console.WriteLine("正在获取doub.io节点");
            doub dob = new doub();
            for (int i = 0; i < dob.Doub_ssr.Count; i++)
            {
                string[] doub_ssr = (string[])dob.Doub_ssr[i];
                if (write(doub_ssr[0].ToString(), doub_ssr[1].ToString()) == 0)
                    break;
                else
                {
                    Console.WriteLine("doub.io:已添加" + (i + 1) + "条节点");//节点名称
                    System.Threading.Thread.Sleep(500);
                }
            }

            Console.WriteLine("节点添加结束，任意键关闭该窗口");//节点名称
            Console.Title = "SS免费节点更新完毕";//设置窗口标题
            Console.ReadLine(); //让控制台暂停,否则一闪而过了   
        }
        

        public static int write(string name,string ssr)//写入配置文件（服务器名称，SS连接）
        {
            name = name == null ? "未知" : name;
            if (ssr == null)
                return 0;
            try
            {
            var server = new Server(ssr);//解密SS连接
            var content = File.ReadAllText(CONFIG_FILE);//读取SS配置文件
            string start = "configs\" : [";//定位节点配置
            string notename = "remarks\": \"";//定位节点名称
           
                using (StreamWriter sw = new StreamWriter(File.Open(CONFIG_FILE, FileMode.Create)))
                {//读取并添加新的节点信息
                    string jsonString = JsonConvert.SerializeObject(server, Formatting.Indented);
                    jsonString = jsonString.Insert(jsonString.LastIndexOf("remarks") + notename.Length, name);
                    string result2 = content.Insert(content.LastIndexOf("configs") + start.Length, ",");
                    string result = result2.Insert(result2.LastIndexOf("configs") + start.Length, jsonString);
                    sw.Write(result);
                    sw.Flush();
                }
                return 1;
            }
            catch(IOException e)
            {
                Console.WriteLine("出现未知错误，请手动添加一个空白服务器后重试");
                return 0;
            }
        }

       


        public static string web(string web_src)
        {
            try
            {

                WebClient MyWebClient = new WebClient();


                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据

                Byte[] pageData = MyWebClient.DownloadData(web_src); //从指定网站下载数据


                //string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句            

                string pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句
                return pageHtml;
            }

            catch (WebException webEx)
            {

                Console.WriteLine("网址访问出现以下错误:"+webEx.Message.ToString());
                return null;
            }
        }


        public static void console(string str)
        {
            //自定义颜色
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine(str.PadRight(Console.BufferWidth - (str.Length % Console.BufferWidth))); //设置一整行的背景色
        }


        /// <summary>
        /// 二维码解码
        /// </summary>
        /// <param name="filePath">图片路径</param>
        /// <returns></returns>
        public static string read_img(string imageUri)
        {
            Bitmap myBitmap = getbitmap(imageUri);
            QRCodeDecoder decoder = new QRCodeDecoder();
            string decodedString = decoder.decode(new QRCodeBitmapImage(myBitmap));
            return decodedString;
        }




        /// <summary>
        /// 获取网络二维码对象 
        /// </summary>
        /// <param name="imageUri">网络二维码地址</param>
        /// <returns></returns>
        public static Bitmap getbitmap(string imageUri)
        {
            Bitmap img = null;
            HttpWebRequest req;
            HttpWebResponse res = null;
            try
            {
                System.Uri httpUrl = new System.Uri(imageUri);
                req = (HttpWebRequest)(WebRequest.Create(httpUrl));
                req.Method = "GET";
                res = (HttpWebResponse)(req.GetResponse());
                img = new Bitmap(res.GetResponseStream());//获取图片流        
               // img.Save("H:\\1.png");        
            }

            catch (Exception ex)
            {
                string aa = ex.Message;
            }
            finally
            {
                res.Close();
            }
            return img;
        }
    }
}
