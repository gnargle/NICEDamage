using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Interface.Windowing;
using Dalamud.Game.Gui.FlyText;
using Dalamud.Game.Text.SeStringHandling;
using Microsoft.VisualBasic;
using System;

namespace NICEDamage
{
    public sealed class NICEDamagePlugin : IDalamudPlugin
    {
        public string Name => "NICE Damage Flyouts";

        private DalamudPluginInterface PluginInterface { get; init; }
        private FlyTextGui FlyTextGUI { get; init; }
        public WindowSystem WindowSystem = new("NICEDamage");

        public NICEDamagePlugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
             [RequiredVersion("1.0")] FlyTextGui flyTextGui)

        {
            this.PluginInterface = pluginInterface;
            this.FlyTextGUI = flyTextGui;

            // you might normally want to embed resources and load them from the manifest stream
            var imagePath = Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "goat.png");
            var goatImage = this.PluginInterface.UiBuilder.LoadImage(imagePath);
            FlyTextGUI.FlyTextCreated += FlyTextGUI_FlyTextCreated;
        }

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
                case FlyTextKind.NamedFullyResisted:
                case FlyTextKind.NamedHasNoEffect:
                case FlyTextKind.NamedIconFullyResisted:
                case FlyTextKind.NamedIconHasNoEffect:
                case FlyTextKind.NamedIconInvulnerable:
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
                            text2 = "OMGOMGOMG NICE DUDE NIIIIIICE";
                        }
                        else if (valStr.EndsWith("69"))
                        {
                            text2 = "NICE";
                        }
                        else
                        {
                            text2 = string.Empty;
                        }
                        break;
                    }
            }
        }


        public void Dispose()
        {
            this.WindowSystem.RemoveAllWindows();
        }
    }
}
