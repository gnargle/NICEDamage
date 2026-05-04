using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Interface.Windowing;
using Dalamud.Game.Gui.FlyText;
using Dalamud.Game.Text.SeStringHandling;
using Microsoft.VisualBasic;
using System;
using Dalamud.Plugin.Services;
using NICEDamage.Windows;

namespace NICEDamage
{
    public sealed class NICEDamagePlugin : IDalamudPlugin
    {
        public string Name => "NICE Damage Flyouts";

        [PluginService]
        internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
        private IFlyTextGui FlyTextGUI { get; init; }
        public WindowSystem WindowSystem = new("NICEDamage");
        public Configuration Configuration { get; init; }
        private ConfigWindow ConfigWindow { get; init; }

        public NICEDamagePlugin(IFlyTextGui flyTextGui)
        {
            this.FlyTextGUI = flyTextGui;
            Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            FlyTextGUI.FlyTextCreated += FlyTextGUI_FlyTextCreated;
            
            ConfigWindow = new ConfigWindow(this);
            WindowSystem.AddWindow(ConfigWindow);
            PluginInterface.UiBuilder.Draw += WindowSystem.Draw;
            
            PluginInterface.UiBuilder.OpenConfigUi += ToggleConfigUi;
        }
        public void ToggleConfigUi() => ConfigWindow.Toggle();

        private void FlyTextGUI_FlyTextCreated(ref FlyTextKind kind, ref int val1, ref int val2, ref SeString text1, ref SeString text2, ref uint color, ref uint icon, ref uint damageTypeIcon, ref float yOffset, ref bool handled)
        {
            switch (kind)
            {
                case FlyTextKind.Dodge:
                case FlyTextKind.Incapacitated:
                case FlyTextKind.Interrupted:
                case FlyTextKind.Invulnerable:
                case FlyTextKind.Miss:
                case FlyTextKind.NamedDodge:
                case FlyTextKind.NamedMiss:
                case FlyTextKind.Reflect:
                case FlyTextKind.Reflected:
                case FlyTextKind.Resist:
                    break;
                default:
                    {
                        var valStr = Convert.ToString(val1);
                        if (valStr.EndsWith("42069"))
                        {
                            text2.Append(" OMGOMGOMG NICE DUDE NIIIIIICE ");
                        }
                        else if (valStr.EndsWith("69"))
                        {
                            text2.Append(" NICE ");
                        }
                        else if (valStr.EndsWith("67"))
                        {
                            if(!Configuration.NoFunAllowed)
                                text2.Append(" SIX SEVEEEEEN ");
                            if (Configuration.YesFunAllowed)
                            {
                                text1.Append("SIX SEVEN SIX SEVENNNNNNNN");
                            }
                        }

                        foreach (var custom in Configuration.CustomFunnyNumbers)
                        {
                            if (valStr.EndsWith(custom.Number.ToString()))
                            {
                                text2.Append($" {custom.Message} ");
                            }
                        }
                        break;
                    }
            }
        }


        public void Dispose()
        {
            // Unregister all actions to not leak anything during disposal of plugin
            PluginInterface.UiBuilder.Draw -= WindowSystem.Draw;
            PluginInterface.UiBuilder.OpenConfigUi -= ToggleConfigUi;
            this.WindowSystem.RemoveAllWindows();
            ConfigWindow.Dispose();
        }
    }
}
