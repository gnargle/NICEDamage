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
        this.SizeConstraints = new WindowSizeConstraints {
            MinimumSize = new Vector2(650, 250),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue),
        };

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

        ImGui.Text("Custom funny numbers! Add your own.\nDon't add too many because the game has to check these for every flyout.");
        if (ImGui.BeginTable("CustomNumbers", 3))
        {
            ImGui.TableSetupColumn("Funny Number");
            ImGui.TableSetupColumn("Funny Message");
            ImGui.TableSetupColumn("Commands");
            ImGui.TableHeadersRow();
            ImGui.TableNextRow();
            for (int i = 0; i < configuration.CustomFunnyNumbers.Count; i++)
            {
                ImGui.TableNextColumn();
                var funnyNumber = configuration.CustomFunnyNumbers[i].Number.Value;
                ImGui.SetNextItemWidth(150);
                if (ImGui.InputInt("###funnyNumber" + i, ref funnyNumber))
                {
                    configuration.CustomFunnyNumbers[i].Number = funnyNumber;
                }

                ImGui.TableNextColumn();
                ImGui.SetNextItemWidth(200);
                var funnyMessage = configuration.CustomFunnyNumbers[i].Message;
                if (ImGui.InputText("###funnyMessage" + i, ref funnyMessage))
                {
                    configuration.CustomFunnyNumbers[i].Message = funnyMessage;
                }
                
                ImGui.TableNextColumn();
                if (ImGui.Button("Delete###deleteEntry" + i))
                {
                    configuration.CustomFunnyNumbers.RemoveAt(i);
                }
            }
            ImGui.TableNextColumn();
            ImGui.TableNextColumn();
            ImGui.TableNextColumn();
            if (ImGui.Button("Add"))
            {
                configuration.CustomFunnyNumbers.Add(new CustomFunnyNumber()
                {
                    Number = 0,
                    Message = string.Empty
                });
            }
            ImGui.EndTable();
        }
    }
}
