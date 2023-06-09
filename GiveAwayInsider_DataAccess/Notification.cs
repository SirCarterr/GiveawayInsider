﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveAwayInsider_DataAccess
{
    public class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string Search { get; set; }
        public string? Platform { get; set; }
        public string? Type { get; set; }
        public string? Sort { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
