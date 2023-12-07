namespace Day07
{
  class Hand : IEquatable<Hand>, IComparable<Hand>
  {
    protected Dictionary<char, long> cardRanks = new(){
      {'A', 12},
      {'K', 11},
      {'Q', 10},
      {'J', 9},
      {'T', 8},
      {'9', 7},
      {'8', 6},
      {'7', 5},
      {'6', 4},
      {'5', 3},
      {'4', 2},
      {'3', 1},
      {'2', 0},
    };

    protected long bet;
    public string handString;

    public long Bet
    {
      get { return bet; }
    }

    protected long secondaryScore;
    protected long primaryScore;

    public long Score
    {
      get { return 0; }
    }

    Dictionary<char, long> cards;

    public bool Equals(Hand other)
    {
      return other != null && this.handString.Equals(other.handString);
    }

    public int CompareTo(Hand other)
    {
      if (other == null)
      {
        return 1;
      }

      if (this.primaryScore != other.primaryScore)
      {
        return this.primaryScore.CompareTo(other.primaryScore);
      }

      return this.secondaryScore.CompareTo(other.secondaryScore);
    }

    public override string ToString()
    {
      return $"Hand: {handString}, primary: {primaryScore}, secondary:{secondaryScore}";
    }

    public Hand(string line)
    {
      cards = new();
      string[] parts = line.Split(" ");
      bet = long.Parse(parts[1]);
      handString = parts[0];

      string secondary = "";

      for (int ii = 0; ii < parts[0].Length; ii++)
      {
        if (!cards.ContainsKey(parts[0][ii]))
        {
          cards[parts[0][ii]] = 0;
        }
        secondary += cardRanks[parts[0][ii]].ToString("D2");
        cards[parts[0][ii]]++;
      }

      switch (cards.Values.Max())
      {
        case 5:
          // Five of a kind
          // Console.WriteLine($"Five of a kind {line}");
          primaryScore = 7;
          break;
        case 4:
          // four of a kind
          // Console.WriteLine($"Four of a kind {line}");
          primaryScore = 6;
          break;
        case 3:
          if (cards.Values.Min() == 2)
          {
            // full house
            primaryScore = 5;
          }
          else
          {
            // Three of a kind
            primaryScore = 4;
          }
          break;
        case 2:
          if (cards.Values.Where(val => val == 2).Count() == 2)
          {
            // Two pair
            primaryScore = 3;
          }
          else
          {
            // Single pair
            primaryScore = 2;
          }
          break;
        default:
          primaryScore = 1;
          break;
      }

      secondaryScore = long.Parse(secondary);
    }
  }
}