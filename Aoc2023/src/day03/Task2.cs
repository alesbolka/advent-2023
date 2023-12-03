namespace Day03
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
      Schematic schema = new Schematic(data);

      return schema.FindGearRatio();
    }
  }
}