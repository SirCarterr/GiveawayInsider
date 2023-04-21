using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveAwayInsider_Models
{
    public class SettingsDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Theme { get; set; } = "light";
    }
}
