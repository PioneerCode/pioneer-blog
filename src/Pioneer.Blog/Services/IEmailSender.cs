using System.Threading.Tasks;

namespace Pioneer.Blog.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
