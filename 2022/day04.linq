<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load ".\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(4);

    Part1(content).Dump("Part1");
    Part2(content).Dump("Part1");
}

int Part1(List<string> content)
{
    var parsed = content.Select(x => ConvertInput(x));
    return parsed.Where(x => (x.First.Lower <= x.Second.Lower && x.First.Upper >= x.Second.Upper) || (x.Second.Lower <= x.First.Lower && x.Second.Upper >= x.First.Upper)).Count();
}

int Part2(List<string> content)
{
    var parsed = content.Select(x => ConvertInput(x));
    return parsed.Where(x => (x.First.Lower <= x.Second.Upper && x.First.Upper >= x.Second.Lower) || (x.Second.Lower <= x.First.Upper && x.Second.Upper >= x.First.Lower)).Count();
}

(Bound First, Bound Second) ConvertInput(string input)
{
    var parts = input.Split(',');
    return (ConvertRange(parts[0]), ConvertRange(parts[1]));
}

Bound ConvertRange(string input)
{
    var parts = input.Split('-');
    return new Bound
    {
        Lower = int.Parse(parts[0]),
        Upper = int.Parse(parts[1])
    };
}

class Bound
{
    public int Lower;
    public int Upper;
}
    