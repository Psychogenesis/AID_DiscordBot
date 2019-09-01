using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace AID_DiscordBot
{
	internal class Utilities
	{
		private static readonly Dictionary<string,string> Alerts;

		private const string Alertpath = "SystemLang/Alerts.json";
		static Utilities()
		{
			string json = File.ReadAllText(Alertpath);
			dynamic data = JsonConvert.DeserializeObject<dynamic>(json);
			Alerts = data.ToObject<Dictionary<string, string>>();
		}

		public static string GetAlert(string key)
		{
			return (Alerts.ContainsKey(key)) ? Alerts[key] : $"key {key} is not recognized.";
		}

		public static string GetFormattedAlert(string key, params object[] parameters)
		{
			return Alerts.ContainsKey(key) ? string.Format(Alerts[key], parameters) : "";
		}
	}
}