using AutoMapper;
using GiveAwayInsider_Business.Repository.IRepository;
using GiveAwayInsider_DataAccess;
using GiveAwayInsider_DataAccess.Data;
using GiveAwayInsider_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveAwayInsider_Business.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public NotificationRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<NotificationDTO> CreateNotification(NotificationDTO notificationDTO)
        {
            Notification notification = _mapper.Map<NotificationDTO, Notification>(notificationDTO);
            _db.Notifications.Add(notification);
            await _db.SaveChangesAsync();
            return _mapper.Map<Notification, NotificationDTO>(notification);
        }

        public async Task<int> DeleteNotification(int id)
        {
            var notification = _db.Notifications.FirstOrDefault(n => n.Id == id);
            if (notification != null)
            {
                _db.Notifications.Remove(notification);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<NotificationDTO> Get(int id)
        {
            var notification = await _db.Notifications.FirstOrDefaultAsync(n => n.Id == id);
            if (notification != null)
            {
                return _mapper.Map<Notification, NotificationDTO>(notification);
            }
            return new();
        }

        public async Task<IEnumerable<NotificationDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationDTO>>(_db.Notifications);
        }

        public async Task<IEnumerable<NotificationDTO>> GetNotifications(string userId)
        {
            return _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationDTO>>(_db.Notifications.Where(n => n.UserId.Equals(userId)));
        }

        public async Task<NotificationDTO> UpdateNotification(NotificationDTO notificationDTO)
        {
            var notification = _db.Notifications.FirstOrDefault(n => n.Id == notificationDTO.Id);
            if (notification != null)
            {
                notification.Search = notificationDTO.Search;
                notification.IsDisabled = notificationDTO.IsDisabled;
                notification.Platform = notificationDTO.Platform;
                notification.Type = notificationDTO.Type;
                notification.Sort = notificationDTO.Sort;
                _db.Notifications.Update(notification);
                await _db.SaveChangesAsync();
                return _mapper.Map<Notification, NotificationDTO>(notification);
            }
            return notificationDTO;
        }
    }
}
