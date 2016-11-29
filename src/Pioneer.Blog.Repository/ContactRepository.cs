using Pioneer.Blog.DAL;
using Pioneer.Blog.DAL.Entites;
using System.Linq;

namespace Pioneer.Blog.Repository
{
    public interface IContactRepository
    {
        ContactEntity Add(ContactEntity contactEntity);
        ContactEntity GetByEmail(string email);
    }

	public class ContactRepository : IContactRepository
    {
        private readonly BlogContext _blogContext;

        public ContactRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public ContactEntity Add(ContactEntity contactEntity)
        {
            _blogContext
                .Contacts
                .Add(contactEntity);
            _blogContext.SaveChanges();

            return contactEntity;
        }

        /// <summary>
        /// Get by email address
        /// </summary>
        /// <returns>Contact Entity</returns>
        public ContactEntity GetByEmail(string email)
        {
            return _blogContext
                    .Contacts
                    .FirstOrDefault(x => x.Email == email);
        }
    }
}