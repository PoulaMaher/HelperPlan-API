using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HelperPlan.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? Location { get; set; }
        public string? description { get; set; }
    }
}
