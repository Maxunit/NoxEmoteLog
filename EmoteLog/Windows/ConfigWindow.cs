using Dalamud.Interface;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Components;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using System;
using System.Numerics;

namespace EmoteLog.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;

    private Plugin Plugin { get; set; }

    public ConfigWindow(Plugin plugin) : base(
        "Zanarkand Emote Log Configuration",
        ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
        ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.AlwaysAutoResize)
    {
        this.Size = new Vector2(0, 0);
        this.SizeCondition = ImGuiCond.Always;

        this.Configuration = plugin.Configuration;

        this.Plugin = plugin;
    }

    public void Dispose() { }

    public override void Draw()
    {
        var logSize = this.Configuration.LogSize;
        var collapseSpam = this.Configuration.CollapseSpam;
        var showTimestamps = this.Configuration.ShowTimestamps;
        var openOnLogin = this.Configuration.OpenOnLogin;
        var moveWindow = this.Configuration.MoveWindow;
        var resizeWindow = this.Configuration.ResizeWindow;
        var showWindowFrames = this.Configuration.ShowWindowFrames;
        var inCombat = this.Configuration.InCombat;
        var inInstance = this.Configuration.InInstance;
        var inCutscenes = this.Configuration.InCutscenes;
        var hideEmpty = this.Configuration.HideEmpty;
        var showClearButton = this.Configuration.ShowClearButton;
        var useCustomFontSize = this.Configuration.UseCustomFontSize;
        var fontSize = this.Configuration.FontSize;
        var iconFontSize = this.Configuration.IconFontSize;
        var wrapText = this.Configuration.WrapText;

        ImGui.Text("Log Settings");
        ImGui.SetNextItemWidth(120f);
        if (ImGui.InputInt("Log size", ref logSize))
        {
            if (logSize > 0)
            {
                Configuration.LogSize = logSize;
                Configuration.Save();
            }
        }
        if (ImGui.Checkbox("Collapse duplicates into one line", ref collapseSpam))
        {
            Configuration.CollapseSpam = collapseSpam;
            Configuration.Save();
        }
        if (ImGui.Checkbox("Show timestamps", ref showTimestamps))
        {
            Configuration.ShowTimestamps = showTimestamps;
            Configuration.Save();
        }
        if (ImGui.Checkbox("Show clear button in the Zanarkand Emote Log", ref showClearButton))
        {
            Configuration.ShowClearButton = showClearButton;
            Configuration.Save();
        }
        if (ImGui.Checkbox("Wrap Zanarkand Emote Log text", ref wrapText))
        {
            Configuration.WrapText = wrapText;
            Configuration.Save();
        }
        if (ImGui.IsItemHovered())
        {
            ImGui.SetTooltip("Puts log entries into multiple lines if they don't fit in one");
        }

        ImGui.Separator();
        ImGui.Text("Window Settings");
        if (ImGui.Checkbox("Open on login", ref openOnLogin))
        {
            Configuration.OpenOnLogin = openOnLogin;
            Configuration.Save();
        }
        if (ImGui.Checkbox("Allow moving the Zanarkand Emote Log", ref moveWindow))
        {
            Configuration.MoveWindow = moveWindow;
            Configuration.Save();
        }
        if (ImGui.Checkbox("Allow resizing the Zanarkand Emote Log", ref resizeWindow))
        {
            Configuration.ResizeWindow = resizeWindow;
            Configuration.Save();
        }
        if (ImGui.Checkbox("Show Zanarkand Emote Log's frames", ref showWindowFrames))
        {
            Configuration.ShowWindowFrames = showWindowFrames;
            Configuration.Save();
        }
        if (ImGui.Checkbox("Show Zanarkand Emote Log in combat", ref inCombat))
        {
            Configuration.InCombat = inCombat;
            Configuration.Save();
        }
        if (ImGui.Checkbox("Show Zanarkand Emote Log in instance", ref inInstance))
        {
            Configuration.InInstance = inInstance;
            Configuration.Save();
        }
        if (ImGui.Checkbox("Show Zanarkand Emote Log in cutscene", ref inCutscenes))
        {
            Configuration.InCutscenes = inCutscenes;
            Configuration.Save();
        }
        if (ImGui.Checkbox("Hide Zanarkand Emote Log when empty", ref hideEmpty))
        {
            Configuration.HideEmpty = hideEmpty;
            Configuration.Save();
        }
        ImGui.Separator();
        ImGui.Text("Font Settings");

        if (ImGui.Checkbox("Use custom font size", ref useCustomFontSize))
        {
            Configuration.UseCustomFontSize = useCustomFontSize;
            Configuration.Save();
        }
        if (useCustomFontSize)
        {
            ImGui.SetNextItemWidth(120f);
            if (ImGui.InputFloat("Zanarkand Emote Log font size", ref fontSize, 1f, 2f, "%.1fpt"))
            {
                fontSize = Math.Max(fontSize, 1f);
                Configuration.FontSize = fontSize;
                Configuration.Save();
            }
            if (ImGui.IsItemHovered())
            {
                ImGui.SetTooltip("Font size is affected by the Global Font Scale setting, default value is 12pt");
            }
            ImGui.SetNextItemWidth(120f);
            if (ImGui.InputFloat("Clear button font size", ref iconFontSize, 1f, 2f, "%.1fpt"))
            {
                iconFontSize = Math.Max(iconFontSize, 1f);
                Configuration.IconFontSize = iconFontSize;
                Configuration.Save();
            }
            if (ImGui.IsItemHovered())
            {
                ImGui.SetTooltip("Font size is affected by the Global Font Scale setting, default value is 12pt");
            }
        }
        if (ImGuiComponents.IconButtonWithText(FontAwesomeIcon.Sync, "Refresh fonts"))
        {
            Plugin.PluginInterface.UiBuilder.FontAtlas.BuildFontsAsync();
        }
        ImGui.TextColored(ImGuiColors.DalamudGrey, "For font size changes to take effect press the refresh button");
    }
}
