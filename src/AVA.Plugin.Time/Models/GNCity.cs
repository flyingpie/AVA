using System.Collections.Generic;
using System.IO;

namespace AVA.Plugin.Time.Models
{
    public class GNCity
    {
        private class Columns
        {
            public const int Name = 2;
            public const int AlternateNames = 3;
            public const int Population = 14;
            public const int TimeZone = 17;
        }

        public string Name { get; set; }

        public string[] AlternateNames { get; set; }

        public int Population { get; set; }

        public string TimeZoneId { get; set; }

        public static IEnumerable<GNCity> LoadGNCities(int minPopulation)
        {
            using (var reader = new StringReader(Resources.Resources.cities15000))
            {
                while (true)
                {
                    var line = reader.ReadLine();
                    if (line == null) break;

                    var cols = line.Split('\t');

                    var geoName = new GNCity()
                    {
                        Name = cols[Columns.Name],
                        AlternateNames = cols[Columns.AlternateNames].Split(','),
                        Population = int.Parse(cols[Columns.Population]),
                        TimeZoneId = cols[Columns.TimeZone]
                    };

                    if (geoName.Population < minPopulation) continue;

                    yield return geoName;
                }
            }
        }
    }
}