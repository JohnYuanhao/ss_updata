using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static doubi.Program;

namespace doubi.doubi_src
{
    class doub
    {
        private ArrayList doub_ssr = new ArrayList();
        public ArrayList Doub_ssr
        {
            get
            {
                return doub_ssr;
            }
        }

        public doub()
        {

            string pageHtml = web("https://doub.io/sszhfx/","");

            //FileStream file = new FileStream("", FileMode.Open);
            //file.Seek(0, SeekOrigin.Begin);
            //file.Read()
            //string pageHtml = File.ReadAllText(@"D:\1.txt",Encoding.Default);
            GetTitleContent(pageHtml);

        }


        public  void GetTitleContent(string str)//获取目标标签
        {
            // MatchCollection server = Regex.Matches(str, "<a[^>]+?href=[\"']?([^\"']+)[\"']?[^>]*>([^<]+)</a>", RegexOptions.IgnoreCase);//匹配所有A标签

            string tmpStr = string.Format("<{0} class[^>]*?{1}=(['\"\"]?)(?<url>[^'\"\"\\s>]+)\\1[^>]*>(?<Text>[^<]*)</{0}>", "a", "href"); //获取<a>之间内容   
            MatchCollection server = Regex.Matches(str, "<tr[\\s\\S]*?</tr>", RegexOptions.IgnoreCase);
            foreach (Match m in server)
            {
                string[] server_ssr = new string[2];
                Match ssr = Regex.Match(m.Value, tmpStr, RegexOptions.IgnoreCase);
                string name = Regex.Match(m.Value, "<td[\\s\\S]*?</td>", RegexOptions.IgnoreCase).Value;
                name = Regex.Replace(name, "[^\u4e00-\u9fa5]", "");
                server_ssr[0] = name;
                //if (ssr.Groups["Text"].Value == "二维码")
                //string i = ssr.Groups["Text"].Value.ToString();
                if (ssr.Groups["Text"].Value.IndexOf("二维码") > -1)
                {
                    string href = ssr.Groups["url"].Value;
                //href = href.Replace("http://pan.baidu.com/share/qrcode?w=300&amp;h=300&amp;url=", "");
               
                    href = href.Replace("http://doub.pw/qr/qr.php?text=", "");
                    server_ssr[1] = href;
                    doub_ssr.Add(server_ssr);
                }
            }



            //string tmpStr = string.Format("<{0} class[^>]*?{1}=(['\"\"]?)(?<url>[^'\"\"\\s>]+)\\1[^>]*>(?<Text>[^<]*)</{0}>", "a", "href"); //获取<a>之间内容   
            //MatchCollection server = Regex.Matches(str, "<tr class[\\s\\S]*?</tr>", RegexOptions.IgnoreCase);
            //foreach (Match m in server)
            //{
            //    string[] server_ssr = new string[2];
            //    Match ssr = Regex.Match(m.Value, tmpStr, RegexOptions.IgnoreCase);
            //    string name = Regex.Match(m.Value, "<td[\\s\\S]*?</td>", RegexOptions.IgnoreCase).Value;
            //    name=Regex.Replace(name, "[^\u4e00-\u9fa5]", "");
            //    server_ssr[0] = name;
            //    if (ssr.Groups["Text"].Value == "二维码")
            //    {
            //        string href = ssr.Groups["url"].Value;
            //        href = href.Replace("http://pan.baidu.com/share/qrcode?w=300&amp;h=300&amp;url=", "");
            //        server_ssr[1]= href;
            //        doub_ssr.Add(server_ssr);
            //    }
            //}



            //foreach (Match m in matchs)
            //{
            //    string name = m.Groups["Text"].Value;
            //    if (name == "二维码")
            //    {
            //        string href = m.Groups["url"].Value;
            //        href=href.Replace("http://pan.baidu.com/share/qrcode?w=300&amp;h=300&amp;url=", "");
            //        doub_ssr.Add(href);
            //    }

            //}

        }
    }
}
