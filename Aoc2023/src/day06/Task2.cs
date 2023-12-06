namespace Day06
{
  class Task2
  {
    List<long> times;
    List<long> distances;

    public Task2(AdventUtils.Input input)
    {

      var lines = input.GetLines();
      times = ParseNumbers(lines[0]);
      distances = ParseNumbers(lines[1]);
    }

    protected List<long> ParseNumbers(string line)
    {
      List<long> res = new();
      var number = line.Split(":")[1].Trim().Replace(" ", "");

      res.Add(long.Parse(number));

      return res;
    }

    public long Execute()
    {
      long res = 1;

      long timeTotal = times[0];
      long minDistance = distances[0];

      var DD = Math.Sqrt(timeTotal * timeTotal - 4 * minDistance);
      var xx = 0.5 * (timeTotal - DD);
      var yy = 0.5 * (timeTotal + DD);
      // Console.WriteLine($"x{xx}, y{yy}");

      long lowerBound = (long)Math.Floor(xx + 1);
      long upperBound = (long)Math.Ceiling(yy - 1);
      // Console.WriteLine($"For time {timeTotal} and dist {minDistance} we can charge between {lowerBound} and {upperBound}");

      long waysToWin = upperBound - lowerBound + 1;
      // Console.WriteLine($"ways to win: {waysToWin}");

      res *= waysToWin;

      return res;
    }
  }
}