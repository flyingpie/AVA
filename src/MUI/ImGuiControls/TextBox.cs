using ImGuiNET;
using System;

namespace MUI.ImGuiControls
{
    public class TextBox
    {
        private byte[] _termBuffer = new byte[1024];
        private string _termBufferString;

        private bool _reset = false;
        private int _id;

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public bool IsChanged { get; private set; }

        public string Text
        {
            get => _termBufferString;
            set
            {
                _termBuffer.CopyToBuffer(value);
                _termBufferString = value;

                _reset = true;
                _id++;
            }
        }

        public void ResetChanged()
        {
            IsChanged = false;
        }

        public void Draw()
        {
            ImGui.PushItemWidth(-1);

            unsafe
            {
                ImGui.InputText("q" + _id, _termBuffer, (uint)_termBuffer.Length - 1, InputTextFlags.CallbackAlways, new TextEditCallback(data =>
                {
                    if (_reset)
                    {
                        data->CursorPos = data->BufTextLen;

                        data->SelectionStart = 0;
                        data->SelectionEnd = 0;

                        _reset = false;
                    }
                    return 0;
                }));

                if (!ImGui.IsAnyItemActive()) ImGui.SetKeyboardFocusHere();
            }

            var term = _termBuffer.BufferToString();
            if (term != _termBufferString || _reset)
            {
                _termBufferString = term;
                IsChanged = true;
            }

            ImGui.PopItemWidth();
        }
    }
}