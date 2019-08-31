using System;
using System.Collections.Generic;
using System.Linq;
using Discord.WebSocket;

namespace AID_DiscordBot.Core.UserAccounts
{
	public static class UserAccounts
	{
		private static readonly List<UserAccount> accounts;

        private static string accountsFile = "resources/accounts.json";

        static UserAccounts()
		{
            if (DataCoordinator.SaveExists(accountsFile))
            {
                accounts = DataCoordinator.LoadUserAccounts(accountsFile).ToList();
            }
            else
            {
                accounts = new List<UserAccount>();
                SaveAccounts();
            }
        }

        public static void SaveAccounts()
        {
            DataCoordinator.SaveUserAccounts(accounts, accountsFile);
        }

        public static UserAccount GetAccount(SocketUser user)
		{
			return GetOrCreateAccount(user.Id);
		}

		private static UserAccount GetOrCreateAccount(ulong id)
		{
			IEnumerable<UserAccount> result = from a in accounts where a.ID == id select a;
			UserAccount account = result.FirstOrDefault() ?? CreateAccount(id);

			return account;
		}

		private static UserAccount CreateAccount(ulong id)
		{
			var newAccont = new UserAccount()
			{
				ID = id,
				Points = 0,
				XP = 0
			};
			accounts.Add(newAccont);
			return newAccont;
		}
	}
}
