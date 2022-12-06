<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "..\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(2015, 1);
    var chars = content.Single().ToCharArray();

    Part1(chars).Dump("Part1");
    Part2(chars).Dump("Part2");
}

int Part1(char[] chars)
{
    var floor = 0;
    foreach (var c in chars)
    {
        if (c == '(')
        {
            floor++;
        }
        else if(c == ')')
        {
            floor--;
        }
    }
    return floor;
}

int Part2(char[] chars)
{
    var floor = 0;
    var position = 1;
    foreach (var c in chars)
    {
        if (c == '(')
        {
            floor++;
        }
        else if (c == ')')
        {
            floor--;
        }
        if(floor == -1)
        {
            return position;    
        }
        position++;
    }
    return position;
}