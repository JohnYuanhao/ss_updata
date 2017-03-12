
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lazy_tools
{
    public partial class Form1 : Form
    {
        public static double updata_begin = 2;//文件版本号

        public Form1()
        {
            InitializeComponent();
            //C:\Windows\System32
            string i = "aaa/naaa\naaa/raaa\raaa";
            MessageBox.Show(i);
            setting();
        }

        private string path;
        private string Filepath = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\Supdata\\setting.ini";

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!IsAdministrator())
            {
                MessageBox.Show("请使用管理员权限运行此程序！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            else
            {
                if(string.IsNullOrEmpty(path))
                path = exePath();
            }
        }

        /// <summary>
        /// 设置客户端目录
        /// </summary>
        /// <returns></returns>
        private string exePath()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "ShadowsocksR客户端";
            ofd.Filter = "官方及修改版|*.exe";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return Path.GetDirectoryName(ofd.FileName);
            }
            else
            {
                MessageBox.Show("未找到ShadowsocksR！");
                return null;
            }
        }
        /// <summary>
        /// 设置配置文件
        /// </summary>
        private void setting()
        {

            if (!File.Exists(@Filepath))
            {
                //Directory.CreateDirectory(@Filepath); //新建文件夹   
                File.Create(@Filepath);
            }
            else
            {
                Read_setting();
            }



        }
        /// <summary>
        /// 读取配置文件
        /// </summary>
        private void Read_setting()
        {
            Ini ini = new Ini(@Filepath);
            try
            {
                if (ini.ReadValue("Name", "doubi") == "1")
                {
                    clb1.SetItemChecked(clb1.Items.IndexOf("doubi"), true);
                }
                if (ini.ReadValue("Name", "shadowsocks8")== "1")
                {
                    clb1.SetItemChecked(clb1.Items.IndexOf("shadowsocks8"), true);
                } 
            }
            catch { }

            path = ini.ReadValue("Setting", "Path");
        }
        /// <summary>
        /// 写入配置文件
        /// </summary>
        /// <param name="SSr"></param>
        private void Add_setting(string[] SSr)
        {
            Ini ini = new Ini(@Filepath);
            ini.Writue("Name", "doubi", "0");
            ini.Writue("Name", "shadowsocks8", "0");
            foreach (var item in SSr)
            {
                ini.Writue("Name", item, "1");
                // ini.Writue("Name", "key"+ Array.IndexOf(SSr,item) ,item);
            }
            ini.Writue("Setting", "Path", path);


        }

        private void Add_setting(string str)
        {
            Ini ini = new Ini(@Filepath);
            ini.Writue("Message", "note", path);


        }

        /// <summary>
        /// 管理员权限判定
        /// </summary>
        /// <returns></returns>
        public bool IsAdministrator()
        {
            WindowsIdentity current = WindowsIdentity.GetCurrent();
            WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
            return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < clb1.Items.Count; i++)
            {
                if (clb1.GetItemChecked(i))
                {
                    list.Add(clb1.GetItemText(clb1.Items[i]));
                }
            }
            Add_setting(list.ToArray());
            MessageBox.Show("成功");
        }

        private void clb1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (btn1.Text == "添加")
                btn1.Text = "修改";
            else
                btn1.Text = "添加";
        }

        private void Say()
        {
            active act = new active();
            //获取服务器软件版本
            double updata_end = double.Parse(act.Web("http://123.206.189.235/updata/lazy_tools/lazy_tools.txt", ""));
            //获取软件公告

            string updata_message = act.Web("http://123.206.189.235/updata/lazy_tools/lazy_tools_message.txt", "gbk");
            //判断公告是否已经出现过了
            if (!act.Read_setting(updata_message))
            {
                MessageBox.Show(updata_message, "公告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Add_setting(updata_message);
            }
            //判断软件版本是否更新
            if (updata_begin < updata_end)
            {
                if (MessageBox.Show("有新的版本可以更新，是否现在更新？\n\n网盘密码为1234", "更新检测", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("http://pan.baidu.com/s/1eSqTy18");
                    this.Close();
                }
            }
        }

    }
}
