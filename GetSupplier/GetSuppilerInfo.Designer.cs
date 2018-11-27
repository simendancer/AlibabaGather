namespace GetSupplier
{
    partial class GetSuppilerInfo
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
            this.components = new System.ComponentModel.Container();
            this.MsgList = new System.Windows.Forms.ListBox();
            this.lbFail = new System.Windows.Forms.Label();
            this.lbSuccess = new System.Windows.Forms.Label();
            this.lbQueueCount = new System.Windows.Forms.Label();
            this.BtnStart = new System.Windows.Forms.Button();
            this.FrmTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.TxtSavePath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.checkCodeImg = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCheckCode = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.checkCodeImg)).BeginInit();
            this.SuspendLayout();
            // 
            // MsgList
            // 
            this.MsgList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MsgList.FormattingEnabled = true;
            this.MsgList.ItemHeight = 15;
            this.MsgList.Location = new System.Drawing.Point(26, 67);
            this.MsgList.Margin = new System.Windows.Forms.Padding(4);
            this.MsgList.Name = "MsgList";
            this.MsgList.Size = new System.Drawing.Size(804, 274);
            this.MsgList.TabIndex = 15;
            // 
            // lbFail
            // 
            this.lbFail.AutoSize = true;
            this.lbFail.Location = new System.Drawing.Point(237, 25);
            this.lbFail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFail.Name = "lbFail";
            this.lbFail.Size = new System.Drawing.Size(37, 15);
            this.lbFail.TabIndex = 12;
            this.lbFail.Text = "失败";
            // 
            // lbSuccess
            // 
            this.lbSuccess.AutoSize = true;
            this.lbSuccess.Location = new System.Drawing.Point(133, 25);
            this.lbSuccess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSuccess.Name = "lbSuccess";
            this.lbSuccess.Size = new System.Drawing.Size(37, 15);
            this.lbSuccess.TabIndex = 11;
            this.lbSuccess.Text = "成功";
            // 
            // lbQueueCount
            // 
            this.lbQueueCount.AutoSize = true;
            this.lbQueueCount.Location = new System.Drawing.Point(23, 25);
            this.lbQueueCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbQueueCount.Name = "lbQueueCount";
            this.lbQueueCount.Size = new System.Drawing.Size(67, 15);
            this.lbQueueCount.TabIndex = 10;
            this.lbQueueCount.Text = "队列数量";
            // 
            // BtnStart
            // 
            this.BtnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnStart.Location = new System.Drawing.Point(730, 18);
            this.BtnStart.Margin = new System.Windows.Forms.Padding(4);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(100, 29);
            this.BtnStart.TabIndex = 9;
            this.BtnStart.Text = "开始";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // FrmTimer
            // 
            this.FrmTimer.Interval = 950;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(353, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "图片保存目录";
            // 
            // TxtSavePath
            // 
            this.TxtSavePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TxtSavePath.Location = new System.Drawing.Point(458, 18);
            this.TxtSavePath.Margin = new System.Windows.Forms.Padding(4);
            this.TxtSavePath.Name = "TxtSavePath";
            this.TxtSavePath.Size = new System.Drawing.Size(252, 25);
            this.TxtSavePath.TabIndex = 13;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(98, 367);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(115, 25);
            this.txtUserName.TabIndex = 16;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(262, 367);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(120, 25);
            this.txtPassword.TabIndex = 17;
            // 
            // checkCodeImg
            // 
            this.checkCodeImg.Location = new System.Drawing.Point(461, 360);
            this.checkCodeImg.Name = "checkCodeImg";
            this.checkCodeImg.Size = new System.Drawing.Size(147, 42);
            this.checkCodeImg.TabIndex = 18;
            this.checkCodeImg.TabStop = false;
            this.checkCodeImg.Click += new System.EventHandler(this.checkCodeImg_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 370);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 19;
            this.label2.Text = "阿里帐号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(219, 370);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 20;
            this.label3.Text = "密码";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(730, 360);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(100, 42);
            this.btnLogin.TabIndex = 21;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(403, 372);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 22;
            this.label4.Text = "验证码";
            // 
            // txtCheckCode
            // 
            this.txtCheckCode.Location = new System.Drawing.Point(614, 367);
            this.txtCheckCode.Name = "txtCheckCode";
            this.txtCheckCode.Size = new System.Drawing.Size(96, 25);
            this.txtCheckCode.TabIndex = 23;
            // 
            // GetSuppilerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 420);
            this.Controls.Add(this.txtCheckCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkCodeImg);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.MsgList);
            this.Controls.Add(this.lbFail);
            this.Controls.Add(this.lbSuccess);
            this.Controls.Add(this.lbQueueCount);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtSavePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GetSuppilerInfo";
            this.Text = "GetSuppilerInfo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GetSuppilerInfo_FormClosing);
            this.Load += new System.EventHandler(this.GetSuppilerInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.checkCodeImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox MsgList;
        private System.Windows.Forms.Label lbFail;
        private System.Windows.Forms.Label lbSuccess;
        private System.Windows.Forms.Label lbQueueCount;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Timer FrmTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtSavePath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.PictureBox checkCodeImg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCheckCode;
    }
}