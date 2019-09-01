using System.Collections.Generic;
using System.Linq;
using Discord.WebSocket;

namespace AID_DiscordBot.Core.UserAccounts
{
	public static class UserAccounts
	{
		private static readonly List<UserAccount> Accounts;

        private const string AccountsFile = "resources/accounts.json";

        static UserAccounts()
		{
            if (DataCoordinator.SaveExists(AccountsFile))
            {
                Accounts = DataCoordinator.LoadUserAccounts(AccountsFile).ToList();
            }
            else
            {
                Accounts = new List<UserAccount>();
                SaveAccounts();
            }
        }

        public static void SaveAccounts()
        {
            DataCoordinator.SaveUserAccounts(Accounts, AccountsFile);
        }

        public static UserAccount GetAccount(SocketUser user)
		{
			return GetOrCreateAccount(user.Id);
		}

		private static UserAccount GetOrCreateAccount(ulong id)
		{
			IEnumerable<UserAccount> result = from a in Accounts where a.ID == id select a;
			UserAccount account = result.FirstOrDefault() ?? CreateAccount(id);

			return account;
		}

		private static UserAccount CreateAccount(ulong id)
		{
			var newAccount = new UserAccount()
			{
				ID = id,
				Points = 0,
				XP = 0
			};
			Accounts.Add(newAccount);
			return newAccount;
		}
	}
}
