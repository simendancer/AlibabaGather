namespace ImageDownload
{
    partial class ProductImage
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
            this.label3 = new System.Windows.Forms.Label();
            this.lbFail = new System.Windows.Forms.Label();
            this.lbSuccess = new System.Windows.Forms.Label();
            this.txtProductId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnStart = new System.Windows.Forms.Button();
            this.TxtSavePath = new System.Windows.Forms.TextBox();
            this.MsgList = new System.Windows.Forms.ListBox();
            this.txtThreadNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnOpenLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 337);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 36;
            this.label3.Text = "图片保存目录";
            // 
            // lbFail
            // 
            this.lbFail.AutoSize = true;
            this.lbFail.Location = new System.Drawing.Point(180, 23);
            this.lbFail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFail.Name = "lbFail";
            this.lbFail.Size = new System.Drawing.Size(37, 15);
            this.lbFail.TabIndex = 34;
            this.lbFail.Text = "失败";
            // 
            // lbSuccess
            // 
            this.lbSuccess.AutoSize = true;
            this.lbSuccess.Location = new System.Drawing.Point(22, 23);
            this.lbSuccess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSuccess.Name = "lbSuccess";
            this.lbSuccess.Size = new System.Drawing.Size(37, 15);
            this.lbSuccess.TabIndex = 33;
            this.lbSuccess.Text = "成功";
            // 
            // txtProductId
            // 
            this.txtProductId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtProductId.Location = new System.Drawing.Point(462, 20);
            this.txtProductId.Margin = new System.Windows.Forms.Padding(4);
            this.txtProductId.Name = "txtProductId";
            this.txtProductId.Size = new System.Drawing.Size(91, 25);
            this.txtProductId.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(371, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 30;
            this.label2.Text = "指定产品ID";
            // 
            // BtnStart
            // 
            this.BtnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnStart.Location = new System.Drawing.Point(711, 16);
            this.BtnStart.Margin = new System.Windows.Forms.Padding(4);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(70, 29);
            this.BtnStart.TabIndex = 29;
            this.BtnStart.Text = "开始";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // TxtSavePath
            // 
            this.TxtSavePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TxtSavePath.Location = new System.Drawing.Point(127, 334);
            this.TxtSavePath.Margin = new System.Windows.Forms.Padding(4);
            this.TxtSavePath.Name = "TxtSavePath";
            this.TxtSavePath.Size = new System.Drawing.Size(252, 25);
            this.TxtSavePath.TabIndex = 28;
            // 
            // MsgList
            // 
            this.MsgList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MsgList.FormattingEnabled = true;
            this.MsgList.ItemHeight = 15;
            this.MsgList.Location = new System.Drawing.Point(25, 60);
            this.MsgList.Margin = new System.Windows.Forms.Padding(4);
            this.MsgList.Name = "MsgList";
            this.MsgList.Size = new System.Drawing.Size(825, 259);
            this.MsgList.TabIndex = 27;
            // 
            // txtThreadNum
            // 
            this.txtThreadNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtThreadNum.Location = new System.Drawing.Point(621, 20);
            this.txtThreadNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtThreadNum.Name = "txtThreadNum";
            this.txtThreadNum.Size = new System.Drawing.Size(82, 25);
            this.txtThreadNum.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(561, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "线程数";
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(789, 16);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(61, 29);
            this.btnStop.TabIndex = 54;
            this.btnStop.Text = "终止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnOpenLog
            // 
            this.btnOpenLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenLog.Location = new System.Drawing.Point(710, 330);
            this.btnOpenLog.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenLog.Name = "btnOpenLog";
            this.btnOpenLog.Size = new System.Drawing.Size(140, 29);
            this.btnOpenLog.TabIndex = 55;
            this.btnOpenLog.Text = "打开日志文件夹";
            this.btnOpenLog.UseVisualStyleBackColor = true;
            this.btnOpenLog.Click += new System.EventHandler(this.btnOpenLog_Click);
            // 
            // ProductImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 380);
            this.Controls.Add(this.btnOpenLog);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtThreadNum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbFail);
            this.Controls.Add(this.lbSuccess);
            this.Controls.Add(this.txtProductId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.TxtSavePath);
            this.Controls.Add(this.MsgList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ProductImage";
            this.Text = "产品图片下载处理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProductImage_FormClosing);
            this.Load += new System.EventHandler(this.ProductImage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbFail;
        private System.Windows.Forms.Label lbSuccess;
        private System.Windows.Forms.TextBox txtProductId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.TextBox TxtSavePath;
        private System.Windows.Forms.ListBox MsgList;
        private System.Windows.Forms.TextBox txtThreadNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnOpenLog;
    }
}