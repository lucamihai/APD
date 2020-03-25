using APD.Networking.Entities;
using Newtonsoft.Json;

namespace APD.Networking.Utilities
{
    public class MessageMapper
    {
        public Message GetMessageFromString(string messageString)
        {
            return JsonConvert.DeserializeObject<Message>(messageString);
        }

        public string GetStringFromMessage(Message message)
        {
            return JsonConvert.SerializeObject(message);
        }
    }
}