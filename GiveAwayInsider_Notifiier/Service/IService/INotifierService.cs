using GiveAwayInsider_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveAwayInsider_Notifiier.Service.IService
{
    public interface INotifierService
    {
        Task<bool> Notify(IEnumerable<GiveawayDTO> giveaways, int notificationId);
    }
}
