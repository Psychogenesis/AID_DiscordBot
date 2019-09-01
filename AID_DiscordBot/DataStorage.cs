using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace AID_DiscordBot
{
	internal static class DataStorage
	{
		private static readonly Dictionary<string, string> Pairs = new Dictionary<string, string>();
		private const string Datastoragepath = "SystemLang/DataStorage.json";

		public static void AddPairsToDictionary(string key, string value)
		{
			Pairs.Add(key, value);
			SaveData();
		}

		public static int GetPairCount()
		{
			return Pairs.Count;
		}

		static DataStorage()
		{
			if(!ValidateStorageFile(Datastoragepath))
			{
				return;
			}
			string json = File.ReadAllText(Datastoragepath);
			Pairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
		}

		private static void SaveData()
		{
			string json = JsonConvert.SerializeObject(Pairs, Formatting.Indented);
			File.WriteAllText(Datastoragepath, json);
		}

		private static bool ValidateStorageFile(string file)
		{
			if (File.Exists(file))
			{
				return true;
			}
			File.WriteAllText(file, "");
			SaveData();
			return false;

		}
	}
}
