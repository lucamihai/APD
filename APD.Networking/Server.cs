﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using APD.Networking.Entities;
using APD.Networking.Utilities;

namespace APD.Networking
{
    public class Server
    {
        private readonly TcpListener tcpListener;
        private readonly Dictionary<string, Listener> listeners;
        private readonly Thread threadNewConnections;
        private readonly MessageMapper messageMapper;

        public int Port { get; }

        public Server(int port)
        {
            // TODO: Check if port is already used
            Port = port;

            tcpListener = new TcpListener(System.Net.IPAddress.Any, port);
            tcpListener.Start();

            listeners = new Dictionary<string, Listener>();

            threadNewConnections = new Thread(HandleNewConnections);
            threadNewConnections.Start();

            messageMapper = new MessageMapper();
        }

        public void Stop()
        {
            tcpListener.Stop();

            threadNewConnections.Abort();

            foreach (var listener in listeners.Values)
            {
                listener.Thread.Abort();
                listener.TcpClient.Close();
            }
        }

        private void HandleNewConnections()
        {
            while (true)
            {
                var tcpClient = tcpListener.AcceptTcpClient();
                AddTcpClient(tcpClient);

                NotifyClientOfOtherConnectedClients(tcpClient);
                NotifyOtherClientsOfClientConnection(tcpClient);
            }
        }

        private void HandleMessages(TcpClient tcpClient)
        {
            while (true)
            {
                var stream = tcpClient.GetStream();
                var streamReader = new StreamReader(stream);
                var streamMessage = streamReader.ReadLine();
                var message = messageMapper.GetMessageFromString(streamMessage);

                InterpretMessage(message, tcpClient);

                if (!tcpClient.Connected)
                {
                    break;
                }
            }
        }

        private void InterpretMessage(Message message, TcpClient sender)
        {
            var messageType = message.MessageType;

            if (messageType == MessageType.NotRecognized)
            {
                return;
            }

            else if (messageType == MessageType.SendUsername)
            {

            }

            else if (messageType == MessageType.Disconnect)
            {
                var senderListener = listeners[message.SourceUsername];
                senderListener?.Stop();
            }

            else if (messageType == MessageType.Chat)
            {
                var otherClients = listeners
                    .Values
                    .Select(x => x.TcpClient)
                    .Where(x => x != sender);

                foreach (var tcpClient in otherClients)
                {
                    SendMessageToClient(message, tcpClient);
                }
            }
        }

        private void SendMessageToClient(Message message, TcpClient tcpClient)
        {
            var stream = tcpClient.GetStream();
            var streamWriter = new StreamWriter(stream);
            var messageString = messageMapper.GetStringFromMessage(message);

            streamWriter.WriteLine(messageString);
            streamWriter.Flush();
        }

        private void AddTcpClient(TcpClient tcpClient)
        {
            // TODO: Logic here to determine client username and add to tcpClients
            var username = $"user{listeners.Count + 1}";

            if (listeners.ContainsKey(username))
            {
                // TODO: Prompt for new username
                
            }
            else
            {
                var listener = new Listener
                {
                    TcpClient = tcpClient,
                    Thread = new Thread(() => HandleMessages(tcpClient))
                };

                listeners.Add(username, listener);
                listener.Thread.Start();
            }
        }

        private void NotifyClientOfOtherConnectedClients(TcpClient tcpClient)
        {
            var otherClientsListeners = listeners
                .Values
                .Where(x => x.TcpClient != tcpClient);

            foreach (var otherClientListener in otherClientsListeners)
            {
                var message = new Message
                {
                    MessageType = MessageType.ClientConnected,
                    Value = listeners.First(x => x.Value == otherClientListener).Key
                };

                SendMessageToClient(message, tcpClient);
            }
        }

        private void NotifyOtherClientsOfClientConnection(TcpClient tcpClient)
        {
            var message = new Message
            {
                MessageType = MessageType.ClientConnected,
                Value = listeners.First(x => x.Value.TcpClient == tcpClient).Key
            };

            var otherClients = listeners
                .Values
                .Select(x => x.TcpClient)
                .Where(x => x != tcpClient);

            foreach (var otherTcpClient in otherClients)
            {
                SendMessageToClient(message, otherTcpClient);
            }
        }
    }
}