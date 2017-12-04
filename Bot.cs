using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botter
{
    class Bot
    {
        public int Id { get; protected set; }
        public string Regname { get; protected set; }
        public string Password { get; protected set; }
        public string DisplayName { get; protected set; }
        public string AvatarUrl { get; protected set; }
        public string HomepageUrl { get; protected set; }
        public int OwnerId { get; protected set; }

        public Bot(int botId, string botRegname = null, string botPassword = null)
        {
            
        }
    }
}
