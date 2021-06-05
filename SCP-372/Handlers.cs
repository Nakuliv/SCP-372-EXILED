using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;

namespace SCP_372
{
    public class Handlers
    {
        public Handlers handlers;
        public HashSet<string> scp372 = new HashSet<string>();
        public int scps372 = 0;
        public Random rnd = new Random();
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.NewRole == RoleType.ClassD && scps372 < Plugin.Singleton.Config.Max_SCP372_Count && !scp372.Contains(ev.Player.UserId))
            {
                if (rnd.Next(0, 101) <= Plugin.Singleton.Config.SpawnChance)
                {
                    Add372(ev.Player);
                }
            }
            if (scp372.Contains(ev.Player.UserId))
            {
                Remove372(ev.Player);
            }

        }

        public void OnRoundRestart()
        {
            scps372 = 0;
        }

            public void OnShooting(ShootingEventArgs ev)
        {
            if (scp372.Contains(ev.Shooter.UserId))
            {
                ev.Shooter.IsInvisible = false;
                Timing.CallDelayed(0.5f, () =>
                {
                    ev.Shooter.IsInvisible = true;
                });
            }
        }

        public void OnEnterPocketDimension(EnteringPocketDimensionEventArgs ev)
        {
            if (scp372.Contains(ev.Player.UserId))
            {
                ev.IsAllowed = false;
            }
        }

        public void OnThrowingGrenade(ThrowingGrenadeEventArgs ev)
        {
            if (scp372.Contains(ev.Player.UserId))
            {
                ev.Player.IsInvisible = false;
                Timing.CallDelayed(1f, () =>
                {
                    ev.Player.IsInvisible = true;
                });
            }
        }
        public void onPlayerDied(DiedEventArgs ev)
        {
            if (scp372.Contains(ev.Target.UserId))
            {
                Remove372(ev.Target);
                ev.Target.IsInvisible = false;
            }
        }

            public void Add372(Player p)
        {
            if (scps372 < Plugin.Singleton.Config.Max_SCP372_Count)
            {
                p.IsInvisible = true;
                p.Broadcast(Plugin.Singleton.Config.SpawnMessage.Duration, Plugin.Singleton.Config.SpawnMessage.Content);
                Timing.CallDelayed(0.5f, () =>
                {
                    p.Role = RoleType.Tutorial;
                    p.MaxHealth = Plugin.Singleton.Config.Health;
                    p.Health = Plugin.Singleton.Config.Health;
                    p.CustomInfo = $"<color=red>SCP-372</color>";
                    Scp096.TurnedPlayers.Add(p);
                    Scp173.TurnedPlayers.Add(p);
                    scp372.Add(p.UserId);
                    scps372++;
                });
                Timing.CallDelayed(1f, () =>
                {
                    p.Position = RandomItemSpawner.singleton.posIds.First(x => x.posID == "RandomPistol" && x.DoorTriggerName == "372").position.position;
                });
                }
        }

        public void OnPlayerHurt(HurtingEventArgs ev)
        {

            if (scp372.Contains(ev.Attacker.UserId) && ev.Target.Team == Team.SCP || scp372.Contains(ev.Target.UserId) && ev.Attacker.Team == Team.SCP)
            {
                ev.IsAllowed = false;
            }
        }

            public void Remove372(Player p)
        {
            if (scp372.Contains(p.UserId))
            {
                p.IsInvisible = false;
                Scp096.TurnedPlayers.Remove(p);
                Scp173.TurnedPlayers.Remove(p);
                scp372.Remove(p.UserId);
                scps372--;
            }
            p.CustomInfo = null;
        }
    }
}
