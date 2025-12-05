namespace SecretSantaBackend.Models
{

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public ICollection<Pair> DrawnPair { get; set; } = new List<Pair>();
        public ICollection<Pair> GiverPair { get; set; } = new List<Pair>();
    }
}

