using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveAwayInsider_Models
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "Phrase is required")]
        [MaxLength(100, ErrorMessage = "Max length is 100 characters")]
        public string Search { get; set; }
        [RegularExpression(@"^(\.?[\w\d-]+)+$", ErrorMessage = "Wrong pattern. Must not contain whitespaces and special charactes expect \'-\'")]
        public string? Platform { get; set; }
        [RegularExpression(@"^\w+$", ErrorMessage = "Wrong pattern. Must contain only alphabetic characters")]
        public string? Type { get; set; }
        public string? Sort { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
