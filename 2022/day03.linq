<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load ".\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(3);

    Part1(content).Dump("Part1");
    Part2(content).Dump("Part2");
}

int Part1(List<string> content)
{
    var parsed = content.Select(x => ConvertInputPart1(x));
    return parsed.Select(x => x.FirstCompartment.Intersect(x.SecondCompartment).Single()).Sum();
}

int Part2(List<string> content)
{
    var parsed = content.Batch(3).Select(x => x.Select(y => ConvertInputLine(y)).ToList());
    return parsed.Select(x => x[0].Intersect(x[1]).Intersect(x[2]).Single()).Sum();
}

(List<int> FirstCompartment, List<int> SecondCompartment) ConvertInputPart1(string input)
{
    var converted = ConvertInputLine(input);
    var halfway = converted.Count/2;
    return (converted.Slice(0, halfway).ToList(), converted.Slice(halfway, converted.Count).ToList());
}

List<int> ConvertInputLine(string input)
{
    return input.Select(x => ConvertChar(x)).ToList();
}

int ConvertChar(char input)
{
    if(input >= 'A' && input <= 'Z')
    {
        return ((int)input - 38);
    }
    else if (input >= 'a' && input <= 'z')
    {
        return ((int)input - 96);
    }
    throw new IndexOutOfRangeException($"Unknown char {input}");
}
