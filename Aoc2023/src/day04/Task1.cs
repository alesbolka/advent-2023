namespace Day04
{
  class Task1
  {
    string[] lines;

    public Task1(AdventUtils.Input input)
    {
      lines = input.GetLines();
    }

    public int Execute()
    {
      int sum = 0;
      foreach (string line in lines)
      {
        Ticket tick = new Ticket(line);

        sum += tick.PointValue();
      }
      return sum;
    }
  }
}