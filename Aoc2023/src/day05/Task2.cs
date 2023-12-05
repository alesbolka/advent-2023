namespace Day05
{
  class Task2
  {
    Garden garden;

    public Task2(AdventUtils.Input input)
    {
      garden = new Garden(input.GetLines(), true);
    }

    public long Execute()
    {
      return garden.FindLowestSeedLocationByRanges();
    }
  }
}