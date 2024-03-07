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
using Exiled.Events.EventArgs.Server;
using PlayerRoles;
using Respawning;
using UnityEngine;
using Random = System.Random;

namespace Scp273
{
    public class Plugin:Plugin<Config>
    {
        public override string Author { get; } = "Friza";

        public override string Name { get; } = "SCP-273";

        public override string Prefix { get; } = "SCP-273";

        public override Version Version { get; } = new Version(1, 0, 1);

        public override Version RequiredExiledVersion { get; } = new Version(8, 8, 0);
        
        public Random random = new Random();
        public Plugin plugin;
        public string SCP273ID = "";
        public override void OnEnabled()
        {
            plugin = this;
            Exiled.Events.Handlers.Server.RoundStarted += this.RoundStarted;
            Exiled.Events.Handlers.Player.Died += this.OnDied;
            Exiled.Events.Handlers.Player.ChangingRole += this.ChangingRole;
            Exiled.Events.Handlers.Player.Escaping += this.OnEscaping;
            Log.Info("");
            base.OnEnabled();
        }
        public void RoundStarted()
        {
              
              int rand = random.Next(1, 100);

              if (rand < Config.spawnChance)
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
        }

        public void ChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player.Role.Type == RoleTypeId.NtfCaptain)
            {
                ev.Player.CustomInfo = null;
            }
            if (ev.Player.Role.Type == RoleTypeId.NtfSergeant)
            {
                ev.Player.CustomInfo = null;
            }
            if (ev.Player.Role.Type == RoleTypeId.NtfPrivate)
            {
                ev.Player.CustomInfo = null;
            }
            if (ev.Player.Role.Type == RoleTypeId.ChaosMarauder)
            {
                ev.Player.CustomInfo = null;
            }
            if (ev.Player.Role.Type == RoleTypeId.ChaosRepressor)
            {
                ev.Player.CustomInfo = null;
            }
            if (ev.Player.Role.Type == RoleTypeId.ChaosRifleman)
            {
                ev.Player.CustomInfo = null;
            }
            if (ev.Player.Role.Type == RoleTypeId.ChaosConscript)
            {
                ev.Player.CustomInfo = null;
            } 
        }

        public void OnEscaping(EscapingEventArgs ev)
        {
            if (ev.Player.CustomInfo == "Человек-Феникс.")
            {
                ev.Player.CustomInfo = null;
            }
        }
        
        public void OnDied(DiedEventArgs ev)
        {
            if (ev.Player.CustomInfo == "Человек-Феникс.")
            {
                Timing.CallDelayed(20f, delegate()
                {
                    ev.Player.Role.Set(RoleTypeId.ClassD, RoleSpawnFlags.AssignInventory);
                    ev.Player.CustomInfo = "Человек-Феникс. Жизнь 2";
                    ev.Player.RankColor = "orange";
                    ev.Player.RankName = "Scp-273";
                    ev.Player.MaxHealth = 170;
                    ev.Player.Health = 170;
                    ev.Player.ShowHint(Config.Hint2, 5f);
                    ev.Player.Broadcast(7, "У вас осталось 3 жизни");
                });
            }
            if (ev.Player.CustomInfo == "Человек-Феникс. Жизнь 2")
            {
                Timing.CallDelayed(20f, delegate()
                {
                    ev.Player.Role.Set(RoleTypeId.ClassD, RoleSpawnFlags.AssignInventory);
                    ev.Player.CustomInfo = "Человек-Феникс. Жизнь 3";
                    ev.Player.RankColor = "orange";
                    ev.Player.RankName = "Scp-273";
                    ev.Player.MaxHealth = 170;
                    ev.Player.Health = 170;
                    ev.Player.ShowHint(Config.Hint2, 5f);
                    ev.Player.Broadcast(7, "У вас осталось 2 жизни");
                });
            }
            if (ev.Player.CustomInfo == "Человек-Феникс. Жизнь 3")
            {
                Timing.CallDelayed(20f, delegate()
                {
                    ev.Player.Role.Set(RoleTypeId.ClassD, RoleSpawnFlags.AssignInventory);
                    ev.Player.CustomInfo = "Человек-Феникс. Жизнь 4";
                    ev.Player.RankColor = "orange";
                    ev.Player.RankName = "Scp-273";
                    ev.Player.MaxHealth = 170;
                    ev.Player.Health = 170;
                    ev.Player.ShowHint(Config.Hint2, 5f);
                    ev.Player.Broadcast(7, "У вас осталась 1 жизнь");
                });
            }
            if (ev.Player.CustomInfo == "Человек-Феникс. Жизнь 4")
            {
                ev.Player.CustomInfo = null;
                ev.Player.Broadcast(7, "Вы мертвы!");
            }
        }
    }
}
