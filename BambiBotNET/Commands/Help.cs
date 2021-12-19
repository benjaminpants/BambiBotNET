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
	public class HelpCommand : Command
	{
		


		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{


			


			await userMessage.Channel.SendMessageAsync("```\nCommands\n\">markovcello\" - generates a marcello quote using a markov chain. Add \"smart\" to make it produce more intellgiable output at risk of copying quotes directly\n\">bambsona\" - Randomly generates a \"Bambisona\" with a description\n\">opinion\" - Get marcellos REAL opinion on you (not clickbait)```");
			return Task.CompletedTask;
		}
	}
}
