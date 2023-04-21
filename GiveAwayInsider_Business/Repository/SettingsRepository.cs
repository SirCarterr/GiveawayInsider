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
    public class SettingsRepository : ISettingsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public SettingsRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<SettingsDTO> CreateSettings(SettingsDTO settingsDto)
        {
            Settings settings = _mapper.Map<SettingsDTO, Settings>(settingsDto);
            _db.UsersSettings.Add(settings);
            await _db.SaveChangesAsync();
            return _mapper.Map<Settings, SettingsDTO>(settings);
        }

        public async Task<SettingsDTO> GetSettings(string userId)
        {
            var settings = await _db.UsersSettings.FirstOrDefaultAsync(s => s.UserId.Equals(userId));
            if (settings != null)
            {
                return _mapper.Map<Settings, SettingsDTO>(settings!);
            }
            return new();
        }

        public async Task<SettingsDTO> UpdateSettings(SettingsDTO settingsDto)
        {
            var settings = _db.UsersSettings.FirstOrDefault(s => s.Id == settingsDto.Id);
            if (settings != null)
            {
                settings.Theme = settingsDto.Theme;
                _db.UsersSettings.Update(settings);
                await _db.SaveChangesAsync();
                return _mapper.Map<Settings, SettingsDTO>(settings);
            }
            return settingsDto;
        }
    }
}
