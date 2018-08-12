namespace AVA.Core
{
    public class QueryContext
    {
        private string _text;
        private string _textLower;

        public bool HasPrefix(string prefix) => _textLower.StartsWith(prefix.ToLowerInvariant());

        public bool HideUI { get; set; } = true;

        public bool IsEmpty => string.IsNullOrWhiteSpace(Text);

        public bool ResetText { get; set; } = true;

        public string Arg
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_text)) return string.Empty;

                var space = _text.IndexOf(' ');

                if (space < 0) return string.Empty;

                return _text.Substring(space + 1);
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                _textLower = value?.ToLowerInvariant();
            }
        }

        public void Reset()
        {
            HideUI = true;
            ResetText = true;
            Text = null;
        }

        public override string ToString() => $"{nameof(QueryContext)}({Text})";
    }
}