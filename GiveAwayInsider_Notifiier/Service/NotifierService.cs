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
    public class NotifierService : INotifierService
    {
        private readonly HttpClient _client;

        public NotifierService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> Notify(IEnumerable<GiveawayDTO> giveaways, int notificationId)
        {
            var content = JsonConvert.SerializeObject(giveaways);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"api/giveaway/notify/{notificationId}", bodyContent);

            return response.IsSuccessStatusCode;
        }
    }
}
