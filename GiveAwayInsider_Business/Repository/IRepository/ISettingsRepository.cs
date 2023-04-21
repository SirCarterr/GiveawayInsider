using GiveAwayInsider_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveAwayInsider_Business.Repository.IRepository
{
    public interface ISettingsRepository
    {
        Task<SettingsDTO> CreateSettings(SettingsDTO settingsDto);
        Task<SettingsDTO> UpdateSettings(SettingsDTO settingsDto);
        Task<SettingsDTO> GetSettings(string userId);
    }
}
