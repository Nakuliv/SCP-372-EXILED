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
        int SCP372 = 0;
        public System.Random rnd = new System.Random();
        public HashSet<string> scp372 = new HashSet<string>();
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.NewRole == RoleType.ClassD && SCP372 < 1 && !scp372.Contains($"{ev.Player.UserId}"))
            {
                if (new Random().Next(0, 101) <= Plugin.Instance.Config.SpawnChance)
                {
                    Add372(ev.Player);
                }
            }
            if (scp372.Contains(ev.Player.UserId))
            {
                this.Remove372(ev.Player);
            }

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
            if (SCP372 < 1)
            {
                Timing.CallDelayed(0.1f, () =>
                {
                    p.IsInvisible = true;
                    p.SetRank("", "default");
                });
                p.Broadcast(Plugin.Instance.Config.SpawnMessageDuration, Plugin.Instance.Config.SpawnMessage, 0);

                Timing.CallDelayed(0.5f, () =>
                {
                    p.Position = RandomItemSpawner.singleton.posIds.First(x => x.posID == "RandomPistol" && x.DoorTriggerName == "372").position.position;
                    p.RankName = "SCP-372";
                    p.RankColor = "red";
                    Scp096.TurnedPlayers.Add(p);
                    Scp173.TurnedPlayers.Add(p);
                    scp372.Add($"{p.UserId}");
                    SCP372++;
                });
            }
        }

        public void OnPlayerHurt(HurtingEventArgs ev)
        {

            if (scp372.Contains(ev.Attacker.UserId) && ev.Target.Team == Team.SCP || scp372.Contains(ev.Target.UserId) && ev.Attacker.Team == Team.SCP)
            {
                if (ev.DamageType == DamageTypes.Scp106)
                {
                    Timing.CallDelayed(1f, () =>
                    {
                        ev.Target.Position = ev.Attacker.Position;
                    });
                    ev.Amount = 0f;
                }
                ev.Amount = 0f;
            }
        }

            public void Remove372(Player p)
        {
            if (scp372.Contains($"{p.UserId}"))
            {
                p.IsInvisible = false;
                p.RefreshTag();
                Timing.CallDelayed(0.1f, () =>
                {
                    scp372.Remove($"{p.UserId}");
                    SCP372--;
                });
                Scp096.TurnedPlayers.Remove(p);
                Scp173.TurnedPlayers.Remove(p);
            }
            p.CustomInfo = null;
        }
    }
}
