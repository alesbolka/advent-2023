namespace Day07
{
  class Task2
  {
    AdventUtils.Input data;
    public Task2(AdventUtils.Input input)
    {
      data = input;
    }

    public long Execute()
    {
      long res = 0;

      List<Hand> hands = new();
      foreach (string line in data.GetLines())
      {
        hands.Add(new Hand(line, true));
      }

      hands.Sort();

      for (int ii = 0; ii < hands.Count; ii++)
      {
        res += (ii + 1) * hands[ii].Bet;
      }

      return res;
    }
  }
}