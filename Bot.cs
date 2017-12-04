using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botter
{
    /// <summary>
    /// Store Bot Information.
    /// </summary>
    class Bot
    {
        public int Id { get; protected set; }
        public string Regname { get; protected set; }
        public string Password { get; protected set; }
        public BotAuthenticationKeys AuthenticationKeys { get; protected set; }
        public string DisplayName { get; protected set; }
        public string AvatarUrl { get; protected set; }
        public string HomepageUrl { get; protected set; }
        public int OwnerId { get; protected set; }

        /// <summary>
        /// Bot class constructor.
        /// Generate a new ID.
        /// </summary>
        public Bot()
        {

        }

        /// <summary>
        /// Bot class constructor.
        /// </summary>
        /// <param name="botId">Bot Account ID</param>
        public Bot(int botId, string botAuthenticationKey1, string botAuthenticationKey2)
        {
            this.Id = botId;
        }

        /// <summary>
        /// Bot class constructor.
        /// </summary>
        /// <param name="botId">Bot Account ID</param>
        /// <param name="botRegname">Bot Account Regname</param>
        /// <param name="botPassword">Bot Account Password</param>
        /// <param name="botAuthenticationKey1">Bot Account Authentication Key 1</param>
        /// <param name="botAuthenticationKey2">Bot Account Authentication Key 2</param>
        public Bot(int botId, string botAuthenticationKey1, string botAuthenticationKey2, string botRegname, string botPassword)
        {
            this.Id = botId;
            this.Regname = botRegname;
            this.Password = botPassword;
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
        /// <param name="botDisplayName">Bot Account Display Name</param>
        /// <param name="botAvatarUrl">Bot Account Full Avatar URL (image only)</param>
        /// <param name="botHomepageUrl">Bot Account Full Homepage URL</param>
        /// <param name="botOwnerId">Bot Owner Account ID</param>
        public Bot(int botId, string botAuthenticationKey1, string botAuthenticationKey2, string botRegname, string botPassword, string botDisplayName, string botAvatarUrl, string botHomepageUrl, int botOwnerId)
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
        }

        /// <summary>
        /// Store Bot Account Authentication Keys
        /// </summary>
        public abstract class BotAuthenticationKeys
        {
            public string Key1 { get; set; }
            public string Key2 { get; set; }
        }
    }
}
