using GiveAwayInsider_Models;

namespace GiveAwayInsider_Client.Service.IService
{
    public interface ISettingsService
    {
        Task<SettingsDTO> Get(string userId);
        Task<SettingsDTO> Update(SettingsDTO settingsDTO);
    }
}
