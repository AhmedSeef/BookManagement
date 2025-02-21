using Microsoft.AspNetCore.Identity;


namespace BookManagement.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
