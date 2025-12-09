using System;

namespace SecretSantaBackend.Models
{
    public class Employee
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Pair> DrawnPair { get; set; } = new List<Pair>();
        public ICollection<Pair> GiverPair { get; set; } = new List<Pair>();
    }
}