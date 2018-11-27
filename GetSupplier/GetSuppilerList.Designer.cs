namespace GetSupplier
{
    partial class GetSuppilerList
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
            this.label1 = new System.Windows.Forms.Label();
            this.TxtLastID = new System.Windows.Forms.TextBox();
            this.TxtSpeed = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.BtnReset = new System.Windows.Forms.Button();
            this.ListBox_msg = new System.Windows.Forms.ListBox();
            this.lbSuccess = new System.Windows.Forms.Label();
            this.BtnStart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.chkIsExist = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(697, 303);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 36;
            this.label1.Text = "分类编号";
            // 
            // TxtLastID
            // 
            this.TxtLastID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtLastID.Location = new System.Drawing.Point(772, 297);
            this.TxtLastID.Margin = new System.Windows.Forms.Padding(4);
            this.TxtLastID.Name = "TxtLastID";
            this.TxtLastID.Size = new System.Drawing.Size(114, 25);
            this.TxtLastID.TabIndex = 35;
            // 
            // TxtSpeed
            // 
            this.TxtSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSpeed.Location = new System.Drawing.Point(746, 27);
            this.TxtSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.TxtSpeed.Name = "TxtSpeed";
            this.TxtSpeed.Size = new System.Drawing.Size(71, 25);
            this.TxtSpeed.TabIndex = 34;
            this.TxtSpeed.Text = "200";
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(825, 24);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(61, 29);
            this.btnStop.TabIndex = 33;
            this.btnStop.Text = "终止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // BtnReset
            // 
            this.BtnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnReset.Location = new System.Drawing.Point(894, 24);
            this.BtnReset.Margin = new System.Windows.Forms.Padding(4);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(61, 29);
            this.BtnReset.TabIndex = 32;
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
            this.ListBox_msg.Location = new System.Drawing.Point(30, 65);
            this.ListBox_msg.Margin = new System.Windows.Forms.Padding(4);
            this.ListBox_msg.Name = "ListBox_msg";
            this.ListBox_msg.Size = new System.Drawing.Size(925, 214);
            this.ListBox_msg.TabIndex = 31;
            // 
            // lbSuccess
            // 
            this.lbSuccess.AutoSize = true;
            this.lbSuccess.Location = new System.Drawing.Point(27, 27);
            this.lbSuccess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSuccess.Name = "lbSuccess";
            this.lbSuccess.Size = new System.Drawing.Size(37, 15);
            this.lbSuccess.TabIndex = 30;
            this.lbSuccess.Text = "成功";
            // 
            // BtnStart
            // 
            this.BtnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnStart.Location = new System.Drawing.Point(894, 296);
            this.BtnStart.Margin = new System.Windows.Forms.Padding(4);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(61, 29);
            this.BtnStart.TabIndex = 29;
            this.BtnStart.Text = "开始";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(626, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 15);
            this.label3.TabIndex = 37;
            this.label3.Text = "间隔时间(毫秒)";
            // 
            // chkIsExist
            // 
            this.chkIsExist.AutoSize = true;
            this.chkIsExist.Checked = true;
            this.chkIsExist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsExist.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkIsExist.Location = new System.Drawing.Point(30, 296);
            this.chkIsExist.Name = "chkIsExist";
            this.chkIsExist.Size = new System.Drawing.Size(59, 19);
            this.chkIsExist.TabIndex = 38;
            this.chkIsExist.Text = "去重";
            this.chkIsExist.UseVisualStyleBackColor = true;
            // 
            // GetSuppilerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 342);
            this.Controls.Add(this.chkIsExist);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtLastID);
            this.Controls.Add(this.TxtSpeed);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.BtnReset);
            this.Controls.Add(this.ListBox_msg);
            this.Controls.Add(this.lbSuccess);
            this.Controls.Add(this.BtnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GetSuppilerList";
            this.Text = "供应商列表采集";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GetSuppiler_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtLastID;
        private System.Windows.Forms.TextBox TxtSpeed;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.ListBox ListBox_msg;
        private System.Windows.Forms.Label lbSuccess;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkIsExist;
    }
}

