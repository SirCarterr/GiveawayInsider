using GiveAwayInsider_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveAwayInsider_Business.Repository.IRepository
{
    public interface INotificationRepository
    {
        Task<NotificationDTO> CreateNotification(NotificationDTO notificationDTO);
        Task<NotificationDTO> UpdateNotification(NotificationDTO notificationDTO);
        Task<IEnumerable<NotificationDTO>> GetNotifications(string userId);
        Task<IEnumerable<NotificationDTO>> GetAll();
        Task<NotificationDTO> Get(int id);
        Task<int> DeleteNotification(int id);
    }
}
