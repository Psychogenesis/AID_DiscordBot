using System.Threading.Tasks;
using Discord.Commands;

namespace AID_DiscordBot.Modules.Commands
{
    public partial class Commands
    {
        [Command("echo_game")]
        public async Task TestGame()
        {
            await Context.Channel.SendMessageAsync("This is a test for game partial.");
        }
    }
}
