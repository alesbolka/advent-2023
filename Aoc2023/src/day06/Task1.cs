namespace Day06
{
  class Task1
  {
    List<int> times;
    List<int> distances;

    public Task1(AdventUtils.Input input)
    {

      var lines = input.GetLines();
      times = ParseNumbers(lines[0]);
      distances = ParseNumbers(lines[1]);

    }

    protected List<int> ParseNumbers(string line)
    {
      List<int> res = new();
      var parts = line.Split(":")[1].Trim().Split(" ");
      foreach (string part in parts)
      {
        if (part.Trim() == "")
        {
          continue;
        }

        res.Add(int.Parse(part));
      }

      return res;
    }

    public int Execute()
    {
      int res = 1;

      for (int ii = 0; ii < times.Count; ii++)
      {
        int timeTotal = times[ii];
        int minDistance = distances[ii];

        var DD = Math.Sqrt(timeTotal * timeTotal - 4 * minDistance);
        var xx = 0.5 * (timeTotal - DD);
        var yy = 0.5 * (timeTotal + DD);
        // Console.WriteLine($"x{xx}, y{yy}");

        int lowerBound = (int)Math.Floor(xx + 1);
        int upperBound = (int)Math.Ceiling(yy - 1);
        // Console.WriteLine($"For time {timeTotal} and dist {minDistance} we can charge between {lowerBound} and {upperBound}");

        int waysToWin = upperBound - lowerBound + 1;
        // Console.WriteLine($"ways to win: {waysToWin}");

        res *= waysToWin;
      }
      return res;
    }
  }
}