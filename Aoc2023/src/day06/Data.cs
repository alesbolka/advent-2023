namespace Day06
{
  public class Data : AdventUtils.Input
  {
    public Data(string input) : base(input)
    {
    }

    public static Data fromSample()
    {
      return new Data(@"Time:      7  15   30
Distance:  9  40  200");
    }

    public static Data fromInput()
    {
      return new Data(@"Time:        48     93     85     95
Distance:   296   1928   1236   1391");
    }
  }
}