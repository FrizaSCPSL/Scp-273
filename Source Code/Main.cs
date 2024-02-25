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
}
