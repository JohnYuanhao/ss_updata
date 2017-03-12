
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ishadowsocks.Program;

namespace ishadowsocks.ishadowsocks_src
{
    class ishadowsocks_data
    {
        private string[][] ishadowsocks_ssr = new string[][]
            {
                new string[2],
                new string[2],
                new string[2]
              };

        public string[][] iShadowsocks_ssr
        {
            get
            {
                return ishadowsocks_ssr;
            }
        }

        public ishadowsocks_data()
        {
            string pageHtml = web("http://www.shadowsocks8.net/");
            for (int i = 0; i < 3; i++)
            {
                int IndexofA = pageHtml.IndexOf("<h3>");
                int IndexofB = pageHtml.IndexOf("</h3>");
                string Ru = pageHtml.Substring(IndexofA + 4, IndexofB - IndexofA - 4);
                pageHtml = pageHtml.Substring(IndexofB + 4);
                this.ishadowsocks_ssr[i][0] = Ru;


                string str = "http://isx.yt/img/server0";
                //string str = "http://www.shadowsocks8.com/images/server0";
                str = str + (i + 1) + ".png";
                //shadowsocks8_ssr[i][1] = web(str);
                ishadowsocks_ssr[i][1] = read_img(str);
            }
        }



    }
}
