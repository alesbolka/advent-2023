namespace Day05
{
  class Garden
  {
    protected List<long> seeds;

    protected List<List<(long min, long max, long zero)>> transformations;

    // Ugly workaround for task 2, we parse the data in a different format
    protected List<(long start, long end)> seedList;

    public Garden(string[] instructions, bool seedRange = false)
    {
      seeds = new();
      transformations = new() { };

      seedList = new();

      foreach (string line in instructions)
      {
        if (line == "")
        {
          continue;
        }
        else if (line.StartsWith("seeds: "))
        {
          ParseSeeds(line, seedRange);
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

    protected void ParseSeeds(string rawLine, bool seedRange = false)
    {
      if (!seedRange)
      {
        seeds = Array.ConvertAll(rawLine[7..].Split(" "), long.Parse).ToList();
        return;
      }
      var rawSeeds = Array.ConvertAll(rawLine[7..].Split(" "), long.Parse);
      for (int ii = 0; ii < rawSeeds.Length; ii += 2)
      {
        seedList.Add((rawSeeds[ii], rawSeeds[ii] + rawSeeds[ii + 1] - 1));
      }
    }

    // Task2 code

    public long FindLowestSeedLocationByRanges()
    {
      List<(long start, long end)> current = seedList;

      foreach (var mappings in transformations)
      {
        List<(long start, long end)> nextList = new();

        foreach (var seedRange in current)
        {
          TransformRange(seedRange, mappings, nextList);
        }

        current = nextList;
      }

      long locationMin = long.MaxValue;

      foreach ((long min, long max) in current)
      {
        locationMin = Math.Min(min, locationMin);
      }

      return locationMin;
    }

    protected void TransformRange(
      (long start, long end) seedList,
      List<(long min, long max, long zero)> mappings,
      List<(long start, long end)> mapped
    )
    {
      List<(long, long)> unprocessedSeeds = new() { seedList };
      foreach (var range in mappings)
      {
        List<(long, long)> nextSeeds = new();
        foreach ((long Start, long End) seed in unprocessedSeeds)
        {
          if (seed.Start > range.max || seed.End < range.min)
          {
            // no overlap
            nextSeeds.Add(seed);
            continue;
          }

          long overlapStart = Math.Max(range.min, seed.Start);
          long overlapEnd = Math.Min(range.max, seed.End);

          if (overlapStart > seed.Start)
          {
            nextSeeds.Add((seed.Start, overlapStart - 1));
          }

          if (overlapEnd < seed.End)
          {
            nextSeeds.Add((overlapEnd + 1, seed.End));
          }

          mapped.Add(MapValues(overlapStart, overlapEnd, range.min, range.zero));
        }

        if (unprocessedSeeds.Count == 0)
        {
          return;
        }

        unprocessedSeeds = nextSeeds;
      }

      // There were no matches for these ranges, they simply get mapped 1 for 1
      foreach (var seedRange in unprocessedSeeds)
      {
        mapped.Add(seedRange);
      }
    }

    protected (long, long) MapValues(long newStart, long newEnd, long oldStart, long newZero)
    {
      long offset = newStart - oldStart;
      var newRange = (
        newZero + offset,
        newZero + newEnd - newStart + offset
      );

      return newRange;
    }
  }
}