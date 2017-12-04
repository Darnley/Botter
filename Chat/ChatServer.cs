using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Botter
{
    partial class Chat
    {
        public class ChatServer
        {
            public string Host { get; set; }
            public int Port { get; set; }
            public Socket Socket { get; set; }
            private Chat Chat { get; set; }
            
            public ChatServer(Chat OuterChatInstance)
            {
                this.Chat = OuterChatInstance;
            }

            /// <summary>
            /// Send text to server.
            /// </summary>
            /// <param name="data">Text to send</param>
            /// <param name="verbose">Want to write log in Console?</param>
            /// <returns>Returned string from server</returns>
            public string SendData(string data, bool verbose = false)
            {
                data = data + '\x00';

                // Create a TCPClient object at the IP and port no
                TcpClient client = new TcpClient(this.Host, this.Port);
                NetworkStream nwStream = client.GetStream();
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(data);

                // Send the text
                if (verbose) Console.WriteLine(String.Format("Sent: {0}", data));
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                // Read the received data
                byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                string bytesReadEncoded = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                if (verbose) Console.WriteLine(String.Format("Sent: {0}", bytesReadEncoded));
                client.Close();

                return Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
            }

            /// <summary>
            /// Establish TCP Server Connection
            /// </summary>
            /// <param name="chat">Chat instance</param>
            /// <returns>Server state</returns>
            public void EstablishServerConnection()
            {
                IPAddress[] IPs = Dns.GetHostAddresses(this.Host);
                this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.Socket.Connect(IPs[0], this.Port);
                this.SendData(String.Format("<y r=\"{0}\" m=\"1\" v=\"0\" u=\"{1}\" />", Chat.Id, Chat.Bot.Id), true);
            }
        }
    }
}
