namespace Day04
{
  public class Ticket
  {
    int id;

    public int Id
    {
      get { return id; }
    }

    List<int> winners;
    List<int> ticketNumbers;


    public Ticket(string line)
    {
      string[] raw = line.Split(":");

      id = int.Parse(raw[0].Split(" ").Last());

      string[] numbers = raw[1].Split("|");
      winners = CleanParse(numbers[0]);
      ticketNumbers = CleanParse(numbers[1]);
    }

    public int PointValue()
    {
      int res = 0;
      foreach (int val in ticketNumbers)
      {
        if (winners.Contains(val))
        {
          res = res == 0 ? 1 : res * 2;
        }
      }

      return res;
    }

    protected List<int> CleanParse(string line)
    {
      List<int> res = new();

      foreach (string part in line.Split(" "))
      {
        string cleaned = part.Trim();
        if (cleaned.Length == 0)
        {
          continue;
        }

        res.Add(int.Parse(cleaned));
      }

      return res;
    }
  }
}