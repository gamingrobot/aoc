<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "..\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(2022, 6);
    var parsed = content.Single().ToCharArray();

    Part1(parsed).Dump("Part1");
    Part2(parsed).Dump("Part2");
}

int Part1(char[] content)
{
    var seqLen = 4;
    for(var i = 0; i < content.Length; i++)
    {
        var segment = content.Skip(i).Take(seqLen);
        if(segment.Distinct().Count() == seqLen)
        {
            return i + seqLen;
        } 
    }
    throw new Exception("Did not find sequence");
}

int Part2(char[] content)
{
    var seqLen = 14;
    for (var i = 0; i < content.Length; i++)
    {
        var segment = content.Skip(i).Take(seqLen);
        if (segment.Distinct().Count() == seqLen)
        {
            return i + seqLen;
        }
    }
    throw new Exception("Did not find sequence");
}