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
        private Int32 TotalMails, FinishMails, TotalFolders, FinishFolders;


        private void WinMain_Load(object sender, EventArgs e)
        {
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
            Boolean PSTChecked = CheckPSTFile(out String CheckMessage);
            if (!PSTChecked) { MessageBox.Show(CheckMessage, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            PSTChecked = CheckIMAPServer(out CheckMessage);
            if (!PSTChecked) { MessageBox.Show(CheckMessage, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            MessageBox.Show($"从{(new FileInfo(LabelFile.Text)).Name}中找到\r\n{TotalFolders} 个文件夹，" +
                $"{TotalMails} 封邮件。\r\n按“开始进”行导入。", "确认成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LabelFile.Enabled = false; ButtonFile.Enabled = false;
            TextBoxUsnm.Text = TextBoxUsnm.Text.Split('@')[0];
            TextBoxUsnm.Enabled = false; TextBoxPswd.Enabled = false;
            ButtonCheck.Enabled = false; ButtonStart.Enabled = true;
        }
        private Boolean CheckPSTFile(out String Message)
        {
            // ↓↓↓PST文件不存在
            if (!File.Exists(LabelFile.Text))
            { Message = $"{LabelFile.Text}\r\n文件不存在。"; return false; }
            // ↓↓↓PST文件无法读取
            try { PSTFile = PersonalStorage.FromFile(LabelFile.Text); }
            catch { Message = $"{LabelFile.Text}\r\n无法读取。"; return false; }
            // ↓↓↓PST文件格式错误
            if (PSTFile.Format != FileFormat.Pst)
            { Message = $"{LabelFile.Text}\r\n格式错误。"; return false; }
            // ↓↓↓提取总邮件数
            TotalMails = 0; TotalFolders = 0; LoadTotalItem(PSTFile.RootFolder);
            if (TotalMails == 0)
            { Message = $"从{(new FileInfo(LabelFile.Text)).Name}中未找到邮件"; return false; }
            Message = String.Empty; return true;
        }
        private Boolean CheckIMAPServer(out String Message)
        {
            // ↓↓↓无法获取IMAP服务器
            try { GetIMAPServer_Aliyun(); /*GetIMAPServer();*/ }
            catch { Message = $"用户名或密码不正确。"; return false; }
            // ↓↓↓连接IMAP服务器
            IMAPWorker = new ImapClient()
            {
                Host = IMAPServer,
                Username = /*TextBoxUsnm.Text + "@cicc.com.cn"*/ "pst03@tianyue.ren",
                Password = TextBoxPswd.Text,
                SecurityOptions = SecurityOptions.Auto,
                AutoCommit = true
            };
            // ↓↓↓无法连接IMAP服务器            
            if (IMAPWorker.ConnectionState != ConnectionState.Open)
            { Message = $"{IMAPServer}\r\n连接失败。"; return false; }
            Message = String.Empty; return true;

        }
        private void GetIMAPServer_CICC()
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
        private void GetIMAPServer_Aliyun() => IMAPServer = "imap.qiye.aliyun.com";
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
        private void LoadTotalItem(FolderInfo FolderCount)
        {
            TotalMails += FolderCount.EnumerateMessagesEntryId().Count(); TotalFolders += 1;
            foreach (FolderInfo FI in FolderCount.GetSubFolders(FolderKind.Normal)) LoadTotalItem(FI);
        }
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            FinishMails = 0; FinishFolders = 0;

            TransforRootMessage();
        }

        private void TransforRootMessage()
        {
            ImapFolderInfo IMAPWorkFolder; String RootFolderName = String.Empty;
            List<FolderInfo> RootLavelFolders = PSTFile.RootFolder.GetSubFolders(FolderKind.Normal).ToList();
            foreach (FolderInfo RootFolderInfo in RootLavelFolders)
            {
                RootFolderName = BuiltinFolderName(RootFolderInfo);
                if (!IMAPWorker.ExistFolder(RootFolderName)) { IMAPWorker.CreateFolder(RootFolderName); }
            }
            foreach (FolderInfo RootFolderInfo in RootLavelFolders)
            {
                RootFolderName = BuiltinFolderName(RootFolderInfo);
                IMAPWorker.ExistFolder(RootFolderName, out IMAPWorkFolder);
                IMAPWorker.CurrentFolder = IMAPWorkFolder;
                MessageCloneToIMAP(RootFolderInfo);
            }
            foreach (FolderInfo RootFolderInfo in RootLavelFolders)
            {
                RootFolderName = BuiltinFolderName(RootFolderInfo);
                if (RootFolderInfo.HasSubFolders)
                    foreach (FolderInfo SubFolderInfo in RootFolderInfo.GetSubFolders())
                        TransforSubMessage(SubFolderInfo, RootFolderName);
            }
        }

        private void TransforSubMessage(FolderInfo MessageFolder, String ParentFolder)
        {
            ImapFolderInfo IMAPWorkFolder, IMAPParentFolder;
            IMAPWorker.ExistFolder(ParentFolder, out IMAPParentFolder);
            IMAPWorker.CurrentFolder = IMAPParentFolder;

            if (!IMAPWorker.ExistFolder(MessageFolder.DisplayName))
                IMAPWorker.CreateFolder(MessageFolder.DisplayName);

            IMAPWorker.ExistFolder(MessageFolder.DisplayName, out IMAPWorkFolder);
            IMAPWorker.CurrentFolder = IMAPWorkFolder;
            MessageCloneToIMAP(MessageFolder);

            foreach (FolderInfo SubFolderInfo in MessageFolder.GetSubFolders(FolderKind.Normal))
                TransforSubMessage(SubFolderInfo, MessageFolder.DisplayName);
        }
        private void MessageCloneToIMAP(FolderInfo MessageFolder)
        {
            IEnumerable<MapiMessage> MessageSource = MessageFolder.EnumerateMapiMessages();
            List<MailMessage> MessageTarget = new List<MailMessage>();
            foreach (MapiMessage WorkMessage in MessageSource.ToList())
                MessageTarget.Add(WorkMessage.ToMailMessage(TransforOptionConvert));
            IMAPWorker.AppendMessages(MessageTarget);
            MessageTarget.Clear();
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
