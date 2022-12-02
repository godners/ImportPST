namespace ImportPST
{
    partial class WinMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinMain));
            this.TagFile = new System.Windows.Forms.Label();
            this.TagUsnm = new System.Windows.Forms.Label();
            this.TagPswd = new System.Windows.Forms.Label();
            this.LabelFile = new System.Windows.Forms.Label();
            this.TextBoxUsnm = new System.Windows.Forms.TextBox();
            this.TextBoxPswd = new System.Windows.Forms.TextBox();
            this.ButtonCheck = new System.Windows.Forms.Button();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.ButtonFile = new System.Windows.Forms.Button();
            this.ProgressBarSchedule = new System.Windows.Forms.ProgressBar();
            this.LabelSchedule = new System.Windows.Forms.Label();
            this.OpenFileDialogPST = new System.Windows.Forms.OpenFileDialog();
            this.ToolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // TagFile
            // 
            this.TagFile.Location = new System.Drawing.Point(8, 8);
            this.TagFile.Margin = new System.Windows.Forms.Padding(4);
            this.TagFile.Name = "TagFile";
            this.TagFile.Padding = new System.Windows.Forms.Padding(4);
            this.TagFile.Size = new System.Drawing.Size(90, 26);
            this.TagFile.TabIndex = 0;
            this.TagFile.Text = "PST文件：";
            this.TagFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TagUsnm
            // 
            this.TagUsnm.Location = new System.Drawing.Point(8, 42);
            this.TagUsnm.Margin = new System.Windows.Forms.Padding(4);
            this.TagUsnm.Name = "TagUsnm";
            this.TagUsnm.Padding = new System.Windows.Forms.Padding(4);
            this.TagUsnm.Size = new System.Drawing.Size(87, 26);
            this.TagUsnm.TabIndex = 1;
            this.TagUsnm.Text = "用户名：";
            this.TagUsnm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TagPswd
            // 
            this.TagPswd.Location = new System.Drawing.Point(8, 76);
            this.TagPswd.Margin = new System.Windows.Forms.Padding(4);
            this.TagPswd.Name = "TagPswd";
            this.TagPswd.Padding = new System.Windows.Forms.Padding(4);
            this.TagPswd.Size = new System.Drawing.Size(87, 26);
            this.TagPswd.TabIndex = 2;
            this.TagPswd.Text = "密码：";
            this.TagPswd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelFile
            // 
            this.LabelFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelFile.Location = new System.Drawing.Point(103, 8);
            this.LabelFile.Margin = new System.Windows.Forms.Padding(4);
            this.LabelFile.Name = "LabelFile";
            this.LabelFile.Padding = new System.Windows.Forms.Padding(4);
            this.LabelFile.Size = new System.Drawing.Size(187, 26);
            this.LabelFile.TabIndex = 3;
            this.LabelFile.Text = "D:\\Projects\\ImportPST\\ImportPST.pst";
            this.LabelFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBoxUsnm
            // 
            this.TextBoxUsnm.Location = new System.Drawing.Point(103, 42);
            this.TextBoxUsnm.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxUsnm.Name = "TextBoxUsnm";
            this.TextBoxUsnm.Size = new System.Drawing.Size(187, 26);
            this.TextBoxUsnm.TabIndex = 4;
            this.TextBoxUsnm.Text = "pst01@tianyue.ren";
            // 
            // TextBoxPswd
            // 
            this.TextBoxPswd.Location = new System.Drawing.Point(103, 76);
            this.TextBoxPswd.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxPswd.Name = "TextBoxPswd";
            this.TextBoxPswd.PasswordChar = '*';
            this.TextBoxPswd.Size = new System.Drawing.Size(187, 26);
            this.TextBoxPswd.TabIndex = 5;
            this.TextBoxPswd.Text = "Godners8";
            // 
            // ButtonCheck
            // 
            this.ButtonCheck.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonCheck.Location = new System.Drawing.Point(298, 42);
            this.ButtonCheck.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonCheck.Name = "ButtonCheck";
            this.ButtonCheck.Size = new System.Drawing.Size(49, 26);
            this.ButtonCheck.TabIndex = 6;
            this.ButtonCheck.Text = "检查";
            this.ButtonCheck.UseVisualStyleBackColor = true;
            this.ButtonCheck.Click += new System.EventHandler(this.ButtonCheck_Click);
            // 
            // ButtonStart
            // 
            this.ButtonStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonStart.Enabled = false;
            this.ButtonStart.Location = new System.Drawing.Point(298, 76);
            this.ButtonStart.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(49, 26);
            this.ButtonStart.TabIndex = 7;
            this.ButtonStart.Text = "开始";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // ButtonFile
            // 
            this.ButtonFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonFile.Location = new System.Drawing.Point(298, 8);
            this.ButtonFile.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonFile.Name = "ButtonFile";
            this.ButtonFile.Size = new System.Drawing.Size(49, 26);
            this.ButtonFile.TabIndex = 8;
            this.ButtonFile.Text = "选择";
            this.ButtonFile.UseVisualStyleBackColor = true;
            this.ButtonFile.Click += new System.EventHandler(this.ButtonFile_Click);
            // 
            // ProgressBarSchedule
            // 
            this.ProgressBarSchedule.Location = new System.Drawing.Point(11, 110);
            this.ProgressBarSchedule.Margin = new System.Windows.Forms.Padding(4);
            this.ProgressBarSchedule.Name = "ProgressBarSchedule";
            this.ProgressBarSchedule.Size = new System.Drawing.Size(279, 26);
            this.ProgressBarSchedule.TabIndex = 9;
            // 
            // LabelSchedule
            // 
            this.LabelSchedule.BackColor = System.Drawing.SystemColors.Control;
            this.LabelSchedule.Location = new System.Drawing.Point(291, 112);
            this.LabelSchedule.Margin = new System.Windows.Forms.Padding(4);
            this.LabelSchedule.Name = "LabelSchedule";
            this.LabelSchedule.Padding = new System.Windows.Forms.Padding(4);
            this.LabelSchedule.Size = new System.Drawing.Size(56, 24);
            this.LabelSchedule.TabIndex = 10;
            this.LabelSchedule.Text = "22.0%";
            this.LabelSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OpenFileDialogPST
            // 
            this.OpenFileDialogPST.DefaultExt = "pst";
            this.OpenFileDialogPST.Filter = "PST File (*.pst)|*.pst";
            this.OpenFileDialogPST.Title = "选择PST文件";
            // 
            // ToolTipMain
            // 
            this.ToolTipMain.AutomaticDelay = 200;
            // 
            // WinMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(355, 144);
            this.Controls.Add(this.LabelSchedule);
            this.Controls.Add(this.ProgressBarSchedule);
            this.Controls.Add(this.ButtonFile);
            this.Controls.Add(this.ButtonStart);
            this.Controls.Add(this.ButtonCheck);
            this.Controls.Add(this.TextBoxPswd);
            this.Controls.Add(this.TextBoxUsnm);
            this.Controls.Add(this.LabelFile);
            this.Controls.Add(this.TagPswd);
            this.Controls.Add(this.TagUsnm);
            this.Controls.Add(this.TagFile);
            this.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.MaximizeBox = false;
            this.Name = "WinMain";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PST文件导入工具";
            this.Load += new System.EventHandler(this.WinMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TagFile;
        private System.Windows.Forms.Label TagUsnm;
        private System.Windows.Forms.Label TagPswd;
        private System.Windows.Forms.Label LabelFile;
        private System.Windows.Forms.TextBox TextBoxUsnm;
        private System.Windows.Forms.TextBox TextBoxPswd;
        private System.Windows.Forms.Button ButtonCheck;
        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.Button ButtonFile;
        private System.Windows.Forms.ProgressBar ProgressBarSchedule;
        private System.Windows.Forms.Label LabelSchedule;
        private System.Windows.Forms.OpenFileDialog OpenFileDialogPST;
        private System.Windows.Forms.ToolTip ToolTipMain;
    }
}

