using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;

namespace SCP_372
{
    public class Config : IConfig
    {
        [Description("Whether or not this plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;
        public int SpawnChance { get; set; } = 50;
        public int Max_SCP372_Count { get; set; } = 1;
        public Exiled.API.Features.Broadcast SpawnMessage { get; set; } = new Exiled.API.Features.Broadcast("<b>You have spawned as <color=red>SCP-372</color></b>\n<i>you are invisible (unless you shoot or speak), cooperate with SCPs</i>", 10);

        [Description("how much health should SCP-372 have")]
        public int Health { get; set; } = 150;
    }
}
