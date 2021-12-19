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
	public class RealWiki : Command
	{
		private StringMarkov Level1Markov;

		private StringMarkov Level2Markov;


		public override async Task<Task> Init()
		{
			string marcellokov = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Config", "wikidata.txt"));

			Level1Markov = new StringMarkov(1);

			Level2Markov = new StringMarkov(2);

			Level1Markov.Learn(marcellokov);

			Level2Markov.Learn(marcellokov);

			Level1Markov.EnsureUniqueWalk = true;

			Level2Markov.EnsureUniqueWalk = true;


			return base.Init();
		}

		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{


			Random rng = new Random();


			string current = "";

			bool smart = false;

			if (Params.Count != 0)
			{
				if (Params[0].String == "dumb")
				{
					smart = true;
				}
				
			}



			if (!smart)
			{
				current = Level2Markov.Walk(1).RandomElementUsing(rng).Split('\n').RandomElementUsing(rng);
				current = Level2Markov.Walk(1,current + "\n").RandomElementUsing(rng).Split('\n').RandomElementUsing(rng);
			}
			else
			{
				current = Level1Markov.Walk(1).RandomElementUsing(rng).Split('\n').RandomElementUsing(rng);
				current = Level1Markov.Walk(1, current + "\n").RandomElementUsing(rng).Split('\n').RandomElementUsing(rng);
			}



			await userMessage.Channel.SendMessageAsync("```\n" + current + "```");
			return Task.CompletedTask;
		}
	}
}
