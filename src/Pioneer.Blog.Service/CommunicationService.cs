using System;
using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Pioneer.Blog.DAL;
using Pioneer.Blog.DAL.Entites;
using Pioneer.Blog.Model;
using Pioneer.Blog.Model.Views;
using Pioneer.Blog.Repository;

namespace Pioneer.Blog.Service
{
    public interface ICommunicationService
    {
        OperationResult<ContactViewModel> SendContactEmailNotification(ContactViewModel model);
        OperationResult<Contact> SignUpToMailingList(SignUpViewModel model);
    }

    public class CommunicationService : ICommunicationService
    {
        private readonly IOptions<AppConfiguration> _appConfiguration;
        private readonly IContactRepository _contactRepository;

        public CommunicationService(IOptions<AppConfiguration> appConfiguration, IContactRepository contactRepository)
        {
            _appConfiguration = appConfiguration;
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// When an notification email that someone is trying to contact you.
        /// </summary>
        public OperationResult<ContactViewModel> SendContactEmailNotification(ContactViewModel model)
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

        /// <summary>
        /// Sign someone up to the mailing list.
        /// </summary>
        public OperationResult<Contact> SignUpToMailingList(SignUpViewModel model)
        {
            if (_contactRepository.GetByEmail(model.Email) != null)
            {
                return new OperationResult<Contact>(null, OperationStatus.Error, "This email has already been added to the mailing list.");
            }

            var response = _contactRepository.Add(Mapper.Map<Contact, ContactEntity>(new Contact { Email = model.Email }));
            return new OperationResult<Contact>(Mapper.Map<ContactEntity, Contact>(response), OperationStatus.Created);
        }
    }
}
