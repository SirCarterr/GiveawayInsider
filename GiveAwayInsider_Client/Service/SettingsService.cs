using GiveAwayInsider_Client.Service.IService;
using GiveAwayInsider_Models;
using Newtonsoft.Json;
using System.Text;

namespace GiveAwayInsider_Client.Service
{
    public class SettingsService : ISettingsService
    {
        private readonly HttpClient _client;

        public SettingsService(HttpClient client)
        {
            _client = client;
        }

        public async Task<SettingsDTO> Get(string userId)
        {
            var response = await _client.GetAsync($"api/settings/get/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SettingsDTO>(contentTemp);
                return result!;
            }
            return new();
        }

        public async Task<SettingsDTO> Update(SettingsDTO settingsDTO)
        {
            var content = JsonConvert.SerializeObject(settingsDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/settings/update", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SettingsDTO>(contentTemp);
                return result!;
            }
            return settingsDTO;
        }
    }
}
