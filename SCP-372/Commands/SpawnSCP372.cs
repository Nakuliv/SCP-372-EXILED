using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using MEC;
using SCP_372;
using RemoteAdmin;
namespace SCP_372.Commands
{
	[CommandHandler(typeof(RemoteAdminCommandHandler))]
	internal class SpawnSCP343 : ICommand
	{

		public string Command { get; } = "spawnscp372";

		public string[] Aliases { get; } = new string[]
		{
			"spawn372",
			"372",
			"scp372"
		};

		public string Description { get; } = "Spawn SCP-372";

		public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
		{
			Player player = Player.Get(arguments.At(0));
			if (!Permissions.CheckPermission((CommandSender)sender, "scp372.spawn"))
			{
				response = "You do not have permissions to run this command!";
				return false;
			}

			if (arguments.Count != 1)
            {
				response = "Usage: spawn372 (player id / name)";
				return false;
			}

			if (player == null)
			{
				response = $"Player not found: {arguments.At(0)}";
				return false;
			}

			Handlers.Add372(player);
			response = $"Spawned {player.Nickname} as SCP-372!";
			return true;
		}
	}
}