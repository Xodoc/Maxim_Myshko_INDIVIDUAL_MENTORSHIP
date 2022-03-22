#nullable disable

using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class AuthorizationRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
