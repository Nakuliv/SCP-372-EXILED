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

        public override PluginPriority Priority => PluginPriority.Default;

        public override string Name { get; } = "SCP-372";
        public override string Author { get; } = "Cwaniak U.G";
        public override Version Version => new Version(2, 1, 0);
        public override Version RequiredExiledVersion => new Version(2, 3, 4);

        private Handlers handler;

        public override void OnEnabled()
        {
            handler = new Handlers();
            Plugin.Singleton = this;
            PlayerEv.ChangingRole += handler.OnChangingRole;
            PlayerEv.Shooting += handler.OnShooting;
            PlayerEv.Died += handler.onPlayerDied;
            PlayerEv.Hurting += handler.OnPlayerHurt;
            PlayerEv.ThrowingGrenade += handler.OnThrowingGrenade;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            PlayerEv.ChangingRole -= handler.OnChangingRole;
            PlayerEv.Shooting -= handler.OnShooting;
            PlayerEv.Died -= handler.onPlayerDied;
            PlayerEv.Hurting -= handler.OnPlayerHurt;
            PlayerEv.ThrowingGrenade -= handler.OnThrowingGrenade;
            handler = null;

            base.OnDisabled();
        }

        public static Plugin Singleton;
    }
}
