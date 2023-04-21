using System.ComponentModel.DataAnnotations;

namespace GiveAwayInsider_Client.Models
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-z0-9_.-]+@[a-zA-z0-9-]+.[a-zA-z0-9-.]+$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
}
