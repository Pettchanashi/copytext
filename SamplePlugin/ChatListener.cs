using System;
using System.Threading;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Plugin;
using TextCopy;

namespace XIVNpcDialogueCopy
{
    public class ChatListener : IDisposable
    {
        private readonly DalamudPluginInterface pluginInterface;

        public ChatListener(DalamudPluginInterface pluginInterface)
        {
            this.pluginInterface = pluginInterface;
            pluginInterface.Framework.Gui.Chat.OnChatMessage += OnChatMessage;
        }

        private void OnChatMessage(XivChatType type, uint senderId, SeString sender, SeString message, ref bool isHandled)
        {
            if (type != XivChatType.NPCDialogue)
                return;

            try
            {
                // Use TextCopy for clipboard access on STA
                Thread thread = new Thread(() =>
                {
                    ClipboardService.SetText(message.TextValue);
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();

                pluginInterface.Framework.Gui.Chat.Print("[Plugin] NPC dialogue copied to clipboard.");
            }
            catch (Exception ex)
            {
                pluginInterface.Framework.Gui.Chat.PrintError($"[Plugin Error] Clipboard failed: {ex.Message}");
            }
        }

        public void Dispose()
        {
            pluginInterface.Framework.Gui.Chat.OnChatMessage -= OnChatMessage;
        }
    }
}