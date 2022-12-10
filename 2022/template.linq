<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "..\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(2022, 0);

    Part1(content).Dump("Part1");
}

int Part1(List<string> input)
{
    return 0;
}

