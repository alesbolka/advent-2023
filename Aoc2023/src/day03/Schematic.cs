using System.Text.RegularExpressions;

namespace Day03
{
  struct NumberLocation
  {
    public NumberLocation(int row, int start, int length, int value)
    {
      Row = row;
      Start = start;
      Length = length;
      Value = value;
      // Console.WriteLine($"{row}, {start}, {length}, {value}");
    }
    public int Row;
    public int Start;
    public int Length;
    public int Value;

    public override string ToString() => $"(row: {Row}, start: {Start}, length: {Length}, val: {Value})";
  }

  class Schematic
  {
    public static Regex numberMatcher = new Regex(@"(\d+)");

    protected List<List<char>> grid;

    protected List<NumberLocation> numbers;

    protected Dictionary<int, Dictionary<int, NumberLocation>> numberGrid;

    protected List<(int PosY, int PosX)> gearLocations;

    public Schematic(AdventUtils.Input input)
    {
      grid = new();
      numbers = new();
      gearLocations = new();
      numberGrid = new();

      int ii = 0;

      foreach (string line in input.GetLines())
      {
        grid.Add(line.ToList());

        string numStr = "";
        for (int linePos = 0; linePos < line.Length; linePos++)
        {
          if (line[linePos] == '*')
          {
            gearLocations.Add((ii, linePos));
          }

          var val = line[linePos] - '0';
          if (val < 0 || val > 9)
          {
            if (numStr.Length > 0)
            {
              var num = new NumberLocation(ii, linePos - numStr.Length, numStr.Length, int.Parse(numStr));
              numbers.Add(num);
              if (!numberGrid.ContainsKey(ii))
              {
                numberGrid[ii] = new();
              }

              for (int jj = 0; jj < num.Length; jj++)
              {
                numberGrid[ii][num.Start + jj] = num;
              }

              numStr = "";
            }
            continue;
          }

          numStr += line[linePos];
        }

        if (numStr.Length > 0)
        {
          var num = new NumberLocation(ii, line.Length - numStr.Length, numStr.Length, int.Parse(numStr));
          if (!numberGrid.ContainsKey(ii))
          {
            numberGrid[ii] = new();
          }

          for (int jj = 0; jj < num.Length; jj++)
          {
            numberGrid[ii][num.Start + jj] = num;
          }

          numbers.Add(num);
          numStr = "";
        }
        ii++;
      }
    }

    public int FindGearRatio()
    {
      int res = 0;

      foreach (var gear in gearLocations)
      {
        var nums = ExtractAdjacentNumbers(gear);
        if (nums.Count == 2)
        {
          res += nums[0] * nums[1];
        }
      }

      return res;
    }

    public List<int> FindPartNumbers()
    {
      List<int> res = new();

      // This code is awful, but I was experimenting with c#, which led me down weird logic paths
      foreach (NumberLocation number in numbers)
      {
        int indexAfterNumber = number.Start + number.Length;
        int lastRow = grid.Count() - 1;

        if (number.Start > 0)
        {
          // Left of the number
          if (isSpecialSymbol(grid[number.Row][number.Start - 1]))
          {
            res.Add(number.Value);
            continue;
          }
        }

        if (indexAfterNumber < grid[number.Row].Count())
        {
          // Right of the number
          if (isSpecialSymbol(grid[number.Row][indexAfterNumber]))
          {
            res.Add(number.Value);
            continue;
          }
        }

        // Above / below the number
        int startIndex = Math.Max(number.Start - 1, 0);
        int endIndex = Math.Min(indexAfterNumber, grid[number.Row].Count() - 1);

        for (int ii = startIndex; ii <= endIndex; ii++)
        {
          if (number.Row > 0)
          {
            if (isSpecialSymbol(grid[number.Row - 1][ii]))
            {
              res.Add(number.Value);
              break;
            }
          }

          if (number.Row < lastRow)
          {
            if (isSpecialSymbol(grid[number.Row + 1][ii]))
            {
              res.Add(number.Value);
              break;
            }
          }
        }
      }

      return res;
    }


    protected List<int> ExtractAdjacentNumbers((int PosY, int PosX) location)
    {
      List<int> res = new();

      for (int yy = -1; yy <= 1; yy++)
      {
        int checkY = location.PosY + yy;
        for (int xx = -1; xx <= 1; xx++)
        {
          int checkX = location.PosX + xx;
          if (numberGrid.ContainsKey(checkY) && numberGrid[checkY].ContainsKey(checkX))
          {
            var num = numberGrid[checkY][checkX];
            if (!res.Contains(num.Value))
            {
              res.Add(num.Value);
            }
          }

        }
      }

      return res;
    }

    protected bool isSpecialSymbol(char symbol)
    {
      if (symbol == '.')
      {
        return false;
      }

      return symbol < '0' || symbol > '9';
    }
  }
}