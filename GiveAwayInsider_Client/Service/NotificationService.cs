using GiveAwayInsider_Client.Service.IService;
using GiveAwayInsider_Models;
using Newtonsoft.Json;
using System.Text;

namespace GiveAwayInsider_Client.Service
{
    public class NotificationService : INotificationService
    {
        private readonly HttpClient _client;

        public NotificationService(HttpClient client)
        {
            _client = client;
        }

        public async Task<NotificationDTO> Create(NotificationDTO notificationDTO)
        {
            var content = JsonConvert.SerializeObject(notificationDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/notification/create", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<NotificationDTO>(contentTemp);
                return result!;
            }
            return new();
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _client.DeleteAsync($"api/notification/delete/{id}");

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<IEnumerable<NotificationDTO>> Get(string userId)
        {
            var response = await _client.GetAsync($"api/notification/get/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<NotificationDTO>>(contentTemp);
                return result!;
            }
            return new List<NotificationDTO>();
        }

        public async Task<NotificationDTO> Update(NotificationDTO notificationDTO)
        {
            var content = JsonConvert.SerializeObject(notificationDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/notification/update", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<NotificationDTO>(contentTemp);
                return result!;
            }
            return notificationDTO;
        }
    }
}
