using ImGuiNET;
using System;

namespace MUI
{
    public static class ImGuiEx
    {
        public static bool InputText(string id, byte[] buffer, bool reset, bool resetSelection)
        {
            unsafe
            {
                if (reset) return false;

                var result = ImGui.InputText("query", buffer, (uint)buffer.Length, InputTextFlags.CallbackAlways, new TextEditCallback(data =>
                {
                    if (data->UserData == (IntPtr)1)
                    {
                        data->CursorPos = buffer.Length;

                        data->SelectionStart = 0;
                        data->SelectionEnd = 0;
                    }
                    return 0;
                }), new IntPtr(resetSelection ? 1 : 0));

                if (!ImGui.IsLastItemActive()) ImGui.SetKeyboardFocusHere();

                return result;
            }
        }
    }
}