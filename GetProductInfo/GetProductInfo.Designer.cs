namespace GetProductInfo
{
    partial class GetProductInfo
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
            this.btnOpenLog = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtLastID = new System.Windows.Forms.TextBox();
            this.TxtSpeed = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.BtnReset = new System.Windows.Forms.Button();
            this.ListBox_msg = new System.Windows.Forms.ListBox();
            this.lbSuccess = new System.Windows.Forms.Label();
            this.BtnStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtCompanyId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOpenLog
            // 
            this.btnOpenLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenLog.Location = new System.Drawing.Point(19, 356);
            this.btnOpenLog.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.Size = new System.Drawing.Size(144, 29);
            this.btnOpenLog.TabIndex = 72;
            this.btnOpenLog.Text = "打开日志文件夹";
            this.btnOpenLog.UseVisualStyleBackColor = true;
            this.btnOpenLog.Click += new System.EventHandler(this.btnOpenLog_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(494, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 15);
            this.label3.TabIndex = 71;
            this.label3.Text = "间隔时间(毫秒)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(580, 363);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 70;
            this.label1.Text = "线程数";
            // 
            // TxtLastID
            // 
            this.TxtLastID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtLastID.Location = new System.Drawing.Point(640, 360);
            this.TxtLastID.Margin = new System.Windows.Forms.Padding(4);
            this.TxtLastID.Name = "TxtLastID";
            this.TxtLastID.Size = new System.Drawing.Size(114, 25);
            this.TxtLastID.TabIndex = 69;
            // 
            // TxtSpeed
            // 
            this.TxtSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSpeed.Location = new System.Drawing.Point(614, 23);
            this.TxtSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.TxtSpeed.Name = "TxtSpeed";
            this.TxtSpeed.Size = new System.Drawing.Size(71, 25);
            this.TxtSpeed.TabIndex = 68;
            this.TxtSpeed.Text = "200";
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(693, 22);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(61, 29);
            this.btnStop.TabIndex = 67;
            this.btnStop.Text = "终止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnReset.Location = new System.Drawing.Point(762, 22);
            this.BtnReset.Margin = new System.Windows.Forms.Padding(4);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(61, 29);
            this.BtnReset.TabIndex = 66;
            this.BtnReset.Text = "重置";
            this.BtnReset.UseVisualStyleBackColor = true;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // ListBox_msg
            // 
            this.ListBox_msg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListBox_msg.FormattingEnabled = true;
            this.ListBox_msg.ItemHeight = 15;
            this.ListBox_msg.Location = new System.Drawing.Point(19, 69);
            this.ListBox_msg.Margin = new System.Windows.Forms.Padding(4);
            this.ListBox_msg.Name = "ListBox_msg";
            this.ListBox_msg.Size = new System.Drawing.Size(804, 274);
            this.ListBox_msg.TabIndex = 65;
            // 
            // lbSuccess
            // 
            this.lbSuccess.AutoSize = true;
            this.lbSuccess.Location = new System.Drawing.Point(16, 26);
            this.lbSuccess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSuccess.Name = "lbSuccess";
            this.lbSuccess.Size = new System.Drawing.Size(37, 15);
            this.lbSuccess.TabIndex = 64;
            this.lbSuccess.Text = "成功";
            // 
            // BtnStart
            // 
            this.BtnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnStart.Location = new System.Drawing.Point(762, 356);
            this.BtnStart.Margin = new System.Windows.Forms.Padding(4);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(61, 29);
            this.BtnStart.TabIndex = 63;
            this.BtnStart.Text = "开始";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(369, 363);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 15);
            this.label2.TabIndex = 73;
            this.label2.Text = "指定供应商Id";
            // 
            // TxtCompanyId
            // 
            this.TxtCompanyId.Location = new System.Drawing.Point(473, 360);
            this.TxtCompanyId.Name = "TxtCompanyId";
            this.TxtCompanyId.Size = new System.Drawing.Size(100, 25);
            this.TxtCompanyId.TabIndex = 74;
            // 
            // GetProductInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 406);
            this.Controls.Add(this.TxtCompanyId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOpenLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtLastID);
            this.Controls.Add(this.TxtSpeed);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.BtnReset);
            this.Controls.Add(this.ListBox_msg);
            this.Controls.Add(this.lbSuccess);
            this.Controls.Add(this.BtnStart);
            this.Name = "GetProductInfo";
            this.Text = "获取产品信息";
            this.Load += new System.EventHandler(this.GetProductInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtLastID;
        private System.Windows.Forms.TextBox TxtSpeed;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.ListBox ListBox_msg;
        private System.Windows.Forms.Label lbSuccess;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtCompanyId;
    }
}