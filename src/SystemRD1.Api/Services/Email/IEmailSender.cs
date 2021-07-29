using System.Threading.Tasks;

namespace SystemRD1.Api.Services.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
