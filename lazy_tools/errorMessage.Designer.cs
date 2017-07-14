namespace lazy_tools
{
    partial class errorMessage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_error = new System.Windows.Forms.Button();
            this.link_url = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_error
            // 
            this.btn_error.Location = new System.Drawing.Point(199, 55);
            this.btn_error.Name = "btn_error";
            this.btn_error.Size = new System.Drawing.Size(75, 23);
            this.btn_error.TabIndex = 0;
            this.btn_error.Text = "确定";
            this.btn_error.UseVisualStyleBackColor = true;
            this.btn_error.Click += new System.EventHandler(this.btn_error_Click);
            // 
            // link_url
            // 
            this.link_url.AutoSize = true;
            this.link_url.Location = new System.Drawing.Point(77, 34);
            this.link_url.Name = "link_url";
            this.link_url.Size = new System.Drawing.Size(29, 12);
            this.link_url.TabIndex = 1;
            this.link_url.TabStop = true;
            this.link_url.Text = "日志";
            this.link_url.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_url_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "操作失败，如果多次失败";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "请发送     到934405375@qq.com";
            // 
            // errorMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 90);
            this.ControlBox = false;
            this.Controls.Add(this.link_url);
            this.Controls.Add(this.btn_error);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "errorMessage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "失败";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_error;
        private System.Windows.Forms.LinkLabel link_url;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}