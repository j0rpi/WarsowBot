using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace QuakeRcon
{
    class RCON
    {   

        public string sendCommand(string rconCommand, string gameServerIP, string password, int gameServerPort)
        {
            //connecting to server
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            client.Connect(IPAddress.Parse(gameServerIP), gameServerPort);

            string command;
            command = "rcon " + password + " " + rconCommand;
            byte[] bufferTemp = Encoding.ASCII.GetBytes(command);
            byte[] bufferSend = new byte[bufferTemp.Length + 5];

            //intial 5 characters as per standard
            bufferSend[0] = byte.Parse("255");
            bufferSend[1] = byte.Parse("255");
            bufferSend[2] = byte.Parse("255");
            bufferSend[3] = byte.Parse("255");
            bufferSend[4] = byte.Parse("02");
            int j = 5;

            for (int i = 0; i < bufferTemp.Length; i++)
            {
                bufferSend[j++] = bufferTemp[i];
            }

            //send rcon command and get response
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            client.Send(bufferSend, SocketFlags.None);

            //big enough to receive response
            byte[] bufferRec = new byte[65000];
            client.Receive(bufferRec);            
            return Encoding.ASCII.GetString(bufferRec);
        }
    }
}
