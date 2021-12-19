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
	public class OpinionCommand : Command
	{


		public static string[] Opinions = { "I block you.", "You troll, I block.", "Click to friend me!!", "What is wrong with you??", "You naked sex. Why?", "Disgusting. Please don't talk to them at home."};
		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{


			Random rng = new Random((int)userMessage.Author.Id);




			await userMessage.Channel.SendMessageAsync("<@!" + userMessage.Author.Id + "> " + Opinions[rng.Next(0, Opinions.Length)]);
			return Task.CompletedTask;
		}
	}
}
