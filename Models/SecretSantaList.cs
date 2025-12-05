using System;
using System.Collections.Generic;

namespace SecretSantaBackend.Models
{

    public class SecretSantaList
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int? UnpairedEmployeeId { get; set; } = null;
        public List<Pair> Pairs { get; set; } = new();
        public Employee? UnpairedEmployee { get; set; } = null;
    }
}