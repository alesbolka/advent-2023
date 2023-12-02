using System.Text.RegularExpressions;

namespace Day02
{
  class Bag
  {
    protected Dictionary<string, int> expectedConfiguration;
    protected Dictionary<string, int> minimumNeeded;
    protected Regex cubePattern;

    public Bag(Dictionary<string, int> expected)
    {
      cubePattern = new Regex("(?<count>\\d+)\\s(?<colour>\\w+)");
      minimumNeeded = new();
      expectedConfiguration = expected;
    }
    public Bag() : this(new Dictionary<string, int>()) { }

    public static int PowerOfBag(string line)
    {
      Bag bag = new Bag();
      String[] parts = line.Split(":");
      // int gameId = int.Parse(parts[0].Split(" ")[1]);
      string[] reveals = parts[1].Split(";");

      foreach (string reveal in reveals)
      {
        bag.AddReveal(reveal);
      }

      int power = 1;
      foreach (var keyPair in bag.minimumNeeded)
      {
        power *= keyPair.Value;
      }

      return power;
    }

    public void AddReveal(string reveal)
    {
      var matches = cubePattern.Matches(reveal);

      foreach (Match match in matches)
      {
        string colour = match.Groups["colour"].Value;
        int count = int.Parse(match.Groups["count"].Value);
        if (!minimumNeeded.ContainsKey(colour) || minimumNeeded[colour] < count)
        {
          minimumNeeded[colour] = count;
        }
      }
    }

    public int TestGame(string line)
    {
      String[] parts = line.Split(":");
      int gameId = int.Parse(parts[0].Split(" ")[1]);

      if (this.IsPossible(parts[1].Split(";")))
      {
        return gameId;
      }
      return 0;
    }


    protected bool IsPossible(string[] reveals)
    {
      foreach (var reveal in reveals)
      {
        var matches = cubePattern.Matches(reveal);

        foreach (Match match in matches)
        {
          string colour = match.Groups["colour"].Value;
          if (!expectedConfiguration.ContainsKey(colour))
          {
            return false;
          }

          int count = int.Parse(match.Groups["count"].Value);
          if (count > expectedConfiguration[colour])
          {

            return false;
          }
        }
      }
      return true;
    }
  }
}