﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using GeneralLib;

namespace Server
{
    static class ClientManager
    {
        public static Dictionary<int, Client> client = new Dictionary<int, Client>();

        public static void CreateNewConnection(TcpClient tempClient)
        {
            Client newClient = new Client();
            newClient.socket = tempClient;
            newClient.connectionID = ((IPEndPoint)tempClient.Client.RemoteEndPoint).Port;
            newClient.Start();
            client.Add(newClient.connectionID, newClient);

            DataSender.SendWelcomeMessage(newClient.connectionID);
        }

        public static void SendDataTo(int connectioID,byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((data.GetUpperBound(0) - data.GetLowerBound(0) + 1));
            buffer.WriteBytes(data);

            client[connectioID].Stream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
            buffer.Dispose();
        }
    }
}
