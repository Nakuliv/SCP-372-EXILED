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
        private static readonly Lazy<Plugin> LazyInstance = new Lazy<Plugin>(() => new Plugin());
        public static Plugin Instance => LazyInstance.Value;

        public override PluginPriority Priority => PluginPriority.Low;

        public override string Name { get; } = "SCP-372";
        public override string Author { get; } = "Cwaniak U.G";
        public override Version Version => new Version(1, 0, 0);

        private Handlers handler;

        public override void OnEnabled()
        {
            base.OnEnabled();
            handler = new Handlers();
            PlayerEv.ChangingRole += handler.OnChangingRole;
            PlayerEv.Shooting += handler.OnShooting;
            PlayerEv.Died += handler.onPlayerDied;
            PlayerEv.Hurting += handler.OnPlayerHurt;
            PlayerEv.ThrowingGrenade += handler.OnThrowingGrenade;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            PlayerEv.ChangingRole -= handler.OnChangingRole;
            PlayerEv.Shooting -= handler.OnShooting;
            PlayerEv.Died -= handler.onPlayerDied;
            PlayerEv.Hurting -= handler.OnPlayerHurt;
            PlayerEv.ThrowingGrenade -= handler.OnThrowingGrenade;

            handler = null;
        }
    }
}
