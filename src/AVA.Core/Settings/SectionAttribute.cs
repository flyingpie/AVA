using System;

namespace AVA.Core.Settings
{
    public class SectionAttribute : Attribute
    {
        public string Name { get; set; }

        public SectionAttribute(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}