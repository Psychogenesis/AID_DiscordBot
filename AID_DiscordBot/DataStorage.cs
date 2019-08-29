using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace AID_DiscordBot
{
	internal class DataStorage
	{
		private static readonly Dictionary<string, string> Pairs = new Dictionary<string, string>();
		private const string DATASTORAGEPATH = "SystemLang/DataStorage.json";

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
			if(!ValidateStorageFile(DATASTORAGEPATH))
			{
				return;
			}
			string json = File.ReadAllText(DATASTORAGEPATH);
			Pairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
		}

		public static void SaveData()
		{
			string json = JsonConvert.SerializeObject(Pairs, Formatting.Indented);
			File.WriteAllText(DATASTORAGEPATH, json);
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
