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
        var inputFilePath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input", $"day{day:D2}.txt");
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
