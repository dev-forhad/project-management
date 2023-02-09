using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static project_management_api.Configuration.Enumration;

namespace project_management_api.Model
{
    public class User : IdentityUser
    {
       
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
