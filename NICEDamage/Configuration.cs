using Dalamud.Configuration;
using System;

namespace NICEDamage
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 0;

        public bool NoFunAllowed { get; set; } = true;

        // The below exists just to make saving less cumbersome
        public void Save()
        {
            NICEDamagePlugin.PluginInterface.SavePluginConfig(this);
        }
    }
}
