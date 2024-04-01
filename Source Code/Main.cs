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
using Item = PluginAPI.Core.Items.Item;
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
            Exiled.Events.Handlers.Player.Escaping += this.OnEscaping;
            Exiled.Events.Handlers.Player.TogglingNoClip += this.NoClip;
            Exiled.Events.Handlers.Player.FlippingCoin += this.OnFlippingCoin;
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
                      Cassie.Message("<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>Условия содержания SCP-273 нарушены... <size=0> . SCP 2 7 3 has been contained is error </size>");
                      Cassie.MessageTranslated(String.Empty,"<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>Условия содержания SСP-273 нарушены... <size=0> . SCP 2 7 3 has been contained is error</size>");
                  });
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
                    ev.Player.AddItem(ItemType.Painkillers);
                    ev.Player.AddItem(ItemType.Coin);
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
                    ev.Player.AddItem(ItemType.Painkillers);
                    ev.Player.AddItem(ItemType.Coin);
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
                    ev.Player.AddItem(ItemType.Painkillers);
                    ev.Player.AddItem(ItemType.Coin);
                    ev.Player.MaxHealth = 170;
                    ev.Player.Health = 170;
                    ev.Player.ShowHint(Config.Hint2, 5f);
                    ev.Player.Broadcast(7, "У вас осталась 1 жизнь");
                });
            }
            if (ev.Player.CustomInfo == "Человек-Феникс. Жизнь 4")
            {
                ev.Player.CustomInfo = "";     
                ev.Player.Broadcast(7, "Вы мертвы!");
                Cassie.Message("<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>Условия содержания SCP-273 восстановлены... <size=0> . SCP 2 7 3 has been recontained </size>");
                Cassie.MessageTranslated(String.Empty,"<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>Условия содержания SСP-273 восстановлены... <size=0> . SCP 2 7 3 has been recontained</size>");
                ev.Player.RankName = "";
                ev.Player.RankColor = "";
            }

            if (ev.Player.CustomInfo == "Человек-Феникс. Ульта 1.")
            {
                Timing.CallDelayed(20f, delegate()
                {
                    ev.Player.Role.Set(RoleTypeId.ClassD, RoleSpawnFlags.AssignInventory);
                    ev.Player.CustomInfo = "Человек-Феникс. Жизнь 2";
                    ev.Player.RankColor = "orange";
                    ev.Player.RankName = "Scp-273";
                    ev.Player.AddItem(ItemType.Painkillers);
                    ev.Player.AddItem(ItemType.Coin);
                    ev.Player.MaxHealth = 170;
                    ev.Player.Health = 170;
                    ev.Player.ShowHint(Config.Hint2, 5f);
                    ev.Player.Broadcast(7, "У вас осталось 3 жизни");
                });
            }
            if (ev.Player.CustomInfo == "Человек-Феникс. Ульта 2.")
            {
                Timing.CallDelayed(20f, delegate()
                {
                    ev.Player.Role.Set(RoleTypeId.ClassD, RoleSpawnFlags.AssignInventory);
                    ev.Player.CustomInfo = "Человек-Феникс. Жизнь 3";
                    ev.Player.RankColor = "orange";
                    ev.Player.RankName = "Scp-273";
                    ev.Player.AddItem(ItemType.Painkillers);
                    ev.Player.AddItem(ItemType.Coin);
                    ev.Player.MaxHealth = 170;
                    ev.Player.Health = 170;
                    ev.Player.ShowHint(Config.Hint2, 5f);
                    ev.Player.Broadcast(7, "У вас осталось 3 жизни");
                });
            }
            if (ev.Player.CustomInfo == "Человек-Феникс. Ульта 3.")
            {
                Timing.CallDelayed(20f, delegate()
                {
                    ev.Player.Role.Set(RoleTypeId.ClassD, RoleSpawnFlags.AssignInventory);
                    ev.Player.CustomInfo = "Человек-Феникс. Жизнь 4";
                    ev.Player.RankColor = "orange";
                    ev.Player.RankName = "Scp-273";
                    ev.Player.AddItem(ItemType.Painkillers);
                    ev.Player.AddItem(ItemType.Coin);
                    ev.Player.MaxHealth = 170;
                    ev.Player.Health = 170;
                    ev.Player.ShowHint(Config.Hint2, 5f);
                    ev.Player.Broadcast(7, "У вас осталось 3 жизни");
                });
            }
        }

        public void NoClip(TogglingNoClipEventArgs ev)
        {
            if (ev.Player.CustomInfo == "Человек-Феникс.")
            {
                ev.Player.EnableEffect(EffectType.MovementBoost, 30);
                ev.Player.IsGodModeEnabled = true;
                ev.Player.Broadcast(7, "Вы активировали <color=#orange>ульту!</color>");
                ev.Player.CustomInfo = "Человек-Феникс. Ульта 1.";
                Timing.CallDelayed(15f, delegate()
                {
                    ev.Player.Kill("Вы сожгли себя!");
                    ev.Player.IsGodModeEnabled = false;
                    ev.Player.DisableEffect(EffectType.MovementBoost);
                });
            }
            if (ev.Player.CustomInfo == "Человек-Феникс. Жизнь 2")
            {
                ev.Player.EnableEffect(EffectType.MovementBoost, 30);
                ev.Player.IsGodModeEnabled = true;
                ev.Player.Broadcast(7, "Вы активировали <color=#orange>ульту!</color>");
                ev.Player.CustomInfo = "Человек-Феникс. Ульта 2.";
                Timing.CallDelayed(15f, delegate()
                {
                    ev.Player.Kill("Вы сожгли себя!");
                    ev.Player.IsGodModeEnabled = false;
                    ev.Player.DisableEffect(EffectType.MovementBoost);
                });
            }
            if (ev.Player.CustomInfo == "Человек-Феникс. Жизнь 3")
            {
                ev.Player.EnableEffect(EffectType.MovementBoost, 30);
                ev.Player.CustomInfo = "Человек-Феникс. Ульта 3.";
                ev.Player.IsGodModeEnabled = true;
                ev.Player.Broadcast(7, "Вы активировали <color=#orange>ульту!</color>");
                Timing.CallDelayed(15f, delegate()
                {
                    ev.Player.Kill("Вы сожгли себя!");
                    ev.Player.IsGodModeEnabled = false;
                    ev.Player.DisableEffect(EffectType.MovementBoost);
                });
            }
            if (ev.Player.CustomInfo == "Человек-Феникс. Жизнь 4")
            {
                ev.Player.Broadcast(7, "Это ваша последняя жизнь <color=#orange>ульта</color>не работает!");
            }
        }

        public void OnFlippingCoin(FlippingCoinEventArgs ev)
        {
            if (ev.Player.CustomInfo == "Человек-Феникс.")
            {
                Map.TurnOffAllLights(10, ZoneType.HeavyContainment);
                Map.TurnOffAllLights(10, ZoneType.LightContainment);
                Map.TurnOffAllLights(10, ZoneType.Surface);
                Map.TurnOffAllLights(10, ZoneType.Entrance);
                Cassie.Message("<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>SCP-273 сжег систему света ожидайте 10 секунд... <size=0> . SCP 2 7 3 burn system lights waiting 10 seconds</size>");
                Cassie.MessageTranslated(String.Empty,"<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>SCP-273 сжег систему света ожидайте 10 секунд... <size=0> . SCP 2 7 3 burn system lights waiting 10 seconds</size>");
                ev.Item.Destroy();
                
            }
            if (ev.Player.CustomInfo == "Человек-Феникс. Жизнь 2")
            {
                Map.TurnOffAllLights(13, ZoneType.HeavyContainment);
                Map.TurnOffAllLights(13, ZoneType.LightContainment);
                Map.TurnOffAllLights(13, ZoneType.Surface);
                Map.TurnOffAllLights(13, ZoneType.Entrance);
                Cassie.Message("<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>SCP-273 сжег систему света ожидайте 13 секунд... <size=0> . SCP 2 7 3 burn system lights waiting 13 seconds</size>");
                Cassie.MessageTranslated(String.Empty,"<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>SCP-273 сжег систему света ожидайте 13 секунд... <size=0> . SCP 2 7 3 burn system lights waiting 10 seconds</size>");
                ev.Item.Destroy();
            }
            if (ev.Player.CustomInfo == "Человек-Феникс. Жизнь 3")
            {
                Map.TurnOffAllLights(16, ZoneType.HeavyContainment);
                Map.TurnOffAllLights(16, ZoneType.LightContainment);
                Map.TurnOffAllLights(16, ZoneType.Surface);
                Map.TurnOffAllLights(16, ZoneType.Entrance);
                Cassie.Message("<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>SCP-273 сжег систему света ожидайте 16 секунд... <size=0> . SCP 2 7 3 burn system lights waiting 16 seconds</size>");
                Cassie.MessageTranslated(String.Empty,"<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>SCP-273 сжег систему света ожидайте 16 секунд... <size=0> . SCP 2 7 3 burn system lights waiting 10 seconds</size>");
                ev.Item.Destroy();
            }
            if (ev.Player.CustomInfo == "Человек-Феникс. Жизнь 4")
            {
                Map.TurnOffAllLights(19, ZoneType.HeavyContainment);
                Map.TurnOffAllLights(19, ZoneType.LightContainment);
                Map.TurnOffAllLights(19, ZoneType.Surface);
                Map.TurnOffAllLights(19, ZoneType.Entrance);
                Cassie.Message("<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>SCP-273 сжег систему света ожидайте 19 секунд... <size=0> . SCP 2 7 3 burn system lights waiting 19 seconds</size>");
                Cassie.MessageTranslated(String.Empty,"<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>SCP-273 сжег систему света ожидайте 19 секунд... <size=0> . SCP 2 7 3 burn system lights waiting 10 seconds</size>");
                ev.Item.Destroy();
            }
        }
    }
}
