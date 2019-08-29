using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;

namespace AID_DiscordBot
{
	class CommandHandler
	{
		private DiscordSocketClient _client;
		private CommandService _service;

		public async Task InitializeTask(DiscordSocketClient client)
		{
			_client = client;
			_service = new CommandService();
			await _service.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
			_client.MessageReceived += HandleCommandAsync;
		}

		private async Task HandleCommandAsync(SocketMessage s)
		{
			SocketUserMessage msg = s as SocketUserMessage;
			if (msg == null)
			{
				return;
			}

			SocketCommandContext context = new SocketCommandContext(_client, msg);

			int argPos = 0;

			if (msg.HasStringPrefix(Config.bot.cmdprefix, ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
			{
				var result = await _service.ExecuteAsync(context, argPos, services: null);
				if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
				{
					Console.WriteLine(result.ErrorReason);
				}
			}
		}
	}
}
