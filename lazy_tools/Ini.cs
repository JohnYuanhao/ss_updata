using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazy_tools
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
