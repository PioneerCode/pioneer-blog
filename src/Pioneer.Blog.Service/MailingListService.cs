using System;
using System.Threading.Tasks;
using AutoMapper;
using Pioneer.Blog.DAL;
using Pioneer.Blog.DAL.Entites;
using Pioneer.Blog.Model;
using Pioneer.Blog.Model.Views;
using Pioneer.Blog.Repository;

namespace Pioneer.Blog.Service
{
    public interface IMailingListService
    {
        OperationResult<Contact> SignUp(SignUpViewModel model);
    }

    public class MailingListService : IMailingListService
    {
        private readonly IContactRepository _contactRepository;

        public MailingListService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// Add email to contacts table if it does not already exist 
        /// </summary>
        public OperationResult<Contact> SignUp(SignUpViewModel model)
        {
            if (_contactRepository.GetByEmail(model.Email) != null)
            {
                return new OperationResult<Contact>(null, OperationStatus.Error, "This email has already been added to the mailing list.");
            }

            var response = _contactRepository.Add(Mapper.Map<Contact, ContactEntity>(new Contact {Email = model.Email}));
            return new OperationResult<Contact>(Mapper.Map<ContactEntity, Contact>(response), OperationStatus.Created);
        }
    }
}
