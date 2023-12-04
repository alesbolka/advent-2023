namespace Day04
{
  class Task2
  {
    string[] lines;

    public Task2(AdventUtils.Input input)
    {
      lines = input.GetLines();
    }

    public int Execute()
    {
      int count = 0;
      Dictionary<int, int> repeats = new();

      int ii = 0;
      foreach (string line in lines)
      {
        Ticket tick = new Ticket(line);
        count += repeats.GetValueOrDefault(ii, 0) + 1;

        int power = tick.GetMatchCount();
        for (int jj = 1; jj <= power; jj++)
        {
          repeats[ii + jj] = repeats.GetValueOrDefault(ii + jj, 0) + 1 + repeats.GetValueOrDefault(ii, 0);
        }

        ii++;
      }
      return count;
    }
  }
}