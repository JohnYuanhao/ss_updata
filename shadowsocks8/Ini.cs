using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowsUpdateService
{
    class Ini
    {
// 声明INI文件写操作函数 WritePrivateProfileString()
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        // 声明INI文件读操作函数 GetPrivateProfileString()
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);
        private string sPath = null;
        public Ini(string path)
        {
            this.sPath = path;
            try
            {
                if (!File.Exists(@".\\setting.ini"))
                {
                    Writue("Necessary", "url", "http://www.shadowsocks8.net/");
                    Writue("Optional", "target", "");
                    Writue("Optional", "close", "off");
                    Writue("说明", "注意1", "如果脚本出现错误，请复制url到浏览器打开，然后复制跳转之后的网址覆盖到url；");
                    Writue("说明", "注意1.1", "如果如果仍然错误，请删除配置文件,重新生成；");
                    Writue("说明", "注意2", "支持多文件读取配置，需要你自行修改文件名称如shadowsocks8-others.exe，并在Optional下添加“others=香港 日本”，即可过滤除香港和日本之外的节点；");
                    Writue("说明", "注意3", "Optional下的每一项，都支持多节点过滤如“target=香港 日本”，使用空格分隔。“=”号右边为空，则为不过滤；");
                    Writue("说明", "注意4", "close值为on和off，分别为自动退出脚本和保持窗口，默认保持窗口");
                    Writue("说明", "注意5", "说明项下的注意内容可以全部删除，无关软件运行；");
                    Writue("说明", "注意6", "所有数据均来自shadowsocks8官网免费三节点，有任何疑问请联系@Shadow-影；");
                }
            }
            catch (Exception) { }
        }
        public void Writue(string section, string key, string value)
        {
            // section=配置节key=键名value=键值path=路径
            WritePrivateProfileString(section, key, value, sPath);
        }
        public string ReadValue(string section, string key)
        {
            // 每ini读取少字节
            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);
            // section=配置节key=键名temp=面path=路径
            GetPrivateProfileString(section, key, "", temp, 255, sPath);
            return temp.ToString();
        }
    }
}
