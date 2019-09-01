using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AID_DiscordBot.Core.UserAccounts;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace AID_DiscordBot.Modules.Commands
{
    //TODO: Make class Commands internal\Private. Add layer for discord and make it Public.
    public partial class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("info")]
        [Alias("i")]
        public async Task ShowInformation()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("This is [Author]");
            embed.WithDescription("This is [Description]");
            embed.WithTitle("This is [Title]");
            embed.WithFooter("This is [Footer]");
            embed.WithCurrentTimestamp();

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("echo")]
        [Alias("e", "е", "эхо")]
        public async Task Echo([Remainder]string msg)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle($"Echoed by {Context.User.Username}");
            embed.WithDescription(msg);
            embed.WithColor(0, 255, 0);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("addXP")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AddXp(uint amount)
        {
            UserAccount account = UserAccounts.GetAccount(Context.User);
            account.XP += amount;
            UserAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync($"You gained {amount} XP points.");
        }

        [Command("level")]
        public async Task ShowXp()
        {
            UserAccount account = UserAccounts.GetAccount(Context.User);
            await Context.Channel.SendMessageAsync($"You have {account.XP} XP points and {account.Points} Points");
        }

        [Command("created")]
        public async Task DateCreated([Remainder]string arg = "")
        {
            SocketUser mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            SocketUser target = mentionedUser ?? Context.User;

            await Context.Channel.SendMessageAsync($"Аккаунт {target.Username} был создан {target.CreatedAt}");
        }

        [Command("pick")]
        public async Task Pick([Remainder]string msg)
        {
            string[] options = msg.Split(new [] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Random rnd = new Random();
            string selection = options[rnd.Next(0, options.Length)];

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle($"Picking for {Context.User.Username}");
            embed.WithDescription(selection);
            embed.WithColor(180, 0, 200);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("secret")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task DisplaySecret()
        {
            if (UserIsWorthy((SocketGuildUser)Context.User))
            {
                var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
                await dmChannel.SendMessageAsync(Utilities.GetFormattedAlert("SECRET"));
            }
        }

        [Command("GetData")]
        public async Task GetData()
        {
            await Context.Channel.SendMessageAsync(Utilities.GetFormattedAlert("DATA_$DATA", DataStorage.GetPairCount()));
            DataStorage.AddPairsToDictionary("Count " + DataStorage.GetPairCount(), "The Count " + DataStorage.GetPairCount());
        }

        private bool UserIsWorthy(SocketGuildUser user)
        {
            string targetRoleName = "";
            //TODO: move to a separate method. It should only get roles once during bot initialization and store it in some list.
            IEnumerable<ulong> result = from r in user.Guild.Roles where r.Name == targetRoleName select r.Id;
            ulong roleId = result.FirstOrDefault();
            if (roleId == 0)
            {
                return false;
            }

            var targetRole = user.Guild.GetRole(roleId);
            return user.Roles.Contains(targetRole);
        }
    }
}
