
namespace Day02
{
  class Task2
  {
    AdventUtils.Input data;

    public Task2(AdventUtils.Input input)
    {
      data = input;
    }

    public int Execute()
    {
      int res = 0;

      foreach (var line in data.GetLines())
      {
        res += Bag.PowerOfBag(line);
      }

      return res;
    }
  }
}