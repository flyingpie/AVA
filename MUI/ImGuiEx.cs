using ImGuiNET;
using System;

namespace MUI
{
    public static class ImGuiEx
    {
        public static bool InputText(string id, byte[] buffer, ref bool reset, ref bool resetSelection)
        {
            var resetSele = resetSelection ? new IntPtr(1) : new IntPtr(0);

            unsafe
            {
                if (reset) return reset = false;

                var result = ImGui.InputText("query", buffer, (uint)buffer.Length, InputTextFlags.CallbackAlways, new TextEditCallback(data =>
                {
                    if (data->UserData == (IntPtr)1)
                    {
                        data->CursorPos = buffer.Length;

                        data->SelectionStart = 0;
                        data->SelectionEnd = 0;
                    }
                    return 0;
                }), resetSele);

                if (!ImGui.IsLastItemActive()) ImGui.SetKeyboardFocusHere();

                return result;
            }
        }
    }
}