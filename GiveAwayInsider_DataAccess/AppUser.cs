﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveAwayInsider_DataAccess
{
    public class AppUser : IdentityUser
    {
        [Required]
        [StringLength(20)]
        public string NickName { get; set; }
    }
}
