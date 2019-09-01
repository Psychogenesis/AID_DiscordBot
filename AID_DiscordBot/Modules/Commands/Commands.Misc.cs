using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace AID_DiscordBot.Modules.Commands
{
    public partial class Commands
    {
        [Command("debug info")]
        [Alias("di")]
        public async Task ShowDebugInfo()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("This is [Author]");
            embed.WithDescription("This is [Description]");
            embed.WithTitle("This is [Title]");
            embed.WithFooter("This is [Footer]");
            embed.WithCurrentTimestamp();

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}
