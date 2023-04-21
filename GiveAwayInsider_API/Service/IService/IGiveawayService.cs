﻿using GiveAwayInsider_Models;

namespace GiveAwayInsider_API.Service.IService
{
    public interface IGiveawayService
    {
        Task<ResponseModelDTO> GetGiveaways(string platform = "", string type = "", string sortBy = "");
    }
}
