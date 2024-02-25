using CommandSystem.Commands.RemoteAdmin.ServerEvent;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Interfaces;
using Exiled.Events;
using Exiled.Events.EventArgs.Player;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerRoles;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features.Roles;
using Exiled.Permissions.Extensions;
using MEC;
using RemoteAdmin;
using Exiled.CustomRoles;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using Respawning;

namespace Scp273
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        
        public bool Debug { get; set; } = false;
        public string Hint1 { get; set; } = "SCP-273 - человек-феникс. При смерти вы возраждаетесь спустя 20 секунд.";
        public string Hint2 { get; set; } = "Вы восстали из пепла.";
        
        public string Prefix { get; set; } = "Scp-273";
        
        public string Cinfo { get; set; } = "Человек-Феникс.";
        
    }
    public class Plugin:Plugin<Config>
    {
        public override string Author { get; } = "Friza";

        public override string Name { get; } = "SCP-273";

        public override string Prefix { get; } = "SCP-273";

        public override Version Version { get; } = new Version(1, 0, 0);

        public override Version RequiredExiledVersion { get; } = new Version(8, 8, 0);

        public Plugin plugin;
        public string SCP273ID = "";
        public override void OnEnabled()
        {
            plugin = this;
            Exiled.Events.Handlers.Server.RoundStarted += this.RoundStarted;
            Exiled.Events.Handlers.Player.Died += this.Died;
            Log.Info("");
            base.OnEnabled();
        }
        public void RoundStarted()
        {
            Timing.CallDelayed(2f, () =>
            {
                SCP273ID = Player.Get(PlayerRoles.RoleTypeId.ClassD).ToList().RandomItem().UserId;
            });
              Timing.CallDelayed(2f, () => {
                var player = Player.Get(SCP273ID);
                player.CustomInfo = "Человек-Феникс.";
                player.RankColor = "orange";
                player.RankName = "Scp-273";
                player.MaxHealth = 170;
                player.Health = 170;
                player.AddItem(ItemType.Painkillers);
                player.AddItem(ItemType.Coin);
                player.ShowHint(Config.Hint1, 5f);
                });
        }
        
        public void Died(DiedEventArgs ev)
        {
            if (ev.Player.CustomInfo == "Человек-Феникс.")
            {
                Timing.CallDelayed(20f, delegate()
                {
                    ev.Player.Role.Set(RoleTypeId.ClassD, RoleSpawnFlags.AssignInventory);
                    ev.Player.CustomInfo = "Человек-Феникс.";
                    ev.Player.RankColor = "orange";
                    ev.Player.RankName = "Scp-273";
                    ev.Player.MaxHealth = 170;
                    ev.Player.Health = 170;
                    ev.Player.ShowHint(Config.Hint2, 5f);
                });
            }
        }

    }
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
