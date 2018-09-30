using NCalc2;

namespace AVA.Plugins.UnitConverter
{
    public class Converter
    {
        public string Name { get; set; }

        public string UnitFrom { get; set; }

        public string UnitTo { get; set; }

        public string Conversion { get; set; }

        public string Format { get; set; }

        public float Convert(float from)
        {
            try
            {
                return float.Parse(new Expression(Conversion.Replace("X", "x").Replace("x", from.ToString())).Evaluate().ToString());
            }
            catch { }

            return 0;
        }
    }
}