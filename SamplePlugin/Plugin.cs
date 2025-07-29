using Dalamud.Plugin;

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
