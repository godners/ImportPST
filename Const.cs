using Aspose.Email;
using Aspose.Email.Clients;
using Aspose.Email.Clients.Exchange.WebService.Schema_2016;
using Aspose.Email.Clients.Imap;
using Aspose.Email.Mapi;
using Aspose.Email.Storage.Pst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportPST
{
    internal static class Const
    {
        internal const String DomainName = "cicc.group";

        //internal const String FolderInbox = "INBOX"; //Aliyun
        //internal const String FolderSent = "已发送"; //Aliyun
        //internal const String FolderDelete = "已删除邮件"; //Aliyun
        internal const String FolderInbox = "INBOX"; //Coremail
        internal const String FolderSent = "Sent Items"; //Coremail
        internal const String FolderDelete = "Trash"; //Coremail

        internal static String[] FolderNameInboxes = { "收件箱", "INBOX", "Inbox" };
        internal static String[] FolderNameSents = { "已发送", "已发送邮件", "SENT", "Sent" };
        internal static String[] FolderNameDeletes = { "已删除", "已删除邮件", "DELETED", "Deleted" };
        internal static String[] FolderNameBuiltin = FolderNameInboxes.Concat(FolderNameSents).Concat(FolderNameDeletes).ToArray();
        internal static MailConversionOptions TransforOptionConvert = new MailConversionOptions()
        {
            ConvertAsTnef = false, KeepOriginalEmailAddresses = true,
            PreserveEmbeddedMessageFormat = true, PreserveRtfContent = true,
        };
        internal static SaveOptions TransforOptionSave = SaveOptions.CreateSaveOptions(MailMessageSaveType.EmlFormat);

        internal static String BuiltinFolderName(FolderInfo CheckFolder)
        {
            if (FolderNameInboxes.Contains(CheckFolder.DisplayName)) return FolderInbox;
            if (FolderNameSents.Contains(CheckFolder.DisplayName)) return  FolderSent;
            if (FolderNameDeletes.Contains(CheckFolder.DisplayName)) return FolderDelete;
            return CheckFolder.DisplayName;
        }


    }
}
