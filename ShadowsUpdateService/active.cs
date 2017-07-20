using Newtonsoft.Json;
using Shadowsocks.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace ShadowsUpdateService
{
    public class active
    {
        private static string CONFIG_FILE = "gui-config.json";//配置文件
        private static string LOG_FILE = "error.log";//配置文件

        /// <summary>
        /// 写入SS配置文件
        /// </summary> 
        /// <param name="name"></param>
        /// <param name="ssr"></param>
        /// <returns></returns>
        public static int write(string name, string ssr)
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
            catch (IOException e)
            {
                //Console.WriteLine("出现未知错误，请手动添加一个空白服务器后重试");
                return 0;
            }
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool write(string str)
        {
            string time = DateTime.Now.ToString("yyyy/M/d H:mm:ss");

            try
            {
                using (StreamWriter sw = new StreamWriter(File.Open(LOG_FILE, FileMode.Create)))
                {
                    sw.Write(time+"   "+str);
                    return true;
                }
            }
            catch {
                return false;
            }
            
        }

        /// <summary>
        /// 获取目标网页源代码
        /// </summary>
        /// <param name="web_src"></param>
        /// <returns></returns>
        public string Web(string web_src, string encod)
        {
            try
            {

                WebClient MyWebClient = new WebClient();


                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据

                Byte[] pageData = MyWebClient.DownloadData(web_src); //从指定网站下载数据
                string pageHtml = "";
                if (encod == "gbk")
                {
                    pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句            
                }
                else
                {
                    pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句
                }
                return pageHtml;
            }

            catch (WebException webEx)
            {

                Console.WriteLine("网址访问出现以下错误:" + webEx.Message.ToString());
                return null;
            }
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
        public  string read_img(string imageUri)
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
        public Bitmap getbitmap(string imageUri)
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

        /// <summary>
        /// 写入公告内容
        /// </summary>
        /// <param name="str"></param>
        public void Add_setting(string str)
        {
            Ini ini = new Ini(@".\\setting.ini");
            ini.Writue("Message", "Say", str);
        }

        /// <summary>
        /// 判断公告是否出现过
        /// </summary>
        /// <param name="str">公告内容</param>
        /// <returns></returns>
        //public bool Read_setting(string str)
        //{
        //    Ini ini = new Ini(@".\\setting.ini");
        //    try
        //    {
        //        if (ini.ReadValue("Message", "Say") == str)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch { }
        //    return false;
        //}
    }
}
