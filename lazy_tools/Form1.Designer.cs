namespace lazy_tools
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn1 = new System.Windows.Forms.Button();
            this.clb1 = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(139, 10);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(55, 84);
            this.btn1.TabIndex = 0;
            this.btn1.Text = "添加";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // clb1
            // 
            this.clb1.CheckOnClick = true;
            this.clb1.FormattingEnabled = true;
            this.clb1.Items.AddRange(new object[] {
            "doubi",
            "shadowsocks8"});
            this.clb1.Location = new System.Drawing.Point(12, 10);
            this.clb1.Name = "clb1";
            this.clb1.Size = new System.Drawing.Size(120, 84);
            this.clb1.TabIndex = 2;
            this.clb1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clb1_ItemCheck);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(207, 101);
            this.Controls.Add(this.clb1);
            this.Controls.Add(this.btn1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "懒人工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.CheckedListBox clb1;
    }
}

