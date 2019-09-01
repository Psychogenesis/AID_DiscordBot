using System.Threading.Tasks;
using Discord.Commands;

namespace AID_DiscordBot.Modules.Commands
{
    public partial class Commands
    {
        [Command("echo_weather")]
        public async Task TestWeather()
        {
            await Context.Channel.SendMessageAsync("This is a test for Weather API partial.");
        }
    }
}
