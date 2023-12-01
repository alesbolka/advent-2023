namespace Day01
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
      int sum = 0;

      foreach (var line in data.GetLines())
      {
        var firstNumber = 10;
        var lastNumber = 10;

        foreach (var symbol in line)
        {
          int number = symbol - '0';

          if (number >= 0 && number < 10)
          {
            lastNumber = number;

            if (firstNumber == 10)
            {
              firstNumber = number;
            }
          }
        }

        if (lastNumber == 10)
        {
          continue;
        }

        sum += 10 * firstNumber + lastNumber;
      }

      return sum;
    }
  }
}