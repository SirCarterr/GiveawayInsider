using AspNetCore.Authentication.ApiKey;
using GiveAwayInsider_API.Models;
using System.Security.Claims;

namespace GiveAwayInsider_API.Helper
{
    public class ApiKeyProvider : IApiKeyProvider
    {
        public async Task<IApiKey> ProvideAsync(string key)
        {
            return new ApiKey("<your custom apikey>", "Notifier", new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "notifier"),
                new Claim(ClaimTypes.Email, "test.company.707@gmail.com")
            });
        }
    }
}
