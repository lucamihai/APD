namespace APD.Networking.Entities
{
    public enum MessageType
    {
        Chat,
        ClientConnected,
        ClientDisconnected,
        SendUsername,
        RequestUsername,
        Ok,
        Repeat,
        Disconnect,
        NotRecognized
    }
}