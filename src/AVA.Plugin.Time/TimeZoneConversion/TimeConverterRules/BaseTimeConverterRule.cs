using System.Text.RegularExpressions;

namespace ExpNodaTime.TimeConverterRules
{
    public abstract class BaseTimeConverterRule : ITimeConverterRule
    {
        public abstract string Name { get; }

        public abstract Regex Regex { get; }

        public bool TryParse(TimeConverterContext context, out TimeConverterResult result)
        {
            result = null;

            var match = Regex.Match(context.Expression);

            if (!match.Success) return false;

            return TryParseUtc(context, match, out result);
        }

        protected abstract bool TryParseUtc(TimeConverterContext context, Match match, out TimeConverterResult result);
    }
}