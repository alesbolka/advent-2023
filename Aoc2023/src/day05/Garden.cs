namespace Day05
{
  class Garden
  {
    protected List<long> seeds;

    protected List<List<(long min, long max, long zero)>> transformations;

    public Garden(string[] instructions)
    {
      seeds = new();
      transformations = new() { new() };

      foreach (string line in instructions)
      {
        if (line == "")
        {
          continue;
        }
        else if (line.StartsWith("seeds: "))
        {
          ParseSeeds(line);
        }
        else if (line.EndsWith(" map:"))
        {
          // This relies on the order of input mappings to always be correct
          transformations.Add(new());
        }
        else
        {
          long[] ranges = Array.ConvertAll(line.Split(" "), long.Parse);
          transformations.Last().Add((
            ranges[1], // minimum, inclusive
            ranges[1] + ranges[2] - 1, // maximum, inclusive
            ranges[0] // offset for new value
          ));
        }
      }
    }

    public long FindLowestSeedLocation()
    {
      long locationMin = long.MaxValue;
      foreach (long seed in seeds)
      {
        long seedValue = MapSeedToLocation(seed);
        locationMin = Math.Min(seedValue, locationMin);
      }

      return locationMin;
    }

    protected void ParseSeeds(string rawLine)
    {
      seeds = Array.ConvertAll(rawLine[7..].Split(" "), long.Parse).ToList();
    }

    protected long MapSeedToLocation(long current)
    {
      foreach (var mappings in transformations)
      {
        current = GetMappedValue(current, mappings);
      }
      return current;
    }

    protected static long GetMappedValue(long original, List<(long, long, long)> mappings)
    {
      foreach ((long min, long max, long offset) in mappings)
      {
        if (original >= min && original <= max)
        {
          return offset + original - min;
        }
      }

      return original;
    }
  }
}