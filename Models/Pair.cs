namespace SecretSantaBackend.Models
{

    public class Pair
    {
        public int Id { get; set; }
        public int GiverId { get; set; }
        public Employee? Giver { get; set; } // varijabla moze biti null
        public int ReceiverId { get; set; }
        public Employee? Receiver { get; set; }
        public int ListId { get; set; }
        public SecretSantaList? List { get; set; }
    }
}

