using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace Shadowsocks
{
    class Data_dispose
    {
        private string CONFIG_FILE = "gui-config.json";//配置文件

        /// <summary>
        /// 写入配置文件
        /// </summary>
        /// <param name="name">服务器名称</param>
        /// <param name="ssr">SS连接</param>
        /// <returns></returns>
        public int write(string name, string ssr)
        {
            name = name ?? "未知";
            string time = DateTime.Now.ToString("H:mm MM-dd");
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
                    jsonString = jsonString.Insert(jsonString.LastIndexOf("remarks") + notename.Length, name + " " + time);
                    string result2 = content.Insert(content.LastIndexOf("configs") + start.Length, ",");
                    string result = result2.Insert(result2.LastIndexOf("configs") + start.Length, jsonString);
                    sw.Write(result);
                    sw.Flush();
                }
                return 1;
            }
            catch (IOException e)
            {
                Console.WriteLine("出现未知错误，请手动添加一个空白服务器后重试");
                return 0;
            }
        }

        /// <summary>
        /// 获取目标网页源代码
        /// </summary>
        /// <param name="web_src"></param>
        /// <returns></returns>
        public static string web(string web_src, string encod= "UTF8")
        {
            try
            {

                WebClient MyWebClient = new WebClient()
                {
                    Credentials = CredentialCache.DefaultCredentials//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                };
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
        /// 获取网络图片流
        /// </summary>
        /// <param name="imageUri">网络二维码地址</param>
        /// <returns></returns>
        public  Bitmap getbitmap(string imageUri)
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
                string message = ex.Message;
            }
            finally
            {
                res.Close();
            }
            return img;
        }


        


        /// <summary>
        /// 更新检查
        /// </summary>
        /// <param name="name">更新源网站名称(doubi|shadowsocks8|ishadowsocks)</param>
        /// <param name="versions">版本号</param>
        public void updata_check(string name,double versions)
        {
            //获取软件版本号
            string web_location = "http://123.206.189.235/updata/" + name +"/" + name + ".txt";
            //获取软件公告
            string updata_message = web("http://123.206.189.235/updata/" + name + "/" + name + "_message.txt", "gbk");
            double mark = double.Parse(web(web_location,""));
            if (versions < mark)
            {
                if (MessageBox.Show("有新的版本可以更新，是否现在更新？\n\n网盘密码为1234", "更新检测", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("http://pan.baidu.com/s/1eSqTy18");
                    return;
                }
            }
        }

        /// <summary>
        /// 写入公告内容
        /// </summary>
        /// <param name="str"></param>
        private static void Add_setting(string str)
        {
            Ini ini = new Ini(@".\\setting.ini");
            ini.Writue("Message", "Say", str);
        }

        /// <summary>
        /// 判断公告是否出现过
        /// </summary>
        /// <param name="str">公告内容</param>
        /// <returns></returns>
        private static bool Read_setting(string str)
        {
            Ini ini = new Ini(@".\\setting.ini");
            try
            {
                if (ini.ReadValue("Message", "Say") == str)
                {
                    return true;
                }
                return false;
            }
            catch { }
            return false;
        }

    }
}
