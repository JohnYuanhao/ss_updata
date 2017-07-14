using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shadowsocks.Data_dispose;

namespace Shadowsocks
{
    class Shadowsocks_data
    {
        private string[][] shadowsocks_ssr = new string[][]
            {
                new string[2],
                new string[2],
                new string[2]
              };
            
        public string[][] Shadowsocks_ssr
        {
            get
            {
                return shadowsocks_ssr;
            }
        }

        public Shadowsocks_data()
        {
            string pageHtml = web("http://get.shadowsocks8.cc/");
            for (int i = 0; i < 3; i++)
            {
                int IndexofA = pageHtml.IndexOf("<h3>");
                int IndexofB = pageHtml.IndexOf("</h3>");
                string Ru = pageHtml.Substring(IndexofA + 4, IndexofB - IndexofA - 4);
                pageHtml = pageHtml.Substring(IndexofB + 4);
                this.shadowsocks_ssr[i][0] = Ru;

                
                string str = "http://get.shadowsocks8.cc/images/server0";
                //string str = "http://www.shadowsocks8.com/images/server0";
                str = str + (i+1) + ".png";
                //shadowsocks8_ssr[i][1] = web(str);
                shadowsocks_ssr[i][1] = read_img(str);
            }
        }

      

    }
}
