using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Loader;
using System.Reflection;

using PlayerEv = Exiled.Events.Handlers.Player;
using ServerEv = Exiled.Events.Handlers.Server;
using MEC;


namespace SCP_372
{
    public class Plugin : Plugin<Config>
    {

        public override string Name { get; } = "SCP-372";
        public override string Author { get; } = "Cwaniaak.";
        public override Version Version => new Version(1, 3, 2);
        public override Version RequiredExiledVersion => new Version(2, 8, 0);

        private Handlers handler;

        public override void OnEnabled()
        {
            handler = new Handlers();
            Plugin.Singleton = this;
            PlayerEv.ChangingRole += handler.OnChangingRole;
            PlayerEv.Shooting += handler.OnShooting;
            PlayerEv.Died += handler.onPlayerDied;
            PlayerEv.Hurting += handler.OnPlayerHurt;
            PlayerEv.ThrowingItem += handler.OnThrowingItem;
            PlayerEv.EnteringPocketDimension += handler.OnEnterPocketDimension;
            ServerEv.RestartingRound += handler.OnRoundRestart;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            PlayerEv.ChangingRole -= handler.OnChangingRole;
            PlayerEv.Shooting -= handler.OnShooting;
            PlayerEv.Died -= handler.onPlayerDied;
            PlayerEv.Hurting -= handler.OnPlayerHurt;
            PlayerEv.ThrowingGrenade -= handler.OnThrowingGrenade;
            ServerEv.RestartingRound -= handler.OnRoundRestart;
            handler = null;

            base.OnDisabled();
        }

        public static Plugin Singleton;
    }
}
