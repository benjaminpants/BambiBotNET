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
	public class DialogueMarkov : Command
	{
		private StringMarkov Level1Markov;

		private StringMarkov Level2Markov;

		protected string FileToLook = "dialogue.txt";

		protected bool DefaultSmart = false;


		public override async Task<Task> Init()
		{
			string marcellokov = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Config", FileToLook));

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
				if ((Params[0].String == "smart" && !DefaultSmart) || (Params[0].String == "dumb" && DefaultSmart))
				{
					smart = true;
				}
				
			}



			if (!smart)
			{
				if (DefaultSmart)
				{
					current = Level2Markov.Walk(1).RandomElementUsing(rng);
				}
				else
				{
					current = Level1Markov.Walk(1).RandomElementUsing(rng);
				}
			}
			else
			{
				if (DefaultSmart)
				{
					current = Level1Markov.Walk(1).RandomElementUsing(rng);
				}
				else
				{
					current = Level2Markov.Walk(1).RandomElementUsing(rng);
				}
			}

			string[] splits = current.Split('\n');

			string final = "";

			for (int i = 0; i < Math.Clamp(splits.Length,1,8); i++)
			{
				if (splits[i] == "")
				{
					break;
				}
				final += splits[i];
			}



			await userMessage.Channel.SendMessageAsync("```\n" + final + "```");
			return Task.CompletedTask;
		}
	}
}
