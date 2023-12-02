
namespace Day02
{
  class Task1
  {
    AdventUtils.Input data;

    public Task1(AdventUtils.Input input)
    {
      data = input;
    }

    public int Execute()
    {
      int res = 0;

      Bag bag = new Bag(new(){
        {"red", 12},
        {"green", 13},
        {"blue", 14},
      });

      foreach (var line in data.GetLines())
      {
        res += bag.TestGame(line);
      }

      return res;
    }
  }
}