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
            Player player = Player.Get(((CommandSender)sender).SenderId);
            
            
            if (!Permissions.CheckPermission((CommandSender)sender, "scp273.spawn"))
            {
                response = "У вас не достаточно прав!";
                return false;
            }
            int count = arguments.Count;
            if (count != 0)
            {
                if (count != 1)
                {
                    response = "Usage: spawn273";
                    return false;
                }
                if (player == null)
                {
                    response = $"Чек {Player.Get(player.Sender).Nickname} инвалид";
                    return false;
                }
                response = "Он уже scp-273";
                return true;
            }
            {
                if (count != 0)
                {
                    response = "Уже есть 273!";
                    return false;
                }

                player.Role.Set(RoleTypeId.ClassD);
                player.CustomInfo = "Человек-Феникс.";
                player.RankColor = "orange";
                player.RankName = "SCP-273";
                player.AddItem(ItemType.Painkillers);
                player.AddItem(ItemType.Coin);
                player.ShowHint("SCP-273 - человек-феникс. При смерти вы возраждаетесь спустя 20 секунд.", 5f);
                player.MaxHealth = 170;
                player.Health = 170;
                
                response = "Вы стали Scp273";
                return true;
            }
        }
    }
}