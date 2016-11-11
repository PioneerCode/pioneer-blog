using System;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Pioneer.Blog.DAL;
using Pioneer.Blog.Model;
using Pioneer.Blog.Model.Views;

namespace Pioneer.Blog.Service
{
    public interface IContactService
    {
        OperationResult<ContactViewModel> Send(ContactViewModel model);
    }

    public class ContactService : IContactService
    {
        private readonly IOptions<AppConfiguration> _appConfiguration;

        public ContactService(IOptions<AppConfiguration> appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        /// <summary>
        /// Send contact email
        /// </summary>
        public OperationResult<ContactViewModel> Send(ContactViewModel model)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_appConfiguration.Value.SiteTitle + " - Contact", model.Email));
            message.To.Add(new MailboxAddress(_appConfiguration.Value.SiteTitle + " - Contact", _appConfiguration.Value.EmailUsername));
            message.Subject = _appConfiguration.Value.SiteTitle + " - Contact: " + model.Subject;
            message.Body = new TextPart("html")
            {
                Text = string.Format("<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>", model.Name, model.Email, model.Message)
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(_appConfiguration.Value.EmailHost, Convert.ToInt32(_appConfiguration.Value.EmailPort), false);
                    client.Authenticate(_appConfiguration.Value.EmailUsername, _appConfiguration.Value.EmailPassword);
                    client.Send(message);
                    client.Disconnect(true);
                    return new OperationResult<ContactViewModel>(model, OperationStatus.Ok);
                }
            }
            catch (Exception)
            {
                // TODO: Log
                return new OperationResult<ContactViewModel>(model, OperationStatus.Error);
            }
        }
    }
}
