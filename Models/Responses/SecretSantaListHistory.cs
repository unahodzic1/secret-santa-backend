namespace SecretSantaBackend.Models.Responses
{
    public class SecretSantaListHistory
    {
        public int ListId { get; set; }
        public DateTime CreatedDate { get; set; } 
        public List<PairHistory> Pairs { get; set; } = new();
        public EmployeeHistory? UnpairedEmployee { get; set; } 
        public int TotalEmployees { get; set; } 
    }

    public class PairHistory
    {
        public string GiverName { get; set; } = string.Empty;
        public string GiverSurname { get; set; } = string.Empty;
        public string GiverEmail { get; set; } = string.Empty;
        public string ReceiverName { get; set; } = string.Empty;
        public string ReceiverSurname { get; set; } = string.Empty;
        public string ReceiverEmail { get; set; } = string.Empty;
        public DateTime DateGenerated { get; set; }
    }

    public class EmployeeHistory
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}