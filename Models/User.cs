using Microsoft.AspNetCore.Identity;

namespace SecretSantaBackend.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}