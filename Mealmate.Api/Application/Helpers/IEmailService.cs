using System.Threading.Tasks;

namespace Mealmate.Api.Helpers
{
    public interface IEmailService
    {
        AuthMessageSenderOptions Options { get; }

        Task Execute(string apiKey, string subject, string message, string email);
        Task SendEmailAsync(string email, string subject, string message);
    }
}