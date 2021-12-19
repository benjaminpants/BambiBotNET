using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using MarkovSharp;
using MarkovSharp.TokenisationStrategies;

namespace BambiBotNET
{
	public class EmailAddress : Command
	{

		public static string[] EmailAdd = { "iscool", "loveskids", "homosex", "isawesomecringe", "loveskanye", "bruh", "2008", "basics", "lovesreddit333", "wasnottheimpostor" };

		public static string[] Websites = { "gmail.com", "pornhub.com", "baldisbasics.net", "yahoo.com", "areyoucordingmygame.net", "awesomesauce.com", "sugoma.gov", "aol.com", "gay.net" };

		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{

			Random rng = new Random();

			SocketUser user = userMessage.Author;

			if (Params.Count != 0)
			{
				if (Params[0].Type == ParamaterType.User)
				{
					if (Params[0].User != null)
					{
						user = Params[0].User;
					}
				}
			}

			string username = user.Username;

			username.Replace(' ', '\0');



			await userMessage.Channel.SendMessageAsync("``" + username + EmailAdd[rng.Next(0,EmailAdd.Length - 1)] + "@" + Websites[rng.Next(0, Websites.Length - 1)] + "``");
			return Task.CompletedTask;
		}
	}
}
