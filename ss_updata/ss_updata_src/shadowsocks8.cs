﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ss_updata.Program;

namespace ss_updata.ss_updata_src
{
    class shadowsocks8
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

        public shadowsocks8()
        {
            string pageHtml = web("http://www.shadowsocks8.net/");
            for (int i = 0; i < 3; i++)
            {
                int IndexofA = pageHtml.IndexOf("<h3>");
                int IndexofB = pageHtml.IndexOf("</h3>");
                string Ru = pageHtml.Substring(IndexofA + 4, IndexofB - IndexofA - 4);
                pageHtml = pageHtml.Substring(IndexofB + 4);
                this.shadowsocks8_ssr[i][0] = Ru;

                
                string str = "http://www.shadowsocks8.net/images/server0";
                str = str + (i+1) + ".png";
                //shadowsocks8_ssr[i][1] = web(str);
                shadowsocks8_ssr[i][1] = read_img(str);
            }
        }

      

    }
}