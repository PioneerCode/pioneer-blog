using System;
using AutoMapper;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Pioneer.Blog.Entites;
using Pioneer.Blog.Models;
using Pioneer.Blog.Models.Views;
using Pioneer.Blog.Repositories;

namespace Pioneer.Blog.Services
{
    public interface ICommunicationService
    {
        OperationResult<ContactViewModel> SendContactEmailNotification(ContactViewModel model);
        OperationResult<Contact> SignUpToMailingList(string email);
    }

    public class CommunicationService : ICommunicationService
    {
        private readonly IOptions<AppConfiguration> _appConfiguration;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public CommunicationService(IOptions<AppConfiguration> appConfiguration, IContactRepository contactRepository, IMapper mapper)
        {
            _appConfiguration = appConfiguration;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// When an notification email that someone is trying to contact you.
        /// </summary>
        public OperationResult<ContactViewModel> SendContactEmailNotification(ContactViewModel model)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_appConfiguration.Value.SiteTitle + " - Contact", model.Email));
            message.To.Add(new MailboxAddress(_appConfiguration.Value.SiteTitle + " - Contact", _appConfiguration.Value.SendEmailTo));
            message.Subject = _appConfiguration.Value.SiteTitle + " - Contact: " + model.Subject;
            message.Body = new TextPart("html")
            {
                Text = $"<p>Email From: {model.Name} ({model.Email})</p><p>Message:</p><p>{model.Message}</p>"
            };

            try
            {
                using (var client = new SmtpClient(new ProtocolLogger("smtp.log")))
                {
                    client.Connect(_appConfiguration.Value.EmailHost, Convert.ToInt32(_appConfiguration.Value.EmailPort));
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
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
        public OperationResult<Contact> SignUpToMailingList(string email)
        {
            if (_contactRepository.GetByEmail(email) != null)
            {
                return new OperationResult<Contact>(null, OperationStatus.Error, "This email has already been added to the mailing list.");
            }

            var response = _contactRepository.Add(_mapper.Map<Contact, ContactEntity>(new Contact { Email = email }));
            return new OperationResult<Contact>(_mapper.Map<ContactEntity, Contact>(response), OperationStatus.Created);
        }
    }
}
