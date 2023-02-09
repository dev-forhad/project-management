using System.ComponentModel.DataAnnotations;
using static project_management_api.Configuration.Enumration;

namespace project_management_api.Model
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public Role UserRole { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public Role Role { get; set; }

    }
}
