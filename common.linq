<Query Kind="Program">
  <NuGetReference>morelinq</NuGetReference>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

public static class Helpers
{
    public static async Task<List<string>> LoadInput(int year, int day)
    {
        var inputDirectory = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input");
        if (!Directory.Exists(inputDirectory))
        {
            Directory.CreateDirectory(inputDirectory);
        }
        var inputFilePath = Path.Combine(inputDirectory, $"day{day:D2}.txt");
        if (!File.Exists(inputFilePath))
        {
            var session = Util.GetPassword("aoc.session");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("cookie", $"session={session}");
            httpClient.BaseAddress = new Uri("https://adventofcode.com");
            var response = await httpClient.GetAsync($"/{year}/day/{day}/input");
            using (var fs = new FileStream(inputFilePath, FileMode.CreateNew))
            {
                await response.Content.CopyToAsync(fs);
                await fs.FlushAsync();
            }

        }
        var lines = await File.ReadAllLinesAsync(inputFilePath);
        return lines.ToList();
    }
}

class AGrid<T> : Dictionary<APoint, T>
{
    public APoint GetMax()
    {
        var x = Keys.Select(x => x.X).Max();
        var y = Keys.Select(x => x.Y).Max();
        return new APoint(x, y);
    }

    // Requires the following CSS to be set in linqpad's Preferences->Results->Style sheet for text (HTML) results
    //body
    //{
    //	margin: 0.3em 0.3em 0.4em 0.4em;
    //	font-family: Consolas;
    //}
    override public string ToString()
    {
        var max = GetMax();
        var sb = new StringBuilder();
        var location = new APoint(0, 0);
        for (location.Y = 0; max.Y >= location.Y; location.Y++)
        {
            for (location.X = 0; max.X >= location.X; location.X++)
            {
                if (TryGetValue(location, out var value))
                {
                    sb.Append(value);
                }
                else
                {
                    sb.Append(".");
                }
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
    
    object ToDump() { return ToString(); }
}

struct APoint
{
    public APoint(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X; //east/west
    public int Y; //north/south
}
