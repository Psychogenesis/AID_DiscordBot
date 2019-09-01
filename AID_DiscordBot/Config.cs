using Newtonsoft.Json;
using System.IO;

namespace AID_DiscordBot
{
	class Config
	{
		private const string Configfolder = "resources";
		private const string Configfile = "config.json";

		public static BotConfig bot;

		static Config()
        {
            string json;

			if (!Directory.Exists(Configfolder))
			{
				Directory.CreateDirectory(Configfolder);
			}

			if (!File.Exists($"{Configfolder}/{Configfile}"))
			{
				bot = new BotConfig();
				json = JsonConvert.SerializeObject(bot, Formatting.Indented);
				File.WriteAllText($"{Configfolder}/{Configfile}", json);
			}
			else
			{
				json = File.ReadAllText($"{Configfolder}/{Configfile}");
				bot = JsonConvert.DeserializeObject<BotConfig>(json);
			}
		}
	}

	public struct BotConfig
	{
		public string token;
		public string cmdprefix;
	}
}
