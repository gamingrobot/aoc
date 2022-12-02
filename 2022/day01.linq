<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load ".\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(1);
    var parsed = content.Split("").Select(x => x.Select(int.Parse));

    Part1(parsed).Dump("Part1");
    Part2(parsed).Dump("Part2");
}

int Part1(IEnumerable<IEnumerable<int>> parsed)
{
    return parsed.Select(x => x.Sum()).Max();
}

int Part2(IEnumerable<IEnumerable<int>> parsed)
{
    var totals = parsed.Select(x => x.Sum());
    return totals.OrderByDescending(x => x).Take(3).Sum();
}

