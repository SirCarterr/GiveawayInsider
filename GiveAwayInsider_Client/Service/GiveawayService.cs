using GiveAwayInsider_Client.Service.IService;
using GiveAwayInsider_Models;
using Newtonsoft.Json;

namespace GiveAwayInsider_Client.Service
{
    public class GiveawayService : IGiveawayService
    {
        private readonly HttpClient _client;

        public GiveawayService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ResponseModelDTO> GetGiveaways(string platform, string type, string sortBy)
        {
            var response = await _client.GetAsync($"api/giveaway?platform={platform}&type={type}&sortBy={sortBy}");

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<GiveawayDTO>>(contentTemp);
                return new()
                {
                    IsSuccess = true,
                    Data = result!
                };
            }
            else if ((int)response.StatusCode == 404)
            {
                return new()
                {
                    IsSuccess = false,
                    Errors = "Entry not found..."
                };
            }
            else
            {
                return new()
                {
                    IsSuccess = false,
                    Errors = "Oops, something got wrong..."
                };
            }
        }
    }
}
