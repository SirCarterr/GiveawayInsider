using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveAwayInsider_Models
{
    public class SignInRequestDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-z0-9_.-]+@[a-zA-z0-9-]+.[a-zA-z0-9-.]+$", ErrorMessage = "Invalid email address")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
