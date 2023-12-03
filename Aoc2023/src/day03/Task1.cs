namespace Day03
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
      Schematic schema = new Schematic(data);

      var partNumbers = schema.FindPartNumbers();
      return partNumbers.Sum();
    }
  }
}