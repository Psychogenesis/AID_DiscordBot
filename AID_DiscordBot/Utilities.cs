using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace AID_DiscordBot
{
	class Utilities
	{
		private static Dictionary<string,string> alerts;

		private const string ALERTPATH = "SystemLang/alerts.json";
		static Utilities()
		{
			string json = File.ReadAllText(ALERTPATH);
			dynamic data = JsonConvert.DeserializeObject<dynamic>(json);
			alerts = data.ToObject<Dictionary<string, string>>();
		}

		public static string GetAlert(string key)
		{
			return (alerts.ContainsKey(key)) ? alerts[key] : $"key {key} is not recognized.";
		}
	}
}
