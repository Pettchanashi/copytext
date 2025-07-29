using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using SamplePlugin.Windows;

namespace XIVNpcDialogueCopy
{
    public class Plugin : IDalamudPlugin
    {
        public string Name => "NPC Dialogue Copier";

        private ChatListener chatListener;

        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            chatListener = new ChatListener(pluginInterface);
        }

        public void Dispose()
        {
            chatListener?.Dispose();
        }
    }
}
