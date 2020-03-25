namespace APD.Networking.Entities
{
    public class Message
    {
        public MessageType MessageType { get; set; }
        public string Value { get; set; }
        public string SourceUsername { get; set; }
        public string DestinationUsername { get; set; }
    }
}