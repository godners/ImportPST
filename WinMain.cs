using Aspose.Email;
using Aspose.Email.Clients;
using Aspose.Email.Clients.Imap;
using Aspose.Email.Mapi;
using Aspose.Email.Storage.Pst;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static ImportPST.Const;

namespace ImportPST
{
    public partial class WinMain : Form
    {
        public WinMain() => InitializeComponent();

        private PersonalStorage PSTFile;
        private String IMAPServer;
        private ImapClient IMAPWorker;
        private Boolean PSTChecked;
        private Int32 TotalMails, FinishMails;


        private void WinMain_Load(object sender, EventArgs e)
        {
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
                // ↓↓↓PST文件不存在
                if (!File.Exists(LabelFile.Text))
                {
                    MessageBox.Show($"{LabelFile.Text}\r\n文件不存在。", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                }
                // ↓↓↓PST文件无法读取
                try { PSTFile = PersonalStorage.FromFile(LabelFile.Text); }
                catch
                {
                    MessageBox.Show($"{LabelFile.Text}\r\n无法读取。", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                }
                // ↓↓↓PST文件格式错误
                if (PSTFile.Format != FileFormat.Pst)
                {
                    MessageBox.Show($"{LabelFile.Text}\r\n格式错误。", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                }
            }
            // ↓↓↓提取总邮件数
            TotalMails = 0;
            LoadTotalMessage(PSTFile.RootFolder);
            // ↓↓↓PST中无邮件
            if (TotalMails == 0)
            {
                MessageBox.Show($"从{(new FileInfo(LabelFile.Text)).Name}中未找到邮件",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            // ↓↓↓PST完成验证
            LabelFile.Enabled = false;
            PSTChecked = true;
            ButtonFile.Enabled = false;
            TextBoxUsnm.Text = TextBoxUsnm.Text.Split('@')[0];
            // ↓↓↓无法获取IMAP服务器
            try { /*GetIMAPServer();*/ IMAPServer = "imap.qiye.aliyun.com"; }
            catch
            {
                MessageBox.Show($"用户名或密码不正确。", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            // ↓↓↓连接IMAP服务器
            IMAPWorker = new ImapClient()
            {
                Host = IMAPServer,
                Username = /*TextBoxUsnm.Text + "@cicc.com.cn"*/ "pst01@tianyue.ren",
                Password = TextBoxPswd.Text,
                SecurityOptions = SecurityOptions.Auto,
                AutoCommit = true
            };
            // ↓↓↓无法连接IMAP服务器
            if (IMAPWorker.ConnectionState != ConnectionState.Open)
            {
                MessageBox.Show($"{IMAPServer}\r\n连接失败。", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            // ↓↓↓服务器完成验证
            TextBoxUsnm.Enabled = false;
            TextBoxPswd.Enabled = false;
            ButtonCheck.Enabled = false;
            ButtonStart.Enabled = true;
            MessageBox.Show($"从{(new FileInfo(LabelFile.Text)).Name}" +
                $"中找到 {TotalMails} 封邮件，\r\n按“开始”进行导入",
                "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void GetIMAPServer()
        {
            DirectoryEntry ADInstance = new DirectoryEntry($"LDAP://{DomainName}", TextBoxUsnm.Text, TextBoxPswd.Text);
            DirectorySearcher ADSearcher = new DirectorySearcher()
            {
                SearchRoot = ADInstance,
                Filter = ($"(&(objectClass=user)(sAMAccountName={TextBoxUsnm.Text}))"),
                ClientTimeout = new TimeSpan(0, 0, 3),
            };
            SearchResult ADResult = ADSearcher.FindOne();
            DirectoryEntry ADUser = ADResult.GetDirectoryEntry();
            IMAPServerDomain(ADUser.Properties["extensionAttribute5"][0].ToString());
            ADUser.Close(); ADUser.Dispose(); ADSearcher.Dispose(); ADInstance.Close(); ADInstance.Dispose();
        }
        private void IMAPServerDomain(String IMAPServerID)
        {
            switch (IMAPServerID)
            {
                case "1": IMAPServer = "mailbj.cicc.group"; break;
                case "2": IMAPServer = "mailrc.cicc.group"; break;
                case "3": IMAPServer = "mailhk.cicc.group"; break;
                default: IMAPServer = "mailzb.cicc.group"; break;
            }
        }
        private void LoadTotalMessage(FolderInfo FolderCount)
        {
            TotalMails += FolderCount.EnumerateMessagesEntryId().Count();
            foreach (FolderInfo FI in FolderCount.GetSubFolders(FolderKind.Normal)) LoadTotalMessage(FI);
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            FinishMails = 0;
            TransforMessage(PSTFile.RootFolder, PSTFile.RootFolder, "Root");
            LabelSchedule.Text = TransforSchdule;
        }


        private void TransforMessage(FolderInfo MessageFolder, FolderInfo ParentFolder, String FolderPath)
        {
            ImapFolderInfo IMAPWorkFolder;
            //List<MapiMessage> MessageSource = new List<MapiMessage>();
            //List<MailMessage> MessageTarget = new List<MailMessage>();
            if (FolderPath == "Root")
            {
                List<FolderInfo> NextLevelFolders = PSTFile.RootFolder.GetSubFolders(FolderKind.Normal).ToList();
                foreach (FolderInfo PSTWorkFolder in NextLevelFolders)
                {
                    if (FolderNameInboxes.Contains(PSTWorkFolder.DisplayName))
                    {
                        if (!IMAPWorker.ExistFolder(FolderInbox)) IMAPWorker.CreateFolder(FolderInbox);
                        IMAPWorker.ExistFolder(FolderInbox, out IMAPWorkFolder);
                        IMAPWorker.CurrentFolder = IMAPWorkFolder;
                    }
                    if (FolderNameSents.Contains(PSTWorkFolder.DisplayName))
                    {
                        if (!IMAPWorker.ExistFolder(FolderSent)) IMAPWorker.CreateFolder(FolderSent);
                        IMAPWorker.ExistFolder(FolderSent, out IMAPWorkFolder);
                        IMAPWorker.CurrentFolder = IMAPWorkFolder;
                    }
                    if (FolderNameDeletes.Contains(PSTWorkFolder.DisplayName))
                    {
                        if (!IMAPWorker.ExistFolder(FolderDelete)) IMAPWorker.CreateFolder(FolderDelete);
                        IMAPWorker.ExistFolder(FolderDelete, out IMAPWorkFolder);
                        IMAPWorker.CurrentFolder = IMAPWorkFolder;
                    }
                    if (!FolderNameBuiltin.Contains(PSTWorkFolder.DisplayName))
                    {
                        if (!IMAPWorker.ExistFolder(PSTWorkFolder.DisplayName)) IMAPWorker.CreateFolder(PSTWorkFolder.DisplayName);
                        IMAPWorker.ExistFolder(PSTWorkFolder.DisplayName, out IMAPWorkFolder);
                        IMAPWorker.CurrentFolder = IMAPWorkFolder;
                    }
                    //MessageSource = PSTWorkFolder.EnumerateMapiMessages().ToList();
                    //foreach (MapiMessage WorkMessage in MessageSource)
                    //    MessageTarget.Add(WorkMessage.ToMailMessage(TransforOptionConvert));
                    //IMAPWorker.AppendMessages(MessageTarget);
                    MessageCloneToIMAP(PSTWorkFolder.EnumerateMapiMessages());
                }
            }
            else
            {
                //---------非root文件夹
            }
            //---------下探下级子文件夹



        }
        private void MessageCloneToIMAP(IEnumerable<MapiMessage> MessageSource)
        {
            List<MailMessage> MessageTarget = new List<MailMessage>();
            foreach (MapiMessage WorkMessage in MessageSource.ToList())
                MessageTarget.Add(WorkMessage.ToMailMessage(TransforOptionConvert));
            IMAPWorker.AppendMessages(MessageTarget);
            FinishMails += MessageSource.Count();
        }





        private String TransforSchdule
        {
            get
            {
                if (FinishMails == TotalMails) return "100%";
                return $"{(Double)FinishMails / (Double)TotalMails:0.0%}";
            }
        }

    }





}
