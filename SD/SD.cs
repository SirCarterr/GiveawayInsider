using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveAwayInsider_Common
{
    public static class SD
    {
        public const string Local_Token = "Local_Token";
        
        public const string Settings = "Settings";
        
        public const string Search_Instriction_html = "<p>Search is performed by giveaways title. Also you can use such commands in search bar:</p>" +
            "<ul><li>|pf:'(value)' - specify a platform of giveaway (e.g. |pf:steam; |pf:steam.epic-games-store.ps5; etc)</li><li>|t:'(value)' - specify a type of giveaway (e.g. |t:game; |t:loot; etc)</li>" +
            "<li>|s:'popularity|value|date' - sort search by one of 3 criterias</li></ul>";
    }
}
