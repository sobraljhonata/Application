using System.Threading.Tasks;

namespace Application.Infra.CrossCuting.Identity.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}