namespace Day05
{
  class Task1
  {
    Garden garden;

    public Task1(AdventUtils.Input input)
    {
      garden = new Garden(input.GetLines());
    }

    public long Execute()
    {
      return garden.FindLowestSeedLocation();
    }
  }
}