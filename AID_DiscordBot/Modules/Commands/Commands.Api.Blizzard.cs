using System.Threading.Tasks;
using Discord.Commands;

namespace AID_DiscordBot.Modules.Commands
{
    public partial class Commands
    {
        [Command("echo_blizzard")]
        public async Task TestBlizzard()
        {
            await Context.Channel.SendMessageAsync("This is a test for Blizzard API partial.");
        }
    }
}
