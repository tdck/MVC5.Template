﻿using System;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;

namespace MvcTemplate.Components.Mail
{
    public class SmtpMailClient : IMailClient
    {
        private SmtpClient client;
        private Boolean disposed;
        private String sender;

        public SmtpMailClient()
        {
            sender = ((SmtpSection)WebConfigurationManager.GetSection("system.net/mailSettings/smtp")).From;
            client = new SmtpClient();
        }

        public void Send(String to, String subject, String body)
        {
            MailMessage email = new MailMessage(sender, to, subject, body);
            email.SubjectEncoding = Encoding.UTF8;
            email.BodyEncoding = Encoding.UTF8;
            email.IsBodyHtml = true;

            client.Send(email);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(Boolean disposing)
        {
            if (disposed) return;

            client.Dispose();

            disposed = true;
        }
    }
}
