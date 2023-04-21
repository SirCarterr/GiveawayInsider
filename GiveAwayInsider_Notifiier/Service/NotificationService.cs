using GiveAwayInsider_Models;
using GiveAwayInsider_Notifiier.Service.IService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveAwayInsider_Notifiier.Service
{
    public class NotificationService : INotificationService
    {
        private readonly HttpClient _client;

        public NotificationService(HttpClient client)
        {
            _client = client;        }

        public async Task<IEnumerable<NotificationDTO>> GetNotifications()
        {
            var response = await _client.GetAsync("api/notification/all");

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<NotificationDTO>>(contentTemp);
                return result!;
            }
            return new List<NotificationDTO>();
        }
    }
}
