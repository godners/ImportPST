using Aspose.Email.Clients;
using Aspose.Email.Clients.Imap;
using Aspose.Email.Mapi;
using Aspose.Email.Storage.Pst;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ImportPST
{
    public partial class WinMain : Form
    {
        public WinMain() => InitializeComponent();
        private PersonalStorage PSTFile;
        private String IMAPServer;
        private ImapClient IMAPWorker;
        private Boolean PSTChecked;
        private Int32 TotalMails;
        private void WinMain_Load(object sender, EventArgs e)
        {
            IMAPServer = File.ReadAllText($"{Environment.CurrentDirectory}\\server.ini");
            PSTChecked = false;
        }

        private void ButtonFile_Click(object sender, EventArgs e)
        {
            if (OpenFileDialogPST.ShowDialog() == DialogResult.OK)
            {
                LabelFile.Text = OpenFileDialogPST.FileName;
                ToolTipMain.SetToolTip(LabelFile, OpenFileDialogPST.FileName);
            }
        }

        private void ButtonCheck_Click(object sender, EventArgs e)
        {
            if (!PSTChecked)
            {
                if (!File.Exists(LabelFile.Text))
                {
                    MessageBox.Show($"{LabelFile.Text}\r\n文件不存在。", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                }
                try { PSTFile = PersonalStorage.FromFile(LabelFile.Text); }
                catch
                {
                    MessageBox.Show($"{LabelFile.Text}\r\n无法读取。", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                }
                if (PSTFile.Format != FileFormat.Pst)
                {
                    MessageBox.Show($"{LabelFile.Text}\r\n格式错误。", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                }
            }
            PSTChecked = true;
            ButtonFile.Enabled = false;
            IMAPWorker = new ImapClient()
            {
                Host = IMAPServer,
                Username = TextBoxUsnm.Text,
                Password = TextBoxPswd.Text,
                SecurityOptions = SecurityOptions.Auto,
                AutoCommit = true
            };
            if (IMAPWorker.ConnectionState != ConnectionState.Open)
            {
                MessageBox.Show($"用户名或密码不正确。", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            TextBoxUsnm.Enabled = false;
            TextBoxPswd.Enabled = false;
            ButtonCheck.Enabled = false;
            TotalMails = 0;
            LoadTotalMessage(PSTFile.RootFolder);
            if (TotalMails == 0)
            {
                MessageBox.Show($"从{(new FileInfo(LabelFile.Text)).Name}中未找到邮件", 
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            else
            {
                MessageBox.Show($"从{(new FileInfo(LabelFile.Text)).Name}" +
                    $"中找到 {TotalMails} 封邮件，\r\n按“开始”进行导入",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
        }
        private void LoadTotalMessage(FolderInfo FolderCount)
        {           
            TotalMails += FolderCount.EnumerateMessagesEntryId().Count();
            foreach (FolderInfo FI in FolderCount.GetSubFolders(FolderKind.Normal)) LoadTotalMessage(FI);
        }


    }
}
