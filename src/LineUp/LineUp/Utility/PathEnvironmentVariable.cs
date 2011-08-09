using System;
using System.Collections.Generic;
using System.Linq;

namespace LineUp.Utility
{
    public class PathEnvironmentVariable
    {
        public PathEnvironmentVariable(string value)
        {
            if (value == null) throw new ArgumentNullException("value");
            Value = value;
            Locations = value.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);
        }

        public PathEnvironmentVariable(IEnumerable<string> locations)
        {
            if (locations == null) throw new ArgumentNullException("locations");
            Value = string.Join(";", locations);
            Locations = Value.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public string Value { get; private set; }

        public string[] Locations { get; private set; }

        public bool IsEquivalentTo(PathEnvironmentVariable other)
        {
            return Locations.Length == other.Locations.Length
                && Locations.All(l => other.Locations.Contains(l, StringComparer.InvariantCultureIgnoreCase));
        }

        public bool IsEquivalentTo(string otherPathValue)
        {
            return IsEquivalentTo(new PathEnvironmentVariable(otherPathValue));
        }

        public PathEnvironmentVariable AddLocations(IEnumerable<string> additionalLocations)
        {
            return new PathEnvironmentVariable(Locations.Concat(additionalLocations));
        }
    }
}