using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace AID_DiscordBot.Core.UserAccounts
{
	public static class UserAccounts
	{
		private static List<UserAccount> accounts;

		static UserAccounts()
		{

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
