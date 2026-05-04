using Dalamud.Configuration;
using System;
using System.Collections.Generic;

namespace NICEDamage
{
    [Serializable]
    public record CustomFunnyNumber
    {
        public int? Number { get; set; } = null;
        public string Message { get; set; } = string.Empty;
    }
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 0;

        public bool NoFunAllowed { get; set; } = false;
        public bool YesFunAllowed { get; set; } = false;
        public List<CustomFunnyNumber> CustomFunnyNumbers { get; set; } = new();

        // The below exists just to make saving less cumbersome
        public void Save()
        {
            NICEDamagePlugin.PluginInterface.SavePluginConfig(this);
        }
    }
}
