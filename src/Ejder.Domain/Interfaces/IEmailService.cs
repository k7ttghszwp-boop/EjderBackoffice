using System.Threading.Tasks;

namespace Ejder.Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendNewTourNotificationAsync(string tourName, string tourUrl);
    }
}
