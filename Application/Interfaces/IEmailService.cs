using System.Threading.Tasks;
using Application.User;

namespace Application.Interfaces
{
    public interface IEmailService
    {
         Task SendEmail(EmailDto email);
    }
}