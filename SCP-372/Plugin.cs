﻿using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Loader;
using System.Reflection;

using PlayerEv = Exiled.Events.Handlers.Player;
using ServerEv = Exiled.Events.Handlers.Server;
using System.IO;

namespace SCP_372
{
    public class Plugin : Plugin<Config>
    {

        public override string Name { get; } = "SCP-372";
        public override string Author { get; } = "Naku";
        public override Version Version => new Version(1, 4, 2);
        public override Version RequiredExiledVersion => new Version(3, 0, 0);

        private Handlers handler;

        public override void OnEnabled()
        {
            handler = new Handlers();

            Plugin.Singleton = this;
            ServerEv.EndingRound += handler.OnEndingRound;
            PlayerEv.Spawning += handler.OnSpawningPlayers;
            PlayerEv.Shooting += handler.OnShooting;
            PlayerEv.Died += handler.onPlayerDied;
            PlayerEv.Hurting += handler.OnPlayerHurt;
            PlayerEv.ThrowingItem += handler.OnThrowingItem;
            PlayerEv.DroppingItem += handler.OnDroppingItem;
            PlayerEv.InteractingDoor += handler.OnInteractingDoor;
            PlayerEv.InteractingElevator += handler.OnInteractingElevator;
            PlayerEv.InteractingLocker += handler.OnInteractingLocker;
            PlayerEv.PickingUpItem += handler.OnPickingUpItem;
            PlayerEv.EnteringPocketDimension += handler.OnEnterPocketDimension;
            ServerEv.RestartingRound += handler.OnRoundRestart;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            ServerEv.EndingRound -= handler.OnEndingRound;
            PlayerEv.Spawning -= handler.OnSpawningPlayers;
            PlayerEv.Shooting -= handler.OnShooting;
            PlayerEv.Died -= handler.onPlayerDied;
            PlayerEv.Hurting -= handler.OnPlayerHurt;
            PlayerEv.ThrowingItem -= handler.OnThrowingItem;
            PlayerEv.DroppingItem -= handler.OnDroppingItem;
            PlayerEv.InteractingDoor -= handler.OnInteractingDoor;
            PlayerEv.InteractingElevator -= handler.OnInteractingElevator;
            PlayerEv.InteractingLocker -= handler.OnInteractingLocker;
            PlayerEv.PickingUpItem -= handler.OnPickingUpItem;
            PlayerEv.EnteringPocketDimension -= handler.OnEnterPocketDimension;
            ServerEv.RestartingRound -= handler.OnRoundRestart;
            handler = null;

            base.OnDisabled();
        }

        public static Plugin Singleton;
    }
}
