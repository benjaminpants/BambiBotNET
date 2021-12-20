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
	public class MarkovCello : MarkovCommand
	{
		


		public override async Task<Task> Init()
		{
			FileToLook = "MarcelloQuotes.txt";

			return base.Init();
		}

		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			return base.Run(Params,userMessage);
		}
	}
}
