using System.Threading.Tasks;

namespace Application.Infra.CrossCuting.Identity.Interfaces
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}