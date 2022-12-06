<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "..\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(2022, 5);

    Part1(content).Dump("Part1");
    Part2(content).Dump("Part2");
}

string Part1(List<string> content)
{
    var rawParts = content.Split("").ToList();
    var rawShip = rawParts[0].ToList();
    var rawMoves = rawParts[1].ToList();
    var ship = ConvertShip(rawShip);
    //ship.Dump();
    var moves = ConvertMoves(rawMoves);
    //moves.Dump();
    foreach(var move in moves)
    {
        for(var i = 0; i < move.Count; i++)
        {
            var src = ship[move.Source].Pop();
            ship[move.Dest].Push(src);
            //$"Moved {src} from {move.Source} to {move.Dest}".Dump();
        }    
    }
    //ship.Dump();
    var output = "";
    foreach(var col in ship)
    {
        output += col.Peek();    
    }
    return output;
}

string Part2(List<string> content)
{
    var rawParts = content.Split("").ToList();
    var rawShip = rawParts[0].ToList();
    var rawMoves = rawParts[1].ToList();
    var ship = ConvertShip(rawShip);
    //ship.Dump();
    var moves = ConvertMoves(rawMoves);
    //moves.Dump();
    foreach (var move in moves)
    {
        var temp = new Stack<char>();
        for (var i = 0; i < move.Count; i++)
        {
            var src = ship[move.Source].Pop();
            temp.Push(src);
        }
        foreach (var t in temp)
        {
            ship[move.Dest].Push(t);
            //$"Moved {t} from {move.Source} to {move.Dest}".Dump();
        }

    }
    //ship.Dump();
    var output = "";
    foreach (var col in ship)
    {
        output += col.Peek();
    }
    return output;
}


static Regex ShipRegex = new Regex(@"\[[A-Z]\]|[ ]{4}", RegexOptions.Compiled);

List<Stack<char>> ConvertShip(List<string> content)
{
    var parsed = new List<Stack<char>>();
    
    content.Reverse();
    var data = content.Skip(1);
    foreach (var line in data)
    {
        var groups = ShipRegex.Matches(line);
        var index = 0;
        foreach(var match in groups)
        {
            if(parsed.Count < index + 1)
            {
                parsed.Add(new Stack<char>());
            }
            var value = match.ToString().ToCharArray()[1];
            if(value != ' ')
            {
                parsed[index].Push(value);
            }
            index++;
        }
    }
    return parsed;
}


static Regex MoveRegex = new Regex(@"move ([0-9]*) from ([0-9]) to ([0-9])", RegexOptions.Compiled);

List<Move> ConvertMoves(List<string> content)
{
    return content.Select(x =>
    {
        var groups = MoveRegex.Match(x).Groups;
        return new Move
        {
            Count = int.Parse(groups[1].Value),
            Source = int.Parse(groups[2].Value) - 1,
            Dest = int.Parse(groups[3].Value) - 1,
        };
    }).ToList();
}

class Move 
{
    public int Count;
    public int Source;
    public int Dest;
}