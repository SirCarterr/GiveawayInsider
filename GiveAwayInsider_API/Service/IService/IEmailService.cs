using GiveAwayInsider_Models;

namespace GiveAwayInsider_API.Service.IService
{
    public interface IEmailService
    {
        Task<ResponseModelDTO> SendPasswordRecovery(string email);
        Task<ResponseModelDTO> SendNotification(IEnumerable<GiveawayDTO> giveaways, int notificationId);
    }
}
