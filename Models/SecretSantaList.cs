using System;

namespace SecretSantaBackend.Models
{

    public class SecretSantaList
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public List<Pair> Pairs { get; set; } = new();
        public Employee? UnpairedEmployee { get; set; } = null; // u slucaju da postoji samo jedan igrac ili posljednjem igracu preostane da izabere samo sam sebe
    }
}

