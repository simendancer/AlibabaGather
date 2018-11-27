namespace GetProductList
{
    partial class GetProductList
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
            this.SuspendLayout();
            // 
            // btnOpenLog
            // 
            this.btnOpenLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenLog.Location = new System.Drawing.Point(22, 358);
            this.btnOpenLog.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.Size = new System.Drawing.Size(144, 29);
            this.btnOpenLog.TabIndex = 82;
            this.btnOpenLog.Text = "打开日志文件夹";
            this.btnOpenLog.UseVisualStyleBackColor = true;
            this.btnOpenLog.Click += new System.EventHandler(this.btnOpenLog_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(497, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 15);
            this.label3.TabIndex = 81;
            this.label3.Text = "间隔时间(毫秒)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(583, 365);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 80;
            this.label1.Text = "线程数";
            // 
            // TxtLastID
            // 
            this.TxtLastID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtLastID.Location = new System.Drawing.Point(643, 362);
            this.TxtLastID.Margin = new System.Windows.Forms.Padding(4);
            this.TxtLastID.Name = "TxtLastID";
            this.TxtLastID.Size = new System.Drawing.Size(114, 25);
            this.TxtLastID.TabIndex = 79;
            // 
            // TxtSpeed
            // 
            this.TxtSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSpeed.Location = new System.Drawing.Point(617, 25);
            this.TxtSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.TxtSpeed.Name = "TxtSpeed";
            this.TxtSpeed.Size = new System.Drawing.Size(71, 25);
            this.TxtSpeed.TabIndex = 78;
            this.TxtSpeed.Text = "200";
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(696, 24);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(61, 29);
            this.btnStop.TabIndex = 77;
            this.btnStop.Text = "终止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnReset.Location = new System.Drawing.Point(765, 24);
            this.BtnReset.Margin = new System.Windows.Forms.Padding(4);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(61, 29);
            this.BtnReset.TabIndex = 76;
            this.BtnReset.Text = "重置";
            this.BtnReset.UseVisualStyleBackColor = true;
            // 
            // ListBox_msg
            // 
            this.ListBox_msg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListBox_msg.FormattingEnabled = true;
            this.ListBox_msg.ItemHeight = 15;
            this.ListBox_msg.Location = new System.Drawing.Point(22, 71);
            this.ListBox_msg.Margin = new System.Windows.Forms.Padding(4);
            this.ListBox_msg.Name = "ListBox_msg";
            this.ListBox_msg.Size = new System.Drawing.Size(804, 274);
            this.ListBox_msg.TabIndex = 75;
            // 
            // lbSuccess
            // 
            this.lbSuccess.AutoSize = true;
            this.lbSuccess.Location = new System.Drawing.Point(19, 28);
            this.lbSuccess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSuccess.Name = "lbSuccess";
            this.lbSuccess.Size = new System.Drawing.Size(37, 15);
            this.lbSuccess.TabIndex = 74;
            this.lbSuccess.Text = "成功";
            // 
            // BtnStart
            // 
            this.BtnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnStart.Location = new System.Drawing.Point(765, 358);
            this.BtnStart.Margin = new System.Windows.Forms.Padding(4);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(61, 29);
            this.BtnStart.TabIndex = 73;
            this.BtnStart.Text = "开始";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // GetProductList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 411);
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
            this.Name = "GetProductList";
            this.Text = "获取产品列表";
            this.Load += new System.EventHandler(this.GetProductList_Load);
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
    }
}