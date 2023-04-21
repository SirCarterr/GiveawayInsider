using GiveAwayInsider_API.Service.IService;
using GiveAwayInsider_Models;
using Newtonsoft.Json;

namespace GiveAwayInsider_API.Service
{
    public class GiveawayService : IGiveawayService
    {
        private readonly HttpClient _client;

        public GiveawayService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ResponseModelDTO> GetGiveaways(string? platform = "", string? type = "", string? sortBy = "")
        {
            string request = "https://www.gamerpower.com/api/giveaways";

            if (!string.IsNullOrEmpty(platform))
                request += "?platform=" + platform;

            if (!string.IsNullOrEmpty(type))
            {
                if (request.Contains('?'))
                    request += "&type=" + type;
                else
                    request += "?type=" + type;
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (request.Contains('?'))
                    request += "&sort-by=" + sortBy;
                else
                    request += "?sort-by=" + sortBy;
            }

            var response = await _client.GetAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<GiveawayDTO>>(contentTemp);

                return new()
                {
                    StatusCode = (int)response.StatusCode,
                    IsSuccess = true,
                    Data = result!
                };
            }

            return new()
            {
                StatusCode = (int)response.StatusCode,
                IsSuccess = false
            };
        }
    }
}
