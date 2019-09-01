using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace AID_DiscordBot
{
	internal class Program
	{
		private DiscordSocketClient _client;
		private CommandHandler _handler;

		private static void Main() => new Program().StartAsync().GetAwaiter().GetResult();

		private async Task StartAsync()
		{
			if (string.IsNullOrEmpty(Config.bot.token))
			{
                Console.WriteLine("There are no Bot Config found.");
				return;
			}
			_client = new DiscordSocketClient(new DiscordSocketConfig
			{
				LogLevel = LogSeverity.Verbose
			});
			_client.Log += Log;
			
			await _client.LoginAsync(TokenType.Bot, Config.bot.token);
			await _client.StartAsync();

			_handler = new CommandHandler();
			await _handler.InitializeTask(_client);
			await Task.Delay(-1);
		}

		private async Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.Message);
			await Task.CompletedTask;
		}
	}
}
