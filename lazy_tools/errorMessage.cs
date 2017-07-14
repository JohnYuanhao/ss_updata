using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lazy_tools
{
    public partial class errorMessage : Form
    {
        public errorMessage()
        {
            InitializeComponent();
        }

        private void link_url_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void btn_error_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
