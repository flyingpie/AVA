using MUI.DI;

namespace MLaunch.Core
{
    public class QueryContext
    {
        public bool HideUI { get; set; } = true;

        public string Query { get; set; }
    }
}