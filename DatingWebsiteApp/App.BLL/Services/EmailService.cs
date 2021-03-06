﻿using App.BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using MailKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using System.Net;

namespace App.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHostingEnvironment _appEnvironment;

        public EmailService(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Site administration", "messendertest@mail.ru"));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync("smtp.mail.ru", 587, MailKit.Security.SecureSocketOptions.Auto);
                    await client.AuthenticateAsync("messendertest@mail.ru", "15975310895623Vladimir!");
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
