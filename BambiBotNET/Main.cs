using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BambiBotNET
{
	public class Program
	{
		public static Program Instance;

		public Program()
		{
			Program.Instance = this;
		}

		public async Task<Task> SendMessageWithFilters(ISocketMessageChannel channel, string str)
		{
			if (str.Contains("@everyone") || str.Contains("@here"))
			{
				return channel.SendMessageAsync("Message contained a ping.");
			}
			return channel.SendMessageAsync(str);
		}



		public static Task Main(string[] args) => new Program().MainAsync();

		public static Dictionary<string, Command> Commands = new Dictionary<string, Command>();



		private DiscordSocketClient _client;

		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}



		private async Task<Task> MessageSent(SocketMessage message)
		{
			if (!(message is SocketUserMessage userMessage)) return Task.CompletedTask;
			if (userMessage.Source != MessageSource.User) return Task.CompletedTask;
			
			if (userMessage.Channel.Name != "bambi-bot" && userMessage.Author.Id != 356529303573364737)
			{
				return Task.CompletedTask;
			}


			if (userMessage.Content.StartsWith(">"))
			{
				string[] command = userMessage.Content.Substring(1).Split(" ");

				Console.WriteLine(userMessage.Content);

				List<Parameter> Params = new List<Parameter>();

				for (int i = 1; i < command.Length; i++)
				{
					if (int.TryParse(command[i], out int integer))
					{
						Params.Add(new Parameter(integer));
					}
					else if (float.TryParse(command[i], out float floating_point))
					{
						Params.Add(new Parameter(floating_point));
					}
					else if (command[i].StartsWith("<@!"))//<@!921989230983536700>
					{
						if (ulong.TryParse(command[i].Substring(3, command[i].Length - 4), out ulong id))
						{
							SocketUser user = _client.GetUser(id);
							Params.Add(new Parameter(user));
						}
						else
						{
							Params.Add(new Parameter());
						}
					}
					else
					{
						Params.Add(new Parameter(command[i]));
					}
				}
				if (Commands.TryGetValue(command[0],out Command cmd))
				{
					return cmd.Run(Params,userMessage);
				}
			}
			return Task.CompletedTask;
		}

		public async Task MainAsync()
		{
			_client = new DiscordSocketClient();

			_client.Log += Log;

			_client.MessageReceived += MessageSent;

			if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "Config", "token.txt")))
			{
				Console.WriteLine("token.txt not found, this is probably your first time booting the bot up.");
				return;
			}

			var token = File.ReadAllText(Path.Combine(Environment.CurrentDirectory,"Config","token.txt"));


			//THE BELOW CODE IS A FUCKING HELLHOLE AND WILL BE REPLACED SOON

			Command Mark = new MarkovCello();
			await Mark.Init();

			Command Mark2 = new RealWiki();
			await Mark2.Init();

			Command Mark3 = new DialogueMarkov();
			await Mark3.Init();

			Commands.Add("markovcello", Mark);

			Commands.Add("wikireal", Mark2);

			Commands.Add("dialogueleak", Mark3);

			Commands.Add("help", new HelpCommand());

			Commands.Add("bambsona", new Bambsona());

			Commands.Add("email", new EmailAddress());

			Commands.Add("opinion", new OpinionCommand());

			_client.SetGameAsync("Your mom.",null,ActivityType.Listening);

			await _client.LoginAsync(TokenType.Bot, token);
			await _client.StartAsync();

			await Task.Delay(-1);


		}
	}
}
