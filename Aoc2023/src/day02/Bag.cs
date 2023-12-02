using System.Text.RegularExpressions;

namespace Day02
{
  class Bag
  {
    protected Dictionary<string, int> expectedConfiguration;
    protected Regex cubePattern;

    public Bag(Dictionary<string, int> expected)
    {
      cubePattern = new Regex("(?<count>\\d+)\\s(?<colour>\\w+)");
      expectedConfiguration = expected;
    }

    public int TestGame(string line)
    {
      String[] parts = line.Split(":");
      int gameId = int.Parse(parts[0].Split(" ")[1]);


      if (this.isPossible(parts[1].Split(";")))
      {
        return gameId;
      }
      return 0;
    }


    protected bool isPossible(string[] reveals)
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