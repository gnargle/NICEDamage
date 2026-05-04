using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;
 
namespace NICEDamage.Windows;

public class ConfigWindow : Window, IDisposable
{
    private readonly Configuration configuration;
    
    public ConfigWindow(NICEDamagePlugin plugin) : base("NiceDamage Config")
    {
        Flags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
                ImGuiWindowFlags.NoScrollWithMouse;

        Size = new Vector2(232, 90);
        SizeCondition = ImGuiCond.Always;

        configuration = plugin.Configuration;
    }
    public void Dispose() { }
    
    public override void Draw()
    {
        // Can't ref a property, so use a local copy
        var configValue = configuration.NoFunAllowed;
        if (ImGui.Checkbox("NO FUN ALLOWED", ref configValue))
        {
            configuration.NoFunAllowed = configValue;
            // Can save immediately on change if you don't want to provide a "Save and Close" button
            configuration.Save();
        }
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Disables the 67 trigger");
    }
}
