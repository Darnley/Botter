using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;

namespace Botter
{
    /// <summary>
    /// Store Bot Information.
    /// </summary>
    class Bot
    {
        #region Attributes
        public int Id { get; protected set; }
        public string Regname { get; protected set; }
        public string Password { get; protected set; }
        public BotAuthenticationKeys AuthenticationKeys = new BotAuthenticationKeys();
        public string DisplayName { get; protected set; } = "Botter Bot";
        public string AvatarUrl { get; protected set; } = "http://imgur.com/logo.png";
        public string HomepageUrl { get; protected set; } = "http://xat.com";
        public int OwnerId { get; protected set; } = 250481203;
        public Chat Chat { get; protected set; }
        #endregion

        #region Class Constructors
        /// <summary>
        /// Bot class constructor.
        /// Generate a new ID.
        /// </summary>
        /// <param name="chatName">Chat Name to connect</param>
        public Bot(string chatName)
        {
            WebClient webClient = new WebClient();
            Uri authUserUrl = new Uri(String.Format("https://xat.com/?{0}", webClient.DownloadString("https://xat.com/web_gear/chat/auser3.php")));
            var serverResponse = HttpUtility.ParseQueryString(authUserUrl.Query);

            this.Id = int.Parse(serverResponse.Get("UserId"));
            this.AuthenticationKeys.Key1 = serverResponse.Get("k1");
            this.AuthenticationKeys.Key2 = serverResponse.Get("k2");
            this.Chat = new Chat(chatName, this);
        }

        /// <summary>
        /// Bot class constructor.
        /// </summary>
        /// <param name="botId">Bot Account ID</param>
        /// <param name="chatName">Chat Name to connect</param>
        public Bot(string chatName, int botId, string botAuthenticationKey1, string botAuthenticationKey2)
        {
            this.Id = botId;
            this.Chat = new Chat(chatName, this);
            this.AuthenticationKeys.Key1 = botAuthenticationKey1;
            this.AuthenticationKeys.Key2 = botAuthenticationKey2;
        }

        /// <summary>
        /// Bot class constructor.
        /// </summary>
        /// <param name="botId">Bot Account ID</param>
        /// <param name="botRegname">Bot Account Regname</param>
        /// <param name="botPassword">Bot Account Password</param>
        /// <param name="botAuthenticationKey1">Bot Account Authentication Key 1</param>
        /// <param name="botAuthenticationKey2">Bot Account Authentication Key 2</param>
        /// <param name="chatName">Chat Name to connect</param>
        public Bot(string chatName, int botId, string botAuthenticationKey1, string botAuthenticationKey2, string botRegname, string botPassword)
        {
            this.Id = botId;
            this.Regname = botRegname;
            this.Password = botPassword;
            this.AuthenticationKeys.Key1 = botAuthenticationKey1;
            this.AuthenticationKeys.Key2 = botAuthenticationKey2;
            this.Chat = new Chat(chatName, this);
        }

        /// <summary>
        /// Bot class constructor.
        /// </summary>
        /// <param name="botId">Bot Account ID</param>
        /// <param name="botRegname">Bot Account Regname</param>
        /// <param name="botPassword">Bot Account Password</param>
        /// <param name="botAuthenticationKey1">Bot Account Authentication Key 1</param>
        /// <param name="botAuthenticationKey2">Bot Account Authentication Key 2</param>
        /// <param name="botDisplayName">Bot Account Display Name</param>
        /// <param name="botAvatarUrl">Bot Account Full Avatar URL (image only)</param>
        /// <param name="botHomepageUrl">Bot Account Full Homepage URL</param>
        /// <param name="botOwnerId">Bot Owner Account ID</param>
        /// <param name="chatName">Chat Name to connect</param>
        public Bot(string chatName, int botId, string botAuthenticationKey1, string botAuthenticationKey2, string botRegname, string botPassword, string botDisplayName, string botAvatarUrl, string botHomepageUrl, int botOwnerId)
        {
            this.Id = botId;
            this.Regname = botRegname;
            this.Password = botPassword;
            this.AuthenticationKeys.Key1 = botAuthenticationKey1;
            this.AuthenticationKeys.Key2 = botAuthenticationKey2;
            this.DisplayName = botDisplayName;
            this.AvatarUrl = botAvatarUrl;
            this.HomepageUrl = botHomepageUrl;
            this.OwnerId = botOwnerId;
            this.Chat = new Chat(chatName, this);
        }
        #endregion
    }
}
