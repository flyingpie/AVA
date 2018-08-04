using ImGuiNET;

namespace MUI
{
    public static class ImGuiEx
    {
        public static bool InputText(int id, byte[] buffer, ref bool reset)
        {
            unsafe
            {
                if (reset)
                {
                    reset = false;

                    buffer[buffer.Length - 1] = 1;
                }

                var result = ImGui.InputText("q" + id, buffer, (uint)buffer.Length - 1, InputTextFlags.CallbackAlways, new TextEditCallback(data =>
                {
                    if (buffer[buffer.Length - 1] == 1)
                    {
                        data->CursorPos = data->BufTextLen;

                        data->SelectionStart = 0;
                        data->SelectionEnd = 0;

                        buffer[buffer.Length - 1] = 0;
                    }
                    return 0;
                }));

                if (!ImGui.IsAnyItemActive()) ImGui.SetKeyboardFocusHere();



                return result;
            }
        }
    }
}