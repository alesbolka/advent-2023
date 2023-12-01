namespace AdventUtils
{
  public class Input {
    private string raw;

    public Input (string data)
    {
      raw = data;
    }


    public IEnumerable<string> GetLines() {
      var lines = raw.Split("\n");
      foreach (var line in lines)
      {
        yield return line;
      }
    }
  }
}