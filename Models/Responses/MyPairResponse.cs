namespace SecretSantaBackend.Models.Responses 
{
    public class MyPairResponse
    {
        public string ReceiverName { get; set; } = default!;
        public string ReceiverSurname { get; set; } = default!;
        public string ReceiverEmail { get; set; } = default!;
    }
}