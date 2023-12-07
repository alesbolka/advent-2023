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

    protected Dictionary<char, long> cardRanksJoker = new(){
      {'A', 13},
      {'K', 12},
      {'Q', 11},
      {'J', 1},
      {'T', 10},
      {'9', 9},
      {'8', 8},
      {'7', 7},
      {'6', 6},
      {'5', 5},
      {'4', 4},
      {'3', 3},
      {'2', 2},
    };

    protected long bet;
    public string handString;

    public long Bet
    {
      get { return bet; }
    }

    protected long jokerScore;
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

    public Hand(string line, bool jokerSpecial = false)
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

        secondary += jokerSpecial ? cardRanksJoker[parts[0][ii]].ToString("D2") : cardRanks[parts[0][ii]].ToString("D2");
        cards[parts[0][ii]]++;
      }


      primaryScore = DeterminePrimaryScore(cards.Values.ToList());
      secondaryScore = long.Parse(secondary);

      if (jokerSpecial && cards.ContainsKey('J') && cards.Count > 1)
      {
        long jokerCount = cards['J'];
        cards.Remove('J');
        var maxKey = cards.MaxBy(kvp => kvp.Value).Key;
        cards[maxKey] += jokerCount;
        primaryScore = DeterminePrimaryScore(cards.Values.ToList());
      }
    }

    protected long DeterminePrimaryScore(List<long> values)
    {
      switch (values.Max())
      {
        case 5:
          // Five of a kind
          return 7;
        case 4:
          // four of a kind
          return 6;
        case 3:
          if (values.Min() == 2)
          {
            // full house
            return 5;
          }
          else
          {
            // Three of a kind
            return 4;
          }
        case 2:
          if (values.Where(val => val == 2).Count() == 2)
          {
            // Two pair
            return 3;
          }
          else
          {
            // Single pair
            return 2;
          }
        default:
          return 1;
      }
    }
  }
}