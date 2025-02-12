using Microsoft.AspNetCore.Identity;

namespace Test2.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
    }
}
