using System;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.Permissions.Extensions;
using MEC;
using Scp273;
using RemoteAdmin;
using Exiled.CustomRoles;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using Respawning;
using UnityEngine;

namespace Scp273.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SpawnSCP273 : ICommand
    {
        
       
        public string Command { get; } = "spawnscp273";

        public string[] Aliases { get; } = new string[]
        {
            "scp273",
            "spawn273"
        };

        public string Description { get; } = "Смена класса на Scp-273";
        
        
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            Player player = Player.Get(arguments.At(0));
            if (player == null)
            {
                response = $"Игрок не найден: {arguments.At(0)}";
                return false;
            }

            else
            {
                player.Role.Set(RoleTypeId.ClassD);
                player.CustomInfo = "Человек-Феникс.";
                player.RankColor = "orange";
                player.RankName = "SCP-273";
                player.AddItem(ItemType.Painkillers);
                player.AddItem(ItemType.Coin);
                player.ShowHint("SCP-273 - человек-феникс. При смерти вы возраждаетесь спустя 20 секунд.", 5f);
                player.MaxHealth = 170;
                player.Health = 170;
                Cassie.Message("<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>Условия содержания SCP-273 нарушены... <size=0> . SCP 2 7 3 has been contained is error </size>");
                Cassie.MessageTranslated(String.Empty,"<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>Условия содержания SСP-273 нарушены... <size=0> . SCP 2 7 3 has been contained is error</size>");
                response = "Done";
                return true;
            }

        }
    }
}