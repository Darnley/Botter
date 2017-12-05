using System;
using System.Collections.Generic;
using System.IO;
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
            #region Attributes
            public string Host { get; set; }
            public int Port { get; set; }
            public Socket Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            private Bot Bot { get; set; }
            private PacketData Packet = new PacketData();
            #endregion

            #region Class Constructor
            public ChatServer(Bot _outer)
            {
                this.Bot = _outer;
            }
            #endregion

            private struct PacketData
            {
                public string I { get; set; }
                public string L5 { get; set; }
                public string P { get; set; }
            }

            /// <summary>
            /// Send text to server.
            /// </summary>
            /// <param name="data">Text to send</param>
            /// <param name="verbose">Want to write log in Console?</param>
            /// <returns>Returned string from server</returns>
            public string SendData(string data, bool verbose = false)
            {
                byte[] bytes = new byte[1024];
                data = String.Format("{0}\x00", data);

                // Send data to remote device
                if (verbose) Console.WriteLine("Sent: {0}", data);
                this.Socket.Send(System.Text.Encoding.ASCII.GetBytes(String.Format("<y r=\"{0}\" m=\"1\" v=\"0\" u=\"{1}\" />\x00", Bot.Chat.Id, Bot.Id)));

                // Receive the response from the remote device.  
                int bytesRec = Socket.Receive(bytes);
                if (verbose) Console.WriteLine("Received: {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

                return Encoding.ASCII.GetString(bytes, 0, bytesRec);
            }

            /// <summary>
            /// Establish TCP Server Connection
            /// </summary>
            /// <param name="chat">Chat instance</param>
            /// <returns>Server state</returns>
            public void Connect()
            {
                string response = String.Empty;

                IPAddress[] IPs = Dns.GetHostAddresses(this.Host);
                this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.Socket.Connect(IPs[0], this.Port);

                //response = this.SendData(String.Format("<y r=\"{0}\" m=\"1\" v=\"0\" u=\"{1}\" />", Bot.Chat.Id, Bot.Id), true);
                response = SendData(String.Format("<y r=\"{0}\" m=\"1\" v=\"0\" u=\"{1}\" />", Bot.Chat.Id, Bot.Id), true);

                this.Packet.I = GetBetween(response, "i=\"", "\"");
                this.Packet.P = GetBetween(response, "p=\"", "\"");
                this.Packet.L5 = CalculateL5Authentication();

                if(!string.IsNullOrEmpty(Bot.Regname))
                {
                    // Is registered
                } else
                {
                    // Isn't registered
                    response = SendData(String.Format("<j2 cb=\"0\" l5=\"{0}\" y=\"{1}\" k=\"{2}\" p=\"0\" c=\"{3}\" f=\"0\" u=\"{4}\" d0=\"0\" n=\"{5}\" a=\"{6}\" h=\"{7}\" v=\"10\" />", this.Packet.L5, this.Packet.I, this.Bot.AuthenticationKeys.Key1, this.Bot.Chat.Id, this.Bot.Id, this.Bot.DisplayName, this.Bot.AvatarUrl, this.Bot.HomepageUrl), true);
                }

                if(response.StartsWith("<logout"))
                {
                    throw new Exception("Invalid authentication.");
                }
            }

            /// <summary>
            /// Get string value between [first] startString and [last] endString.
            /// </summary>
            /// <param name="fullString">The full string</param>
            /// <param name="startString">The start string to search</param>
            /// <param name="endString">The end string to search</param>
            /// <returns>String between [startString] and [endString] in [fullString]</returns>
            private static string GetBetween(string fullString, string startString, string endString)
            {
                int posA = fullString.IndexOf(startString) + startString.Length;
                int posB = fullString.Substring(posA).IndexOf(endString) + posA;
                return fullString.Substring(posA, posB - posA).TrimEnd('\0');
            }

            /// <summary>
            /// Function that generates the L5.
            /// </summary>
            /// <returns>The L5 string</returns>
            private string CalculateL5Authentication()
            {
                string[] l5_info = this.Packet.P.Split('_');
                int p_w = int.Parse(l5_info[0]);
                int p_h = int.Parse(l5_info[1]);
                string p_octaves = l5_info[2];
                string p_seed = l5_info[3];

                int t = (int.Parse(this.Packet.I)%(p_w*p_h));
                int p_x = (int)(t%100);
                int p_y = (int)Math.Floor((double)t/p_w);
                string fileContent;

                // Check seed
                if(int.Parse(p_seed) == 100)
                {
                    fileContent = Properties.Resources.swf_values_100;
                } else
                {
                    fileContent = Properties.Resources.swf_values_101;
                }

                using (var reader = new StringReader(fileContent))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] value = line.Split(':');

                        if (String.Format("{0},{1}", p_x, p_y).Equals(value[0]))
                        {
                            return value[1];
                        }
                    }

                    return "0";
                }
            }
        }
    }
}
