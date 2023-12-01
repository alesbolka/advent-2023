using System.Text.RegularExpressions;

namespace Day01
{
  class Task2
  {
    AdventUtils.Input data;
    Regex pattern;

    public Task2(AdventUtils.Input input)
    {
      pattern = new Regex(@"(one|two|three|four|five|six|seven|eight|nine)");
      data = input;
    }

    public int Execute()
    {
      int res = 0;
      foreach (var line in data.GetLines())
      {
        int firstNumber = 10;
        int lastNumber = 10;

        string word = "";


        foreach (char ch in line)
        {
          word += ch;
          int num = ParseState(ch, word);
          if (num != 10)
          {
            lastNumber = num;
            // Remember the last character of the word in case of overlaps ("eightwo" should parse to 82 overall)
            word = ch.ToString();
            if (firstNumber == 10)
            {
              firstNumber = num;
            }
            continue;
          }
        }

        // Console.WriteLine($"{line}: {firstNumber}{lastNumber}");

        if (firstNumber == 10 || lastNumber == 10)
        {
          continue;
        }
        res += firstNumber * 10 + lastNumber;
      }
      return res;
    }

    private int ParseState(char symbol, string word)
    {
      int num = symbol - '0';
      if (num > 0 && num < 10)
      {
        return num;
      }

      var matches = pattern.Matches(word);
      if (matches.Count() > 0)
      {
        switch (matches.First().Value)
        {
          case "one":
            return 1;
          case "two":
            return 2;
          case "three":
            return 3;
          case "four":
            return 4;
          case "five":
            return 5;
          case "six":
            return 6;
          case "seven":
            return 7;
          case "eight":
            return 8;
          case "nine":
            return 9;
          default:
            Console.WriteLine($"How did you get here {matches.First().Value}");
            return 10;
        }
      }

      return 10;
    }
  }

}