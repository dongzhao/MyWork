using MyWork.Core;
using MyWork.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.CommonService
{
    public class EmailServiceClient : IEmailServiceClient, IDisposable
    {
        private readonly IEmailConfig config;
        private readonly SmtpClient smtpClient;

        public EmailServiceClient(IEmailConfig cfg)
        {
            this.config = cfg;

            smtpClient = new SmtpClient(config.Host, config.Port);

            var username = config.Username;
            var password = config.Password;
            var key = config.Key;
            var plainText = PasswordUtility.Decrypt(password, key);
            smtpClient.Credentials = new System.Net.NetworkCredential(username, plainText);
 
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
        }

        public void Send(MailDto dto)
        {
            if (!config.IsActive)
            {
                return;
            }

            MailMessage mail = new MailMessage();
            mail.Subject = dto.Subject;
            mail.IsBodyHtml = true;
            mail.Body = dto.Body;
            mail.From = new MailAddress(dto.From, null, System.Text.Encoding.UTF8);

            if(dto.ToList != null && dto.ToList.Count > 0)
            {
                AddEmailAddress(mail, dto.ToList);
            }
           
            if (dto.Attachments != null && dto.Attachments.Count > 0)
            {
                AddAttachments(mail, dto.Attachments);
            }

            if (!string.IsNullOrEmpty(dto.SignatureUrl))
            {
                AddSignature(mail, dto.Body, dto.SignatureUrl);
            }
            smtpClient.Send(mail);
        }

        private void AddSignature(MailMessage mail, string body, string imageUrl)
        {
            var imgContentId = Path.GetFileName(imageUrl);
            AlternateView altView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            LinkedResource linkedResource = new LinkedResource(imageUrl, MediaTypeNames.Image.Jpeg);
            linkedResource.ContentId = imgContentId;
            altView.LinkedResources.Add(linkedResource);
            mail.AlternateViews.Add(altView);
        }

        private void AddAttachments(MailMessage mail, List<AttachmentDto> attachments)
        {
            foreach (var att in attachments)
            {
                MemoryStream ms = new MemoryStream();
                StreamWriter writer = new StreamWriter(ms);
                writer.Write(att.FileContent);
                writer.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                Attachment attachment = new Attachment(ms, att.FileName, att.FileContentType);
                mail.Attachments.Add(attachment);
            }
        }

        private void AddEmailAddress(MailMessage mail, List<string> emailList)
        {
            mail.To.Clear();

            var emailSet = new HashSet<string>(emailList);
            //var mailAddressList = new List<MailAddress>();
            foreach (var str in emailSet)
            {
                mail.To.Add(new MailAddress(str));
                //mailAddressList.Add(new MailAddress(str));
            }
            //return mailAddressList;
        }

        public void Dispose()
        {
            smtpClient.Dispose();
        }
    }

    public interface IEmailServiceClient
    {
        void Send(MailDto dto);
    }
}
