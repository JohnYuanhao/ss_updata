using ShadowsUpdateService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static shadowsocks8.Program;

namespace shadowsocks8.shadowsocks8_src
{
    class shadowsocks8_data
    {
        private string[][] shadowsocks8_ssr = new string[][]
            {
                new string[2],
                new string[2],
                new string[2]
              };

        public string[][] Shadowsocks8_ssr
        {
            get
            {
                return shadowsocks8_ssr;
            }
        }

        public string[] target
        {
            get
            {
                if(read_setting()!=null)
                return read_setting().Split();
                return null;
            }
        }

        public shadowsocks8_data(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            ServicePointManager.Expect100Continue = false;
            try
            {
                ((HttpWebResponse)request.GetResponse()).Close();
                url = request.Address.ToString();

            }
            catch (WebException exception)
            {
            }

            string pageHtml = web(url);
            //for (int i = 0; i < 3; i++)
            bool flag = true;
            int i = 0;
            while (flag)
            {
                int IndexofA = pageHtml.IndexOf("<h3>");
                int IndexofB = pageHtml.IndexOf("</h3>");
                if (IndexofA < 0)
                {
                    break;
                }
                string Ru = pageHtml.Substring(IndexofA + 4, IndexofB - IndexofA - 4);
                pageHtml = pageHtml.Substring(IndexofB + 4);
                string str = url + "images/server0";
                //string str = "http://www.shadowsocks8.com/images/server0";
                str = str + (i + 1) + ".png";
                if (target != null)
                {
                    foreach (var item in target)
                    {
                        if (item != "")
                            if (Ru.IndexOf(item) > -1)
                            {
                                shadowsocks8_ssr[i][0] = Ru;
                                shadowsocks8_ssr[i][1] = read_img(str);
                            }
                    }
                }
                else
                {
                    shadowsocks8_ssr[i][0] = Ru;
                    shadowsocks8_ssr[i][1] = read_img(str);
                }       
                i++;
            }
        }

        public string read_setting()
        {
            Ini ini = new Ini(@".\\setting.ini");
            Process processes = Process.GetCurrentProcess();
            string name = processes.ProcessName.Replace("shadowsocks8", "").Replace("-","").Trim();
            if(name!="")
            {
                if (ini.ReadValue("Optional", name) != "")
                    return ini.ReadValue("Optional", name);
            }

            if(ini.ReadValue("Optional", "target")!="")
            return ini.ReadValue("Optional", "target");
            return null;
        }

    }
}
