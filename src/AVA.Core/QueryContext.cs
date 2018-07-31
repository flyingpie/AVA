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