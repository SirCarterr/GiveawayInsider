using GiveAwayInsider_Models;

namespace GiveAwayInsider_Client.Service.IService
{
    public interface INotificationService
    {
        Task<NotificationDTO> Create(NotificationDTO notificationDTO);
        Task<NotificationDTO> Update(NotificationDTO notificationDTO);
        Task<bool> Delete(int id);
        Task<IEnumerable<NotificationDTO>> Get(string userId);
    }
}
