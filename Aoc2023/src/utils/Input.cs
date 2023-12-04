namespace AdventUtils
{
  public class Input
  {
    private string raw;

    public Input(string data)
    {
      raw = data;
    }


    public string[] GetLines()
    {
      return raw.Split("\n");
    }
  }
}