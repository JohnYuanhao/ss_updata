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
using System.Windows.Forms;
using System.Web;
using shadowsocks8.shadowsocks8_src;
using System.Reflection;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace shadowsocks8
{
    class Program
    {


        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");
            dllName = dllName.Replace(".", "_");
            if (dllName.EndsWith("_resources")) return null;
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("shadowsocks8.Resources", System.Reflection.Assembly.GetExecutingAssembly());
            byte[] bytes = (byte[])rm.GetObject(dllName);
            return System.Reflection.Assembly.Load(bytes);
        }

        
        private static string CONFIG_FILE = "gui-config.json";//配置文件

        static void Main(string[] args)
        {
            //double updata_begin = 4;//文件版本号
            //Console.Title = "SS免费节点 shadowsocks8版 v" + updata_begin.ToString("0.0") + "                       by.Shadow-隐";//设置窗口标题

            //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            //double updata_end = double.Parse(web("http://123.206.189.235/updata/shadowsocks8.txt"));
            //if (updata_begin < updata_end)
            //{
            //    if (MessageBox.Show("有新的版本可以更新，是否现在更新？\n\n网盘密码为1234", "更新检测", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        System.Diagnostics.Process.Start("http://pan.baidu.com/s/1eSqTy18");
            //        return;
            //    }
            //}

            //Console.WriteLine("========================================================");
            //Console.WriteLine("===                                                  ===");
            //Console.WriteLine("===        特别鸣谢http://www.shadowsocks8.com/      ===");
            //Console.WriteLine("===      请将脚本跟shadowsocks放在同级目录下使用     ===");
            //Console.WriteLine("===  任何错误都可以用管理员权限或添加空白服务器解决  ===");
            //Console.WriteLine("===                                                  ===");
            //Console.WriteLine("========================================================");
            //Console.WriteLine("正在获取shadowsocks8节点");
            //shadowsocks8_data sh8 = new shadowsocks8_data();
            //for (int i = 0; i < sh8.Shadowsocks8_ssr.Length; i++)
            //{
            //    if (write(sh8.Shadowsocks8_ssr[i][0], sh8.Shadowsocks8_ssr[i][1]) == 0)
            //        break;
            //    else
            //    {
            //        Console.WriteLine("Shadowsocks8:已添加" + (i + 1) + "条节点");//节点名称
            //        System.Threading.Thread.Sleep(500);
            //    }
            //}
            SaveRecord("aa");

               // Console.WriteLine("节点添加结束，任意键关闭该窗口");//节点名称
               //Console.ReadLine(); //让控制台暂停,否则一闪而过了   
        }

        public static void SaveRecord(string content)
        {
            if (string.IsNullOrEmpty(content)) { return; }
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            { //ApplicationBace在bin文件夹下Debug文件夹，或者Realease文件夹下 
                string path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, string.Format("0:yyyyMMdd", DateTime.Now));
                using (fileStream = new FileStream(@"log.log", FileMode.Append, FileAccess.Write))
                {
                    using (streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.WriteLine(content);
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
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool write(string str)
        {
            string time = DateTime.Now.ToString("yyyy/M/d H:mm:ss");

            try
            {
                using (StreamWriter sw = new StreamWriter(File.Open(@"log.log", FileMode.Create)))
                {
                    string error = time + "   " + str;
                    sw.Write(error);
                    return true;
                }
            }
            catch
            {
                return false;
            }

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


        ///// <summary>
        ///// 读取二维码
        ///// 读取失败，返回空字符串
        ///// </summary>
        ///// <param name="filename">指定二维码图片位置</param>
        //public static string read_img(string imageUri)
        //{
        //    BarcodeReader reader = new BarcodeReader();
        //    Bitmap map = getbitmap(imageUri);
        //    Result result = reader.Decode(map);
        //    return result == null ? "" : result.Text;
        //}

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
