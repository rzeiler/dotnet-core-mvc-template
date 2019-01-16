using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Urlaub
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}