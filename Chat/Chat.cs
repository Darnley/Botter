﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Botter
{
    partial class Chat
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
        public ChatServer Server { get; private set; } = new ChatServer();

        /// <summary>
        /// Chat Constructor.
        /// </summary>
        /// <param name="chatName">Chat Name to connect</param>
        public Chat(string chatName)
        {
            JToken serverResponse = JObject.Parse(new WebClient().DownloadString(String.Format("https://api.mundosmilies.com/chatinfo.php?chat={0}", chatName)));

            if (((string)serverResponse.SelectToken("status")).Equals("OK"))
            {
                this.Id = (int)serverResponse.SelectToken("chatid");
                this.Name = (string)serverResponse.SelectToken("name");
                this.Description = (string)serverResponse.SelectToken("desc");
                this.Type = (string)serverResponse.SelectToken("chatType");
                this.Server.Host = (string)serverResponse.SelectToken("connIP");
                this.Server.Port = (int)serverResponse.SelectToken("connPort");
            }
            else
                throw new Exception(String.Format("Invalid chat. {0}", (string)serverResponse.SelectToken("message")));
        }
    }
}
