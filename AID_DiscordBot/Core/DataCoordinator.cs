using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AID_DiscordBot.Core.UserAccounts;

namespace AID_DiscordBot.Core
{
	public static class DataCoordinator
	{
		//Save User Accounts
		public static void SaveUserAccounts(IEnumerable<UserAccount> accounts, string filePath)
		{
			string json = JsonConvert.SerializeObject(accounts, Formatting.Indented);
			//perform checks
			File.WriteAllText(filePath, json);
		}

		//Get User Accounts
		public static IEnumerable<UserAccount> LoadUserAccounts(string filePath)
		{
			if (File.Exists(filePath))
			{
				return null;
			}

			string json = File.ReadAllText(filePath);
			return JsonConvert.DeserializeObject<List<UserAccount>>(json);
		}

        public static bool SaveExists(string filePath)
        {
            return File.Exists(filePath);
        }

        //This is where to create DataBase
    }
}
