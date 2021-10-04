using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using UnityEngine;

namespace SCP_372
{
    public class Handlers
    {
        public Handlers handlers;
        public static HashSet<string> scp372 = new HashSet<string>();
        public System.Random rnd = new System.Random();

        public static void RemoveDisplayInfo(PlayerInfoArea playerInfo, Player ply) => ply.ReferenceHub.nicknameSync.Network_playerInfoToShow &= ~playerInfo;

        public static void AddDisplayInfo(PlayerInfoArea playerInfo, Player ply) => ply.ReferenceHub.nicknameSync.Network_playerInfoToShow |= playerInfo;

        public void OnSpawningPlayers(SpawningEventArgs ev)
        {
            if (Player.List.Count() < 3)
            {
                if (ev.Player.Team == Team.SCP && scp372.Count < Plugin.Singleton.Config.Max_SCP372_Count && !scp372.Contains(ev.Player.UserId))
                {
                    if (rnd.Next(0, 101) <= Plugin.Singleton.Config.SpawnChance)
                    {
                        Add372(ev.Player);
                    }
                }
            }
            else
            {
                if (ev.RoleType == RoleType.ClassD && scp372.Count < Plugin.Singleton.Config.Max_SCP372_Count && !scp372.Contains(ev.Player.UserId))
                {
                    if (rnd.Next(0, 101) <= Plugin.Singleton.Config.SpawnChance)
                    {
                        Add372(ev.Player);
                    }
                }
            }
            if (scp372.Contains(ev.Player.UserId))
            {
                Remove372(ev.Player);
            }

        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (scp372.Contains(ev.Player.UserId) && ev.NewRole != RoleType.ClassD)
            {
                Remove372(ev.Player);
            }
        }

        public void OnRoundRestart()
        {
            scp372.Clear();
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

        public void OnThrowingItem(ThrowingGrenadeEventArgs ev)
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

        public void OnPickingUpItem(PickingUpItemEventArgs ev)
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

        public void OnDroppingItem(DroppingItemEventArgs ev)
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

        public void OnInteractingDoor(InteractingDoorEventArgs ev)
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

        public void OnInteractingElevator(InteractingElevatorEventArgs ev)
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

        public void OnInteractingLocker(InteractingLockerEventArgs ev)
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

        public static void Add372(Player p)
        {
            if (scp372.Count < Plugin.Singleton.Config.Max_SCP372_Count)
            {
                p.IsInvisible = true;
                p.Broadcast(Plugin.Singleton.Config.SpawnMessage, true);
                Timing.CallDelayed(0.5f, () =>
                {
                    RemoveDisplayInfo(PlayerInfoArea.Role, p);
                    p.GameObject.AddComponent<SCP372Component>();
                    p.Role = RoleType.Tutorial;
                    p.MaxHealth = Plugin.Singleton.Config.Health;
                    p.Health = Plugin.Singleton.Config.Health;
                    p.CustomInfo = $"<color=red>SCP-372</color>";
                    Scp096.TurnedPlayers.Add(p);
                    Scp173.TurnedPlayers.Add(p);
                    scp372.Add(p.UserId);
                });
                Timing.CallDelayed(1f, () =>
                {
                    foreach (Room rm in Map.Rooms)
                    {
                        p.Position = RandomItemSpawner.singleton.posIds.First(x => x.posID == "RandomPistol" && x.DoorTriggerName == "372").position.position;
                    }
                });
            }
        }
        public void OnEndingRound(EndingRoundEventArgs ev)
        {
            if (scp372.Count > 0)
            {
                if (Player.Get(Team.CDP).ToList().Count == 0 && Player.Get(Team.MTF).ToList().Count == 0 && Player.Get(Team.RSC).ToList().Count == 0)
                {
                    ev.LeadingTeam = LeadingTeam.Anomalies;
                    ev.IsRoundEnded = true;
                }
                else
                {
                    ev.IsAllowed = false;
                }
            }
        }
        public void OnPlayerHurt(HurtingEventArgs ev)
        {

            if (scp372.Contains(ev.Attacker.UserId) && ev.Target.Team == Team.SCP || scp372.Contains(ev.Target.UserId) && ev.Attacker.Team == Team.SCP)
            {
                ev.IsAllowed = false;
            }
        }

        public static void Remove372(Player p)
        {
            if (scp372.Contains(p.UserId))
            {
                AddDisplayInfo(PlayerInfoArea.Role, p);
                p.GameObject.GetComponent<SCP372Component>().Destroy();
                p.IsInvisible = false;
                Scp096.TurnedPlayers.Remove(p);
                Scp173.TurnedPlayers.Remove(p);
                scp372.Remove(p.UserId);
            }
            p.CustomInfo = null;
        }
    }
}
