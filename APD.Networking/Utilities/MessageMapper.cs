using APD.Networking.Entities;
using Newtonsoft.Json;

namespace APD.Networking.Utilities
{
    public class MessageMapper
    {
        public Message GetMessageFromString(string messageString)
        {
            return string.IsNullOrWhiteSpace(messageString) 
                ? new Message {MessageType = MessageType.NotRecognized} 
                : JsonConvert.DeserializeObject<Message>(messageString);
        }

        public string GetStringFromMessage(Message message)
        {
            return message == null 
                ? string.Empty 
                : JsonConvert.SerializeObject(message);
        }
    }
}