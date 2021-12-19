﻿using Discord;
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
	public class Bambsonda : Command
	{
		public static char[] Constants = { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z' };

		public static char[] Vowels = { 'A', 'E', 'I', 'O', 'U' };

		public static string[] Colors = { "Red", "Blue", "Green", "Yellow", "Black", "White", "Orange", "Purple", "Gray", "Rainbow" };


		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{

			Random rng = new Random();

			string funny = "```\n";

			funny += Constants[rng.Next(0, Constants.Length - 1)];

			funny += Vowels[rng.Next(0, Vowels.Length - 1)];

			funny += "MB";


			if (rng.Next(0, 2) == 0)
			{
				funny += "I";
			}
			else
			{
				funny += Vowels[rng.Next(0, Vowels.Length - 1)];
			}


			funny += "\nHat:" + Colors[rng.Next(0, Colors.Length - 1)] + "\nShirt:" + Colors[rng.Next(0, Colors.Length - 1)] + "\nPants:" + Colors[rng.Next(0, Colors.Length - 1)];


			await userMessage.Channel.SendMessageAsync(funny + "\n```");
			return Task.CompletedTask;
		}
	}
}
