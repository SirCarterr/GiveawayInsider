using AutoMapper;
using GiveAwayInsider_DataAccess;
using GiveAwayInsider_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveAwayInsider_Business.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Settings, SettingsDTO>().ReverseMap();
            CreateMap<Notification, NotificationDTO>().ReverseMap();
        }
    }
}
