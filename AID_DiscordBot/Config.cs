using Newtonsoft.Json;
using System.IO;

namespace AID_DiscordBot
{
	class Config
	{
		private const string CONFIGFOLDER = "resources";
		private const string CONFIGFILE = "config.json";

		public static BotConfig bot;

		static Config()
		{
			string json = string.Empty;

			if (!Directory.Exists(CONFIGFOLDER))
			{
				Directory.CreateDirectory(CONFIGFOLDER);
			}

			if (!File.Exists($"{CONFIGFOLDER}/{CONFIGFILE}"))
			{
				bot = new BotConfig();
				json = JsonConvert.SerializeObject(bot, Formatting.Indented);
				File.WriteAllText($"{CONFIGFOLDER}/{CONFIGFILE}", json);
			}
			else
			{
				json = File.ReadAllText($"{CONFIGFOLDER}/{CONFIGFILE}");
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
